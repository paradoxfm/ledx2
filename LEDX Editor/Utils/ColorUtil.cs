using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

using LEDX.Components.ColorPicker;

namespace LEDX.Utils {
	public class ColorUtil {

		private static Dialogs.ColorDialog _dlg;

		internal static void SetSolidColor() {
			Model.Frame frm = MWin.I.ActFrm[0];
			if (Keyboard.Modifiers == ModifierKeys.Control)
				frm.BegColor = frm.EndColor;
			else
				frm.EndColor = frm.BegColor;
			frm.Root.UpdateAnimation();
		}

		internal static void SetLrColor() {
			Model.Frame frm = MWin.I.ActFrm[0];
			Model.Controller con = frm.Root;
			int ind = con.Frames.IndexOf(frm);
			Color b = frm.BegColor;
			Color e = frm.EndColor;
			if (ind != 0)
				b = con.Frames[ind - 1].EndColor;
			if (ind < con.Frames.Count - 1)
				e = con.Frames[ind + 1].BegColor;
			frm.SetColors(b, e);
			frm.Root.UpdateAnimation();
		}

		internal static void SetColor(bool end) {
			Model.Frame frm = MWin.I.ActFrm[0];
			_dlg = new Dialogs.ColorDialog();
			_dlg.SetSelectedColors(frm.BegColor, frm.EndColor, !end);
			_dlg.SelectedColorChanged += ColorChange;
			bool rez = _dlg.ShowDialog().GetValueOrDefault();
			if (rez) {
				frm.SetColors(_dlg.SelectedColorStart, _dlg.SelectedColorEnd);
				MWin.I.ActFrm[0].Root.UpdateAnimation();
			}
		}

		internal static void ColorChange(object sender, EventArgs<Color> e) {
			if (!_dlg.IsAllowColorChange)
				return;			
			if (!Flash.Util.IsConfigured) {
				_dlg.SelectedColorChanged -= ColorChange;
				MessageBox.Show("Не настроен порт контроллера.\nЧтобы настроить порт для предросмотра\n"+
				"необходимо настроить его в меню прошивка и включить.\nЕсли он уже включен, то нажать 2 раза включить", "Внимание!", MessageBoxButton.OK, MessageBoxImage.Warning);
				return;
			}
			Flash.Util.SendColor(e.Value.R, e.Value.G, e.Value.B);
		}

	}
}
