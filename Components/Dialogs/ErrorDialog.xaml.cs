using System;
using System.Diagnostics;
using System.Windows;
using Fluent;

namespace LEDX.Dialogs {
	/// <summary>
	/// Interaction logic for ErrorDialog.xaml
	/// </summary>
	public partial class ErrorDialog : RibbonWindow {

		public ErrorDialog() {
			InitializeComponent();
		}

		public ErrorDialog(Exception e) {
			InitializeComponent();
			tbErr.Text = e.Message + "\r\n" + e.StackTrace;
		}

		private void Button_Click(object sender, RoutedEventArgs e) {
			DialogResult = true;
			Process.Start("https://code.google.com/p/ledx2/issues/list");
			tbErr.SelectAll();
			Clipboard.SetText(tbErr.Text);
		}

		private void Button_Click_1(object sender, RoutedEventArgs e) {
			DialogResult = false;
		}
	}
}
