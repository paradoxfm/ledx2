using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;

namespace AviRender {
	public class VideoStream : AviStream {
		/// <summary>handle for AVIStreamGetFrame</summary>
		private int getFrameObject;

		public int FrameSize { get; private set; }

		public double FrameRate { get; protected set; }

		public int Width { get; private set; }

		public int Height { get; private set; }

		public short CountBitsPerPixel { get; private set; }

		public int CountFrames { get; protected set; }

		public int FirstFrame { get; protected set; }

		private Avi.AVICOMPRESSOPTIONS compressOptions;
		public Avi.AVICOMPRESSOPTIONS CompressOptions {
			get { return compressOptions; }
		}

		public Avi.AVISTREAMINFO StreamInfo {
			get { return GetStreamInfo(aviStream); }
		}

		/// <summary>Initialize an empty VideoStream</summary>
		/// <param name="aviFile">The file that contains the stream</param>
		/// <param name="writeCompressed">true: Create a compressed stream before adding frames</param>
		/// <param name="frameRate">Frames per second</param>
		/// <param name="frameSize">Size of one frame in bytes</param>
		/// <param name="width">Width of each image</param>
		/// <param name="height">Height of each image</param>
		/// <param name="format">PixelFormat of the images</param>
		public VideoStream(int aviFile, bool writeCompressed, double frameRate, int frameSize, int width, int height, PixelFormat format) {
			CountFrames = 0;
			this.aviFile = aviFile;
			this.writeCompressed = writeCompressed;
			FrameRate = frameRate;
			FrameSize = frameSize;
			Width = width;
			Height = height;
			CountBitsPerPixel = ConvertPixelFormatToBitCount(format);
			FirstFrame = 0;

			CreateStream();
		}

		/// <summary>Initialize a new VideoStream and add the first frame</summary>
		/// <param name="aviFile">The file that contains the stream</param>
		/// <param name="writeCompressed">true: create a compressed stream before adding frames</param>
		/// <param name="frameRate">Frames per second</param>
		/// <param name="firstFrame">Image to write into the stream as the first frame</param>
		public VideoStream(int aviFile, bool writeCompressed, double frameRate, Bitmap firstFrame) {
			CountFrames = 0;
			FirstFrame = 0;
			Initialize(aviFile, writeCompressed, frameRate, firstFrame);
			CreateStream();
			AddFrame(firstFrame);
		}

		/// <summary>Initialize a new VideoStream and add the first frame</summary>
		/// <param name="aviFile">The file that contains the stream</param>
		/// <param name="frameRate">Frames per second</param>
		/// <param name="firstFrame">Image to write into the stream as the first frame</param>
		public VideoStream(int aviFile, Avi.AVICOMPRESSOPTIONS compressOptions, double frameRate, Bitmap firstFrame) {
			CountFrames = 0;
			FirstFrame = 0;
			Initialize(aviFile, true, frameRate, firstFrame);
			CreateStream(compressOptions);
			AddFrame(firstFrame);
		}

		/// <summary>Initialize a VideoStream for an existing stream</summary>
		/// <param name="aviFile">The file that contains the stream</param>
		/// <param name="aviStream">An IAVISTREAM from [aviFile]</param>
		public VideoStream(int aviFile, IntPtr aviStream) {
			this.aviFile = aviFile;
			this.aviStream = aviStream;

			Avi.BITMAPINFOHEADER bih = new Avi.BITMAPINFOHEADER();
			int size = Marshal.SizeOf(bih);
			Avi.AVIStreamReadFormat(aviStream, 0, ref bih, ref size);
			Avi.AVISTREAMINFO streamInfo = GetStreamInfo(aviStream);

			FrameRate = (float)streamInfo.dwRate / (float)streamInfo.dwScale;
			Width = (int)streamInfo.rcFrame.right;
			Height = (int)streamInfo.rcFrame.bottom;
			FrameSize = bih.biSizeImage;
			CountBitsPerPixel = bih.biBitCount;
			FirstFrame = Avi.AVIStreamStart(aviStream.ToInt32());
			CountFrames = Avi.AVIStreamLength(aviStream.ToInt32());
		}

