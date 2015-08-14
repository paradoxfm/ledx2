using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using fx = LEDX.Utils.Flash;

namespace LEDX.Utils {
	public class FlashUtil {
		static readonly BackgroundWorker RefrContr = new BackgroundWorker();
		static readonly BackgroundWorker Flh = new BackgroundWorker();

		static FlashUtil() {
			RefrContr.WorkerReportsProgress = true;
			RefrContr.DoWork += refrContr_DoWork;
			RefrContr.RunWorkerCompleted += refrContr_RunWorkerCompleted;
			RefrContr.ProgressChanged += ProgressChanged;
			Flh.WorkerReportsProgress = true;
			Flh.DoWork += flh_DoWork;
			Flh.RunWorkerCompleted += flh_RunWorkerCompleted;
			Flh.ProgressChanged += ProgressChanged;
		}

		private static void ProgressChanged(object sender, ProgressChangedEventArgs e) {
			string msg = e.UserState.ToString();
			Logging.Log.Write(msg);
		}

		#region обновление списка контроллеров
		public static void RefreshControllers() {
			if (fx.Util.Worker != null && fx.Util.Worker.IsBusy) {
				MessageBox.Show("Выполняется другая операция", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			Model.FlashSettings sets = MWin.I.Doc.Flash;
			RefrContr.RunWorkerAsync(sets.Speed);
		}

		private static void refrContr_DoWork(object sender, DoWorkEventArgs e) {
			fx.Util.Worker = (BackgroundWorker)sender;
			e.Result = fx.Util.Referesh((int)e.Argument);
		}

		private static void refrContr_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			MWin.I.cbComName.ItemsSource = (string[])e.Result;
			MWin.I.cbComName.Items.Refresh();
			if (MWin.I.cbComName.Items.Count > 0)
				MWin.I.cbComName.SelectedIndex = 0;
			MWin.I.spProgress.Visibility = Visibility.Collapsed;
			Logging.Log.Write("Обновлен список контроллеров");
		}
		#endregion

		#region прошивка
		public static void FlashControllers() {
			if (fx.Util.Worker != null && fx.Util.Worker.IsBusy) {
				MessageBox.Show("Выполняется другая операция", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			Model.FlashSettings sets = MWin.I.Doc.Flash;
			List<Model.Controller> con = GetControllers();
			if (con.Count == 0) {
				MessageBox.Show("Необходимо выбрать контроллеры", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			if (sets.Port == null) {
				MessageBox.Show("Необходимо выбрать порт для прошивки", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			object[] ar = { con, sets };
			MWin.I.spProgress.Visibility = Visibility.Visible;
			Flh.RunWorkerAsync(ar);
		}

		private static List<Model.Controller> GetControllers() {
			return MWin.I.Doc.Controller.Where(cn => cn.IsFlash).ToList();
		}

		private static void flh_DoWork(object sender, DoWorkEventArgs e) {
			object[] ar = (object[])e.Argument;
			List<Model.Controller> con = (List<Model.Controller>)ar[0];
			Model.FlashSettings sets = (Model.FlashSettings)ar[1];
			fx.Util.Flash(con, sets);
		}

		private static void flh_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
			MWin.I.spProgress.Visibility = Visibility.Collapsed;
			Logging.Log.Write("Прошивка завершена");
		}
		#endregion

		internal static void PowerController() {
			Model.FlashSettings sets = MWin.I.Doc.Flash;
			if (sets.Port == null) {
				MessageBox.Show("Необходимо выбрать порт для прошивки", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			fx.Util.Enable(sets);
		}
	}
}
