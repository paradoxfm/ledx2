using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace LEDX.Dialogs {
	/// <summary>
	/// Interaction logic for AboutDialog.xaml
	/// </summary>
	public partial class AboutDialog {

		private static readonly string TampDir = System.AppDomain.CurrentDomain.BaseDirectory + "Help";

		public AboutDialog() {
			InitializeComponent();
		}

		private void GoHelp(object sender, System.Windows.Input.MouseButtonEventArgs e) {
			string[] fils = null;
			if (Directory.Exists(TampDir))
				fils = Directory.GetFiles(TampDir, "user manual.pdf", SearchOption.TopDirectoryOnly);
			if (fils != null && fils.Length > 0)
				Process.Start(fils[0]);
			else
				MessageBox.Show("Руководство пользователя не установлено!");
		}

		private void GoSite(object sender, System.Windows.Input.MouseButtonEventArgs e) {
			Process.Start(((Label)sender).Tag.ToString());
		}
	}
}
