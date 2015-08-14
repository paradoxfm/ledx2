using System;
using System.Drawing;
using System.Threading;
using System.Windows.Controls;

namespace AviRender {
	public class AviPlayer {

		private VideoStream videoStream;
		private PictureBox picDisplay;
		private Control ctlFrameIndexFeedback;
		private int millisecondsPerFrame;
		private bool isRunning;
		private int currentFrameIndex;
		private Bitmap currentBitmap;

		private delegate void SimpleDelegate();
		public event EventHandler Stopped;

		/// <summary>Returns the current playback status</summary>
		public bool IsRunning {
			get { return isRunning; }
		}

		/// <summary>Create a new AVI Player</summary>
		/// <param name="videoStream">Video stream to play</param>
		/// <param name="picDisplay">PictureBox to display the video</param>
		/// <param name="ctlFrameIndexFeedback">Optional Label to show the current frame index</param>
		public AviPlayer(VideoStream videoStream, PictureBox picDisplay, Control ctlFrameIndexFeedback) {
			this.videoStream = videoStream;
			this.picDisplay = picDisplay;
			this.ctlFrameIndexFeedback = ctlFrameIndexFeedback;
			this.isRunning = false;
		}

		/// <summary>Start the video playback</summary>
		public void Start() {
			isRunning = true;
			millisecondsPerFrame = (int)(1000 / videoStream.FrameRate);
			Thread thread = new Thread(new ThreadStart(Run));
			thread.Start();
		}

		/// <summary>Extract and display the frames</summary>
		private void Run() {
			videoStream.GetFrameOpen();

			for (currentFrameIndex = 0; (currentFrameIndex < videoStream.CountFrames) && isRunning; currentFrameIndex++) {
				//show frame
				currentBitmap = videoStream.GetBitmap(currentFrameIndex);
				picDisplay.Invoke(new SimpleDelegate(SetDisplayPicture));
				picDisplay.Invoke(new SimpleDelegate(picDisplay.Refresh));

				//show position
				if (ctlFrameIndexFeedback != null) {
					ctlFrameIndexFeedback.Invoke(new SimpleDelegate(SetLabelText));
				}

				//wait for the next frame
				Thread.Sleep(millisecondsPerFrame);
			}

			videoStream.GetFrameClose();
			isRunning = false;

			if (Stopped != null) {
				Stopped(this, EventArgs.Empty);
			}
		}

		/// <summary>Change the visible frame</summary>
		private void SetDisplayPicture() {
			picDisplay.Image = currentBitmap;
		}

		/// <summary>Change the frame index feedback</summary>
		private void SetLabelText() {
			ctlFrameIndexFeedback.Text = currentFrameIndex.ToString();
		}

		/// <summary>Stop the video playback</summary>
		public void Stop() {
			isRunning = false;
		}
	}
}
