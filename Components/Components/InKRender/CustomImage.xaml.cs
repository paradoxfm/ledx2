using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;

namespace LEDX.Components {
	/// <summary>
	/// Interaction logic for CUstomImage.xaml
	/// </summary>
	public partial class CustomImage : Image, IAtimate {

		private bool? isPlaying = false;
		private double plTim = 0;

		public CustomImage() {
			InitializeComponent();
		}

		#region IAtimate Members

		public bool? IsPlay {
			get {
				return isPlaying;
			}
			set {
				if (value == null) {
					sBrd.Pause(this);
					sBrd.SeekAlignedToLastTick(this, TimeSpan.FromSeconds(0), TimeSeekOrigin.BeginTime);
				} else if ((bool)value)
					sBrd.Resume(this);
				else
					sBrd.Pause(this);
				isPlaying = value;
			}
		}

		public double PlayTime {
			get {
				try {
					plTim = sBrd.GetCurrentTime(this).GetValueOrDefault().TotalSeconds;
				} catch {
				}
				return plTim;
			}
			set {
				sBrd.SeekAlignedToLastTick(this, TimeSpan.FromSeconds(value), TimeSeekOrigin.BeginTime);
			}
		}

		public static readonly DependencyProperty ContrProperty = DependencyProperty.Register("Contr",
			typeof(Model.Controller), typeof(CustomImage));

		public Model.Controller Contr {
			get { return (Model.Controller)GetValue(ContrProperty); }
			set { SetValue(ContrProperty, value); }
		}

		protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e) {
			base.OnPropertyChanged(e);
			if (e.Property.Name == "Contr")
				UpdateAnimation();
		}

		public void UpdateAnimation() {
			if (Contr == null)
				return;
			double MaxSeconds = Contr.MaxLength;
			clAni.KeyFrames.Clear();
			clAni.Duration = TimeSpan.FromSeconds(MaxSeconds);
			Model.Controller cn = Contr;
			if (cn.Frames.Count > 0) {
				double endTime = MaxSeconds - cn.Length;
				Model.Frame frm = cn.Frames[0];
				clAni.KeyFrames.Add(GetFrame(frm.BegColor, 0));
				double offs = 0;
				for (int i = 0; i < cn.Frames.Count; i++) {
					frm = cn.Frames[i];
					if (i > 0 && cn.Frames[i - 1].EndColor != frm.BegColor)
						clAni.KeyFrames.Add(GetFrame(frm.BegColor, offs));
					offs += frm.Length;
					clAni.KeyFrames.Add(GetFrame(frm.EndColor, offs));
				}
				frm = cn.Frames[cn.Frames.Count - 1];
				clAni.KeyFrames.Add(GetFrame(Colors.Black, offs));
			} else {
				clAni.KeyFrames.Add(GetFrame(Colors.Black, 0));
				clAni.KeyFrames.Add(GetFrame(Colors.Black, MaxSeconds));
			}
			sBrd.Begin(this, true);
			if (!isPlaying.GetValueOrDefault())
				sBrd.Pause(this);
			TimeSpan prg = TimeSpan.FromSeconds(PlayTime);
			sBrd.Seek(this, prg, TimeSeekOrigin.BeginTime);
		}

		public LinearColorKeyFrame GetFrame(Color cl, double time) {
			return new LinearColorKeyFrame(cl, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(time)));
		}
		#endregion

	}
}