		/// <summary>Copy all properties from one VideoStream to another one</summary>
		/// <remarks>Used by EditableVideoStream</remarks>
		/// <param name="frameSize"></param><param name="frameRate"></param>
		/// <param name="width"></param><param name="height"></param>
		/// <param name="countBitsPerPixel"></param>
		/// <param name="countFrames"></param><param name="compressOptions"></param>
		internal VideoStream(int frameSize, double frameRate, int width, int height, Int16 countBitsPerPixel, int countFrames, Avi.AVICOMPRESSOPTIONS compressOptions, bool writeCompressed) {
			FrameSize = frameSize;
			FrameRate = frameRate;
			Width = width;
			Height = height;
			CountBitsPerPixel = countBitsPerPixel;
			CountFrames = countFrames;
			this.compressOptions = compressOptions;
			this.writeCompressed = writeCompressed;
			FirstFrame = 0;
		}

		/// <summary>Initialize a new VideoStream</summary>
		/// <remarks>Used only by constructors</remarks>
		/// <param name="aviFile">The file that contains the stream</param>
		/// <param name="writeCompressed">true: create a compressed stream before adding frames</param>
		/// <param name="frameRate">Frames per second</param>
		/// <param name="firstFrame">Image to write into the stream as the first frame</param>
		private void Initialize(int aviFile, bool writeCompressed, double frameRate, Bitmap firstFrameBitmap) {
			this.aviFile = aviFile;
			this.writeCompressed = writeCompressed;
			FrameRate = frameRate;
			FirstFrame = 0;

			BitmapData bmpData = firstFrameBitmap.LockBits(new Rectangle(0, 0, firstFrameBitmap.Width, firstFrameBitmap.Height),
														   ImageLockMode.ReadOnly, firstFrameBitmap.PixelFormat);

			FrameSize = bmpData.Stride * bmpData.Height;
			Width = firstFrameBitmap.Width;
			Height = firstFrameBitmap.Height;
			CountBitsPerPixel = ConvertPixelFormatToBitCount(firstFrameBitmap.PixelFormat);

			firstFrameBitmap.UnlockBits(bmpData);
		}

		/// <summary>Get the count of bits per pixel from a PixelFormat value</summary>
		/// <param name="format">One of the PixelFormat members beginning with "Format..." - all others are not supported</param>
		/// <returns>bit count</returns>
		private Int16 ConvertPixelFormatToBitCount(PixelFormat format) {
			String formatName = format.ToString();
			if (formatName.Substring(0, 6) != "Format") {
				throw new Exception("Unknown pixel format: " + formatName);
			}
			formatName = formatName.Substring(6, 2);
			return Int16.Parse(Char.IsNumber(formatName[1]) ? formatName : formatName[0].ToString());
		}

		/// <summary>Returns a PixelFormat value for a specific bit count</summary>
		/// <param name="bitCount">count of bits per pixel</param>
		/// <returns>A PixelFormat value for [bitCount]</returns>
		private PixelFormat ConvertBitCountToPixelFormat(int bitCount) {
			String formatName;
			if (bitCount > 16) {
				formatName = String.Format("Format{0}bppRgb", bitCount);
			} else if (bitCount == 16) {
				formatName = "Format16bppRgb555";
			} else { // < 16
				formatName = String.Format("Format{0}bppIndexed", bitCount);
			}

			return (PixelFormat)Enum.Parse(typeof(PixelFormat), formatName);
		}

		private Avi.AVISTREAMINFO GetStreamInfo(IntPtr aviStream) {
			Avi.AVISTREAMINFO streamInfo = new Avi.AVISTREAMINFO();
			int result = Avi.AVIStreamInfo(StreamPointer, ref streamInfo, Marshal.SizeOf(streamInfo));
			if (result != 0) {
				throw new Exception("Exception in VideoStreamInfo: " + result);
			}
			return streamInfo;
		}

		private void GetRateAndScale(ref double frameRate, ref int scale) {
			scale = 1;
			while (frameRate != (long)frameRate) {
				frameRate = frameRate * 10;
				scale *= 10;
			}
		}

