using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO.Ports;
using System.Linq;
using System.Windows.Media;

using LEDX.Model;

namespace LEDX.Utils.Flash {

	public class Util {

		//private const int READ_TIMEOUT = 50;

		private static readonly SerialPort SerialPort = new SerialPort();
		private static readonly SerialPort ColorPort = new SerialPort();
		private static byte[] _colorArr;
		private static byte _baseSumm;

		public static bool IsConfigured { get; set; }
		public static BackgroundWorker Worker { get; set; }

		#region service
		static Util() {
			ColorPort.ReadTimeout = 50;
			SerialPort.ReadTimeout = 200;
			IsConfigured = false;
			InitColorSend();
		}

		private static void ClosePort() {
			if (SerialPort.IsOpen)
				SerialPort.Close();
		}

		private static void OpenPort() {
			if (!SerialPort.IsOpen) {
				if (ColorPort.IsOpen)
					ColorPort.Close();
				SerialPort.Open();
				SerialPort.DiscardInBuffer();
			}
		}

		private static byte[] ReadAswer() {
			SerialPort.DiscardInBuffer();
			byte[] bt = new byte[8];
			try {
				for (int i = 0; i < 8; i++)
					SerialPort.Read(bt, i, 1);
			} catch {
				return null;
			}
			return bt;
		}

		private static bool SendData(byte[] dat) {
			try {
				SerialPort.Write(dat, 0, dat.Length);
				return true;
			} catch {
				return false;
			}
		}

		private static void SendDataColor(byte[] dat) {
			try {
				ColorPort.Write(dat, 0, dat.Length);
			} catch {
			}
		}

		public static byte GetSumm(List<byte> arr) {
			int rez = arr.Sum(e => (int)e);
			return Convert.ToByte(rez & 0xFF);
		}

		private static void SetupPort(string name, int baudRate) {
			SerialPort.BaudRate = baudRate;
			SerialPort.PortName = name;
		}

		private static void SetupColorPort(string name, int baudRate) {
			ColorPort.BaudRate = baudRate;
			ColorPort.PortName = name;
			if (!ColorPort.IsOpen)
				ColorPort.Open();
			IsConfigured = true;
		}
		#endregion

		#region прошивка
		public static void Flash(List<Controller> con, FlashSettings sets) {
			SetupPort(sets.Port, sets.Speed);
			double synLen = GetSyncLength(con, sets.IsSync);
			foreach (Controller c in con) {
				if (c.Frames.Count == 0) {
					Worker.ReportProgress(0, "Контроллер " + c.Number + ':' + c.Button + " не содержит фреймов и пропущен.");
					continue;
				}
				EnterModeProgramming(c.Number, c.Button);
				Logging.Log.Write("Включен режим программирования");
				bool wErr = false;
				try {
					double frLen = 0;
					foreach (Frame f in c.Frames) {
						frLen += f.Length;
						if (!SendFrame(c.Number, f))
							wErr = true;
					}
					if (frLen < synLen) {
						Frame fr = new Frame { BegColor = Colors.Black, EndColor = Colors.Black, Length = synLen - frLen };
						SendFrame(c.Number, fr);
					}
					Logging.Log.Write("Фреймы отправлены");
				} catch (Exception e) {
					Logging.Log.Write(e.Message);
					Logging.Log.Write(e.StackTrace);
				} finally {
					ExitModeProgramming(c.Number);
					Logging.Log.Write("Выключен режим программирования");
				}
				if (wErr)
					Worker.ReportProgress(0, "Контроллер " + c.Number + ':' + c.Button + " прошит с ошибками.");
			}
		}

		private static double GetSyncLength(IEnumerable<Controller> con, bool syn) {
			if (!syn)
				return -1;
			return con.Select(t1 => t1.Frames.Sum(t => t.Length)).Concat(new double[] { 0 }).Max();
		}

