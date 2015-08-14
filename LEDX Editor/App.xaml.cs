using System;
using System.IO;
using System.Windows;
using System.Windows.Threading;

using LEDX.Dialogs;
using LEDX.Properties;

namespace LEDX {
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App {

		const string ThemPath = "pack://application:,,,/Fluent;component/Themes/Office2010/{0}.xaml";

		public static string OpenFile { get; set; }

		private void Application_Startup(object sender, StartupEventArgs e) {
			DispatcherUnhandledException += App_DispatcherUnhandledException;
			SetTheme(Settings.Default.Theme, false);
			Settings.Default.Develop = Settings.Default.TestMode = false;
			Settings.Default.ShowLog = false;
			foreach (string arg in e.Args) {
				if (OpenFile == null && File.Exists(arg))
					OpenFile = arg;
				if (arg.ToLower().Contains("testmode"))
					Settings.Default.TestMode = true;
				if (arg.ToLower().Contains("develop"))
					Settings.Default.Develop = true;
			}
		}

		private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) {
			new ErrorDialog(e.Exception).ShowDialog();
			e.Handled = true;
		}

		public static void SetTheme(object res) {
			if (res != null)
				SetTheme(res.ToString(), true);
		}

		private static void SetTheme(string res, bool replace) {
			if (replace && Settings.Default.Theme == res)
				return;
			string path = string.Format(ThemPath, res);
			Current.Resources.BeginInit();
			Current.Resources.MergedDictionaries.Add(new ResourceDictionary { Source = new Uri(path) });
			if (replace) {
				Current.Resources.MergedDictionaries.Insert(Current.Resources.MergedDictionaries.Count - 1,
					new ResourceDictionary { Source = new Uri(path) });
				Current.Resources.MergedDictionaries.RemoveAt(Current.Resources.MergedDictionaries.Count - 1);
			}
			Current.Resources.EndInit();
			if (replace) {
				Settings.Default.Theme = res;
				Settings.Default.Save();
				Settings.Default.Upgrade();
			}
		}

	}
}