		/// <summary>Create a new stream</summary>
		private void CreateStreamWithoutFormat() {
			int scale = 1;
			double rate = FrameRate;
			GetRateAndScale(ref rate, ref scale);

			Avi.AVISTREAMINFO strhdr = new Avi.AVISTREAMINFO {
				fccType = Avi.mmioStringToFOURCC("vids", 0),
				fccHandler = Avi.mmioStringToFOURCC("CVID", 0),
				dwFlags = 0,
				dwCaps = 0,
				wPriority = 0,
				wLanguage = 0,
				dwScale = scale,
				dwRate = (int)rate,
				dwStart = 0,
				dwLength = 0,
				dwInitialFrames = 0,
				dwSuggestedBufferSize = FrameSize,
				dwQuality = -1,
				dwSampleSize = 0,
				rcFrame = { top = 0, left = 0, bottom = (uint)Height, right = (uint)Width },
				dwEditCount = 0,
				dwFormatChangeCount = 0,
				szName = new UInt16[64]
			};

			int result = Avi.AVIFileCreateStream(aviFile, out aviStream, ref strhdr);

			if (result != 0) {
				throw new Exception("Exception in AVIFileCreateStream: " + result);
			}
		}

		/// <summary>Create a new stream</summary>
		private void CreateStream() {
			CreateStreamWithoutFormat();

			if (writeCompressed) {
				CreateCompressedStream();
			} else {
				SetFormat(aviStream);
			}
		}

		/// <summary>Create a new stream</summary>
		private void CreateStream(Avi.AVICOMPRESSOPTIONS options) {
			CreateStreamWithoutFormat();
			CreateCompressedStream(options);
		}

		/// <summary>Create a compressed stream from an uncompressed stream</summary>
		private void CreateCompressedStream() {
			//display the compression options dialog...
			Avi.AVICOMPRESSOPTIONS_CLASS options = new Avi.AVICOMPRESSOPTIONS_CLASS {
				fccType = (uint)Avi.streamtypeVIDEO,
				lpParms = IntPtr.Zero,
				lpFormat = IntPtr.Zero
			};

			Avi.AVISaveOptions(IntPtr.Zero, Avi.ICMF_CHOOSE_KEYFRAME | Avi.ICMF_CHOOSE_DATARATE, 1, ref aviStream, ref options);
			Avi.AVISaveOptionsFree(1, ref options);

			//..or set static options
			/*Avi.AVICOMPRESSOPTIONS opts = new Avi.AVICOMPRESSOPTIONS();
			opts.fccType         = (UInt32)Avi.mmioStringToFOURCC("vids", 0);
			opts.fccHandler      = (UInt32)Avi.mmioStringToFOURCC("CVID", 0);
			opts.dwKeyFrameEvery = 0;
			opts.dwQuality       = 0;  // 0 .. 10000
			opts.dwFlags         = 0;  // AVICOMRPESSF_KEYFRAMES = 4
			opts.dwBytesPerSecond= 0;
			opts.lpFormat        = new IntPtr(0);
			opts.cbFormat        = 0;
			opts.lpParms         = new IntPtr(0);
			opts.cbParms         = 0;
			opts.dwInterleaveEvery = 0;*/

			//get the compressed stream
			compressOptions = options.ToStruct();
			int result = Avi.AVIMakeCompressedStream(out compressedStream, aviStream, ref compressOptions, 0);
			if (result != 0) {
				throw new Exception("Exception in AVIMakeCompressedStream: " + result);
			}

			SetFormat(compressedStream);
		}

		/// <summary>Create a compressed stream from an uncompressed stream</summary>
		private void CreateCompressedStream(Avi.AVICOMPRESSOPTIONS options) {
			int result = Avi.AVIMakeCompressedStream(out compressedStream, aviStream, ref options, 0);
			if (result != 0) {
				throw new Exception("Exception in AVIMakeCompressedStream: " + result);
			}

			compressOptions = options;

			SetFormat(compressedStream);
		}

		/// <summary>Add one frame to a new stream</summary>
		/// <param name="bmp"></param>
		/// <remarks>
		/// This works only with uncompressed streams,
		/// and compressed streams that have not been saved yet.
		/// Use DecompressToNewFile to edit saved compressed streams.
		/// </remarks>
		public void AddFrame(Bitmap bmp) {
			bmp.RotateFlip(RotateFlipType.RotateNoneFlipY);

			BitmapData bmpDat = bmp.LockBits(
				new Rectangle(
					0, 0, bmp.Width, bmp.Height),
				ImageLockMode.ReadOnly, bmp.PixelFormat);

			int result = Avi.AVIStreamWrite(writeCompressed ? compressedStream : StreamPointer,
											CountFrames, 1,
											bmpDat.Scan0,
											bmpDat.Stride * bmpDat.Height,
											0, 0, 0);

			if (result != 0) {
				throw new Exception("Exception in VideoStreamWrite: " + result);
			}

			bmp.UnlockBits(bmpDat);

			CountFrames++;
		}