		private static void EnterModeProgramming(int controller, int button) {
			OpenPort();
			List<byte> arr = new List<byte> {
				FEdge.Start,
				FType.Req,
				(byte) controller,
				FCmd.EnterProgMode,
				1,
				(byte) (button - 1)
			};
			arr.Add(GetSumm(arr));
			arr.Add(FEdge.Stop);
			SendData(arr.ToArray());
			ReadAswer();
		}

		private static void ExitModeProgramming(int controller) {
			List<byte> arr = new List<byte> { FEdge.Start, FType.Req, (byte)controller, FCmd.FinilizeProg, 0 };
			arr.Add(GetSumm(arr));
			arr.Add(FEdge.Stop);
			SendData(arr.ToArray());
			ReadAswer();
			ClosePort();
		}

		private static bool SendFrame(int controller, Frame frm) {
			List<byte> arr = new List<byte> { FEdge.Start, FType.Req, (byte)controller, FCmd.WriteFrame };
			List<byte> f = CreateData(frm);
			arr.Add((byte)f.Count);
			arr.AddRange(f);
			arr.Add(GetSumm(arr));
			arr.Add(FEdge.Stop);
			bool fine = SendData(arr.ToArray());
			byte[] rd = ReadAswer();
			if (rd == null)
				fine = false;
			return fine;
		}

		private static List<byte> CreateData(Frame frm) {
			List<byte> rez = new List<byte>();
			byte[] len = BitConverter.GetBytes((ushort)(frm.Length * 100));
			rez.Add(len[0]);//TIME_L
			rez.Add(len[1]);//TIME_H
			rez.Add(frm.BegColor.R);//R1
			rez.Add(frm.BegColor.G);//G1
			rez.Add(frm.BegColor.B);//B1
			rez.Add(frm.EndColor.R);//R2
			rez.Add(frm.EndColor.G);//G2
			rez.Add(frm.EndColor.B);//B2
			return rez;
		}
		#endregion

		#region влк/выкл
		public static void Enable(FlashSettings sets) {
			if (!IsConfigured)
				SetupColorPort(sets.Port, sets.Speed);
			EnDis();
		}

		private static void EnDis() {
			List<byte> arr = new List<byte> { FEdge.Start, FType.Req, 1, FCmd.Power, 0 };
			arr.Add(GetSumm(arr));
			arr.Add(FEdge.Stop);
			if (!ColorPort.IsOpen)
				ColorPort.Open();
			SendDataColor(arr.ToArray());
		}
		#endregion

		#region список портов
		public static string[] Referesh(int baudRate) {
			List<string> rez = new List<string>();
			string[] ports = SerialPort.GetPortNames();
			SerialPort.BaudRate = baudRate;
			foreach (string s in ports) {
				SerialPort.PortName = s;
				if (CheckPort())
					rez.Add(s);
			}
			return rez.ToArray();
		}

		private static bool CheckPort() {
			List<byte> arr = new List<byte> { FEdge.Start, FType.Req, 1, FCmd.Echo, 0 };
			arr.Add(GetSumm(arr));
			arr.Add(FEdge.Stop);

			OpenPort();
			SendData(arr.ToArray());
			bool rez = ReadAswer() != null;
			ClosePort();
			return rez;
		}
		#endregion

		public static void SendColor(byte r, byte g, byte b) {
			if (!IsConfigured)
				return;
			if (!ColorPort.IsOpen)
				ColorPort.Open();
			_colorArr[5] = r;
			_colorArr[6] = g;
			_colorArr[7] = b;
			_colorArr[8] = Convert.ToByte((_baseSumm + _colorArr[5] + _colorArr[6] + _colorArr[7]) & 0xFF);
			SendDataColor(_colorArr);
		}

		private static void InitColorSend() {
			List<byte> arr = new List<byte> { FEdge.Start, FType.Req, 1, FCmd.SetColor, 3, 0, 0, 0 };
			_baseSumm = GetSumm(arr);
			arr.Add(_baseSumm);
			arr.Add(FEdge.Stop);
			_colorArr = arr.ToArray();
		}
	}
}
