using System;
using System.Runtime.InteropServices;

namespace AviRender {
	public class AudioStream : AviStream {

		public int CountBitsPerSample {
			get { return waveFormat.wBitsPerSample; }
		}

		public int CountSamplesPerSecond {
			get { return waveFormat.nSamplesPerSec; }
		}

		public int CountChannels {
			get { return waveFormat.nChannels; }
		}

		/// <summary>the stream's format</summary>
		private Avi.PCMWAVEFORMAT waveFormat = new Avi.PCMWAVEFORMAT();

		/// <summary>Initialize an AudioStream for an existing stream</summary>
		/// <param name="aviFile">The file that contains the stream</param>
		/// <param name="aviStream">An IAVISTREAM from [aviFile]</param>
		public AudioStream(int aviFile, IntPtr aviStream) {
			this.aviFile = aviFile;
			this.aviStream = aviStream;

			int size = Marshal.SizeOf(waveFormat);
			Avi.AVIStreamReadFormat(aviStream, 0, ref waveFormat, ref size);
			GetStreamInfo(aviStream);
		}

		/// <summary>Read the stream's header information</summary>
		/// <param name="aviStream">The IAVISTREAM to read from</param>
		/// <returns>AVISTREAMINFO</returns>
		private Avi.AVISTREAMINFO GetStreamInfo(IntPtr aviStream) {
			Avi.AVISTREAMINFO streamInfo = new Avi.AVISTREAMINFO();
			int result = Avi.AVIStreamInfo(aviStream, ref streamInfo, Marshal.SizeOf(streamInfo));
			if (result != 0) {
				throw new Exception("Exception in AVIStreamInfo: " + result);
			}
			return streamInfo;
		}

		/// <summary>Read the stream's header information</summary>
		/// <returns>AVISTREAMINFO</returns>
		public Avi.AVISTREAMINFO GetStreamInfo() {
			return GetStreamInfo(writeCompressed ? compressedStream : aviStream);
		}

		/// <summary>Read the stream's format information</summary>
		/// <returns>PCMWAVEFORMAT</returns>
		public Avi.PCMWAVEFORMAT GetFormat() {
			Avi.PCMWAVEFORMAT format = new Avi.PCMWAVEFORMAT();
			int size = Marshal.SizeOf(format);
			Avi.AVIStreamReadFormat(aviStream, 0, ref format, ref size);
			return format;
		}

		/// <summary>Returns all data needed to copy the stream</summary>
		/// <remarks>Do not forget to call Marshal.FreeHGlobal and release the raw data pointer</remarks>
		/// <param name="streamInfo">Receives the header information</param>
		/// <param name="format">Receives the format</param>
		/// <param name="streamLength">Receives the length of the stream</param>
		/// <returns>Pointer to the wave data</returns>
		public IntPtr GetStreamData(ref Avi.AVISTREAMINFO streamInfo, ref Avi.PCMWAVEFORMAT format, ref int streamLength) {
			streamInfo = GetStreamInfo();

			format = GetFormat();
			//length in bytes = length in samples * length of a sample
			streamLength = Avi.AVIStreamLength(aviStream.ToInt32()) * streamInfo.dwSampleSize;
			IntPtr waveData = Marshal.AllocHGlobal(streamLength);

			int result = Avi.AVIStreamRead(aviStream, 0, streamLength, waveData, streamLength, 0, 0);
			if (result != 0) {
				throw new Exception("Exception in AVIStreamRead: " + result);
			}

			return waveData;
		}

		/// <summary>Copy the stream into a new file</summary>
		/// <param name="fileName">Name of the new file</param>
		public override void ExportStream(String fileName) {
			Avi.AVICOMPRESSOPTIONS_CLASS opts = new Avi.AVICOMPRESSOPTIONS_CLASS {
				fccType = (UInt32) Avi.mmioStringToFOURCC("auds", 0),
				fccHandler = (UInt32) Avi.mmioStringToFOURCC("CAUD", 0),
				dwKeyFrameEvery = 0,
				dwQuality = 0,
				dwFlags = 0,
				dwBytesPerSecond = 0,
				lpFormat = new IntPtr(0),
				cbFormat = 0,
				lpParms = new IntPtr(0),
				cbParms = 0,
				dwInterleaveEvery = 0
			};

			Avi.AVISaveV(fileName, 0, 0, 1, ref aviStream, ref opts);
		}

	}
}