		/// <summary>Apply a format to a new stream</summary>
		/// <param name="aviStream">The IAVISTREAM</param>
		/// <remarks>
		/// The format must be set before the first frame can be written,
		/// and it cannot be changed later.
		/// </remarks>
		private void SetFormat(IntPtr aviStream) {
			Avi.BITMAPINFOHEADER bi = new Avi.BITMAPINFOHEADER();
			bi.biSize = Marshal.SizeOf(bi);
			bi.biWidth = Width;
			bi.biHeight = Height;
			bi.biPlanes = 1;
			bi.biBitCount = CountBitsPerPixel;
			bi.biSizeImage = FrameSize;

			int result = Avi.AVIStreamSetFormat(aviStream, 0, ref bi, bi.biSize);
			if (result != 0) {
				throw new Exception("Error in VideoStreamSetFormat: " + result);
			}
		}

		/// <summary>Prepare for decompressing frames</summary>
		/// <remarks>
		/// This method has to be called before GetBitmap and ExportBitmap.
		/// Release ressources with GetFrameClose.
		/// </remarks>
		public void GetFrameOpen() {
			GetStreamInfo(StreamPointer);

			//Open frames

			Avi.BITMAPINFOHEADER bih = new Avi.BITMAPINFOHEADER();
			bih.biBitCount = CountBitsPerPixel;
			bih.biClrImportant = 0;
			bih.biClrUsed = 0;
			bih.biCompression = 0;
			bih.biPlanes = 1;
			bih.biSize = Marshal.SizeOf(bih);
			bih.biXPelsPerMeter = 0;
			bih.biYPelsPerMeter = 0;

			// Corrections by M. Covington:
			// If these are pre-set, interlaced video is not handled correctly.
			// Better to give zeroes and let Windows fill them in.
			bih.biHeight = 0; // was (Int32)streamInfo.rcFrame.bottom;
			bih.biWidth = 0; // was (Int32)streamInfo.rcFrame.right;

			// Corrections by M. Covington:
			// Validate the bit count, because some AVI files give a bit count
			// that is not one of the allowed values in a BitmapInfoHeader.
			// Here 0 means for Windows to figure it out from other information.
			if (bih.biBitCount > 24) {
				bih.biBitCount = 32;
			} else if (bih.biBitCount > 16) {
				bih.biBitCount = 24;
			} else if (bih.biBitCount > 8) {
				bih.biBitCount = 16;
			} else if (bih.biBitCount > 4) {
				bih.biBitCount = 8;
			} else if (bih.biBitCount > 0) {
				bih.biBitCount = 4;
			}

			getFrameObject = Avi.AVIStreamGetFrameOpen(StreamPointer, ref bih);

			if (getFrameObject == 0) { throw new Exception("Exception in VideoStreamGetFrameOpen!"); }
		}

		/// <summary>Export a frame into a bitmap file</summary>
		/// <param name="position">Position of the frame</param>
		/// <param name="dstFileName">Name of the file to store the bitmap</param>
		public void ExportBitmap(int position, String dstFileName) {
			Bitmap bmp = GetBitmap(position);
			bmp.Save(dstFileName, ImageFormat.Bmp);
			bmp.Dispose();
		}

