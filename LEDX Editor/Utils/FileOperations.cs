using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Media.Imaging;
using System.Xml.Serialization;

using Ionic.Zip;

namespace LEDX.Utils {
	public class FileOperations {

		private const string Filter = "LEDX editor-file (.ledx)|*.ledx";
		public const string FilterPng = "PNG изображения (.png)|*.png";
		private const string FilterJpg = "JPG изображения (.jpg)|*.jpg";
		public const string FilterAvi = "Видео файлы (.avi)|*.avi";
		private const string Ext = ".ledx";
		//private const string Extt = ".ledt";
		private static readonly string TampDir = AppDomain.CurrentDomain.BaseDirectory + "Tamplates\\";

		public static void NewFile() {
			if (MWin.I.Doc.Changed) {
				MessageBoxResult rez = MessageBox.Show("Документ не сохранен!\nВы хотие сохранить его перед открытием другого?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning);
				if (rez == MessageBoxResult.Yes)
					SaveFile();
			}
			MWin.I.Doc = new Model.Document();
		}

		public static void OpenFile() {
			MWin.I.ribbon.SelectedTabIndex = 0;
			if (MWin.I.Doc.Changed) {
				MessageBoxResult rez = MessageBox.Show("Документ не сохранен!\nВы хотие сохранить его перед открытием другого?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning);
				if (rez == MessageBoxResult.Yes)
					SaveFile();
			}
			//if ((bool)MWin.I.conMan.btPlay.IsChecked) {
			//  MWin.I.conMan.btPlay.IsChecked = false;
			//  PlayerUtil.PlayerStop();
			//}
			string path = ShowOpenDialog();
			OpenFile(path);
		}

		public static void OpenFile(string path) {
			if (!string.IsNullOrEmpty(path)) {
				Model.Document dc = ConvertXml<Model.Document>(path);
				if (dc == null)
					return;
				MWin.I.Doc = dc;
				dc.OnChange += MWin.I.Doc_OnChange;
				MWin.I.Doc.Path = path;
				dc.Clear();
				EditUtil.RefreshSeekBar();
				EditUtil.cn_WidthChanged(null, null);
				Logging.Log.Write("Открыт файл - " + path);
				MWin.I.Title = "LEDX редактор  " + path;
			}
		}

		public static string ShowOpenDialog() {
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog {
				Title = "Открыть файл",
				DefaultExt = Ext,
				Filter = Filter,
				CheckFileExists = true
			};
			if (dlg.ShowDialog() == true)
				return dlg.FileName;
			return null;
		}

		public static BitmapImage OpenBackgroundImage() {
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog {
				Title = "Открыть файл",
				DefaultExt = ".jpg",
				Filter = FilterJpg + '|' + FilterPng,
				CheckFileExists = true
			};
			if (dlg.ShowDialog() == true)
				return OpenFromFile(dlg.FileName);
			return null;
		}

		public static BitmapImage OpenLigthImage() {
			Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog {
				Title = "Открыть файл",
				DefaultExt = ".png",
				Filter = FilterPng,
				CheckFileExists = true
			};
			if (dlg.ShowDialog() == true)
				return OpenFromFile(dlg.FileName);
			return null;
		}

		public static BitmapImage OpenFromFile(string path) {
			BitmapImage image = new BitmapImage();
			using (FileStream stream = File.OpenRead(path)) {
				image.BeginInit();
				image.StreamSource = stream;
				image.CacheOption = BitmapCacheOption.OnLoad;
				image.EndInit(); // load the image from the stream
			}
			return image;
		}

		public static void SaveFile() {
			if (string.IsNullOrEmpty(MWin.I.Doc.Path))
				MWin.I.Doc.Path = ShowSaveDialog("Сохранить", Ext, "Firmware", Filter);
			if (string.IsNullOrEmpty(MWin.I.Doc.Path))
				return;
			WriteFile(MWin.I.Doc, MWin.I.Doc.Path);
			MWin.I.Doc.Changed = false;
		}

		public static void SaveAsFile() {
			MWin.I.Doc.Path = ShowSaveDialog("Сохранить как", Ext, "Firmware", Filter);
			if (!string.IsNullOrEmpty(MWin.I.Doc.Path)) {
				WriteFile(MWin.I.Doc, MWin.I.Doc.Path);
				MWin.I.Doc.Changed = false;
			}
		}

		public static string ShowSaveDialog(string title, string ext, string filename, string filter) {
			Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog {
				Title = title,
				DefaultExt = ext,
				Filter = filter,
				AddExtension = true,
				FileName = filename
			};
			if (dlg.ShowDialog() == true)
				return dlg.FileName;
			return null;
		}

		private static void WriteFile(object doc, string path) {
			try {
				XmlSerializer xmlSerializer = new XmlSerializer(doc.GetType());
				using (MemoryStream strm = new MemoryStream()) {
					xmlSerializer.Serialize(strm, doc);
					strm.Position = 0;
					using (ZipFile zip = new ZipFile()) {
						zip.AddEntry("firmware.bin", strm);
						zip.Save(path);
					}
				}
				Logging.Log.Write("Файл сохранен - " + path);
			} catch {
				Logging.Log.Write("Ошибка сохранения файла - " + path);
			}
		}

		public static T ConvertXml<T>(string path) {
			XmlSerializer serializer = new XmlSerializer(typeof(T));
			object rez;
			using (MemoryStream strm = new MemoryStream()) {
				try {
					using (ZipFile zip = ZipFile.Read(path)) {
						zip["firmware.bin"].Extract(strm);
					}
				} catch {
					Logging.Log.Write("Ошибка чтения файла - " + path);
				}
				strm.Seek(0, SeekOrigin.Begin);//.Position = 0;
				try {
					rez = serializer.Deserialize(strm);
				} catch {
					Logging.Log.Write("Файл поврежден - " + path);
					rez = null;
				}
			}
			return (T)rez;
		}

		public static string SaveSample(Model.Controller con) {
			string path = TampDir + con.Id + ".ledt";
			if (!Directory.Exists(TampDir))
				Directory.CreateDirectory(TampDir);
			WriteFile(con, path);
			return path;
		}

		public static List<Model.Controller> OpenSamples() {
			List<Model.Controller> conts = new List<Model.Controller>();
			if (Directory.Exists(TampDir)) {
				string[] templ = Directory.GetFiles(TampDir, "*.ledt", SearchOption.TopDirectoryOnly);
				conts.AddRange(templ.Select(ConvertXml<Model.Controller>).Where(cn => cn != null));
			}
			return conts;
		}

		public static void DeleteSample(string id) {
			string path = TampDir + id + ".ledt";
			if (File.Exists(path))
				File.Delete(path);
		}

		/*private static string GetName(string path) {
			int pos = path.LastIndexOf('\\') + 1;
			return path.Substring(pos, path.LastIndexOf('.') - pos);
		}*/
	}
}