		/// <summary>Export a frame into a bitmap</summary>
		/// <param name="position">Position of the frame</param>
		public Bitmap GetBitmap(int position) {
			if (position > CountFrames) {
				throw new Exception("Invalid frame position: " + position);
			}

			GetStreamInfo(StreamPointer);

			//Decompress the frame and return a pointer to the DIB
			int dib = Avi.AVIStreamGetFrame(getFrameObject, FirstFrame + position);
			//Copy the bitmap header into a managed struct
			Avi.BITMAPINFOHEADER bih = new Avi.BITMAPINFOHEADER();
			bih = (Avi.BITMAPINFOHEADER)Marshal.PtrToStructure(new IntPtr(dib), bih.GetType());

			if (bih.biSizeImage < 1) {
				throw new Exception("Exception in VideoStreamGetFrame");
			}

			//copy the image

			byte[] bitmapData;
			int address = dib + Marshal.SizeOf(bih);
			if (bih.biBitCount < 16) {
				//copy palette and pixels
				bitmapData = new byte[bih.biSizeImage + Avi.PALETTE_SIZE];
			} else {
				//copy only pixels
				bitmapData = new byte[bih.biSizeImage];
			}

			Marshal.Copy(new IntPtr(address), bitmapData, 0, bitmapData.Length);

			//copy bitmap info
			byte[] bitmapInfo = new byte[Marshal.SizeOf(bih)];
			IntPtr ptr;
			ptr = Marshal.AllocHGlobal(bitmapInfo.Length);
			Marshal.StructureToPtr(bih, ptr, false);
			address = ptr.ToInt32();
			Marshal.Copy(new IntPtr(address), bitmapInfo, 0, bitmapInfo.Length);

			Marshal.FreeHGlobal(ptr);

			//create file header
			Avi.BITMAPFILEHEADER bfh = new Avi.BITMAPFILEHEADER();
			bfh.bfType = Avi.BMP_MAGIC_COOKIE;
			bfh.bfSize = 55 + bih.biSizeImage; //size of file as written to disk
			bfh.bfReserved1 = 0;
			bfh.bfReserved2 = 0;
			bfh.bfOffBits = Marshal.SizeOf(bih) + Marshal.SizeOf(bfh);
			if (bih.biBitCount < 16) {
				//There is a palette between header and pixel data
				bfh.bfOffBits += Avi.PALETTE_SIZE;
			}

			//write a bitmap stream
			BinaryWriter bw = new BinaryWriter(new MemoryStream());

			//write header
			bw.Write(bfh.bfType);
			bw.Write(bfh.bfSize);
			bw.Write(bfh.bfReserved1);
			bw.Write(bfh.bfReserved2);
			bw.Write(bfh.bfOffBits);
			//write bitmap info
			bw.Write(bitmapInfo);
			//write bitmap data
			bw.Write(bitmapData);

			Bitmap bmp = (Bitmap)Image.FromStream(bw.BaseStream);
			Bitmap saveableBitmap = new Bitmap(bmp.Width, bmp.Height);
			Graphics g = Graphics.FromImage(saveableBitmap);
			g.DrawImage(bmp, 0, 0);
			g.Dispose();
			bmp.Dispose();

			bw.Close();
			return saveableBitmap;
		}

		/// <summary>Free ressources that have been used by GetFrameOpen</summary>
		public void GetFrameClose() {
			if (getFrameObject != 0) {
				Avi.AVIStreamGetFrameClose(getFrameObject);
				getFrameObject = 0;
			}
		}

		/// <summary>Copy all frames into a new file</summary>
		/// <param name="fileName">Name of the new file</param>
		/// <param name="recompress">true: Compress the new stream</param>
		/// <returns>AviManager for the new file</returns>
		/// <remarks>Use this method if you want to append frames to an existing, compressed stream</remarks>
		public AviManager DecompressToNewFile(String fileName, bool recompress, out VideoStream newStream2) {
			AviManager newFile = new AviManager(fileName, false);

			GetFrameOpen();

			Bitmap frame = GetBitmap(0);
			VideoStream newStream = newFile.AddVideoStream(recompress, FrameRate, frame);
			frame.Dispose();

			for (int n = 1; n < CountFrames; n++) {
				frame = GetBitmap(n);
				newStream.AddFrame(frame);
				frame.Dispose();
			}

			GetFrameClose();

			newStream2 = newStream;
			return newFile;
		}

		/// <summary>Copy the stream into a new file</summary>
		/// <param name="fileName">Name of the new file</param>
		public override void ExportStream(String fileName) {
			Avi.AVICOMPRESSOPTIONS_CLASS opts = new Avi.AVICOMPRESSOPTIONS_CLASS {
				fccType = (uint)Avi.streamtypeVIDEO,
				lpParms = IntPtr.Zero,
				lpFormat = IntPtr.Zero
			};
			IntPtr streamPointer = StreamPointer;
			Avi.AVISaveOptions(IntPtr.Zero, Avi.ICMF_CHOOSE_KEYFRAME | Avi.ICMF_CHOOSE_DATARATE, 1, ref streamPointer, ref opts);
			Avi.AVISaveOptionsFree(1, ref opts);

			Avi.AVISaveV(fileName, 0, 0, 1, ref aviStream, ref opts);
		}
	}
}
