using System.Linq;
using System.Windows.Input;
using LEDX.Components;
using LEDX.Model;

namespace LEDX.Utils {
	public static class Selectors {

		public static void SelOneFrame(Frame sender) {
			MWin win = MWin.I;
			if (win.ActFrm.Count == 1 && win.ActFrm[0] == sender)
				return;
			foreach (Frame frme in win.ActFrm)
				frme.IsSelected = false;
			win.ActFrm.Clear();
			sender.IsSelected = true;
			win.ActFrm.Add(sender);
			win.rgbColorProp.DataContext = win.rgbFrameProp.DataContext = sender;
		}

		public static void SelShiftFrame(object sender) {
			int minInd = 1000, maxInd = 0;
			MWin win = MWin.I;
			for (int i = 0; i < win.ActCont.Frames.Count; i++)
				if (win.ActCont.Frames[i].IsSelected) {
					minInd = i < minInd ? i : minInd;
					maxInd = i > maxInd ? i : maxInd;
				}
			Frame fr = (Frame)sender;
			int ind = win.ActCont.Frames.IndexOf(fr);
			if (ind < 0) {
				SelOneFrame(fr);
				return;
			}
			if (ind < minInd)
				SelectFromTo(ind, minInd);
			else if (ind > maxInd)
				SelectFromTo(maxInd, ind);
		}

		private static void SelectFromTo(int from, int to) {
			for (int i = from; i <= to; i++)
				if (!MWin.I.ActCont.Frames[i].IsSelected)
					SelMultiFrame(MWin.I.ActCont.Frames[i], false);
			CalcLength();
		}

		public static void SelMultiFrame(object sender, bool recalc) {
			Frame frm = (Frame)sender;
			MWin win = MWin.I;
			if (win.ActFrm.Count > 0 && win.ActFrm[0].Root != frm.Root) {
				foreach (Frame frme in win.ActFrm)
					frme.IsSelected = false;
				win.ActFrm.Clear();
			}
			if (frm.IsSelected)
				win.ActFrm.Remove(frm);
			else
				win.ActFrm.Add(frm);
			frm.IsSelected = !frm.IsSelected;
			win.rgbColorProp.DataContext = win.rgbFrameProp.DataContext = win.ActFrm.Count == 1 ? win.ActFrm[0] : null;
			if (recalc)
				CalcLength();
		}

		public static void CalcLength() {
			MWin win = MWin.I;
			double len = win.ActFrm.Sum(t => t.Length);
			win.spTime.Value = len;
		}

		public static void EnterTrackFrame(object sender) {
			UiFrame frm = (UiFrame)sender;
			Frame mod = frm.Frm;
			SelOneFrame(mod);
			ColorUtil.SetColor((frm.ActualWidth / 2) < Mouse.GetPosition(frm).X);
		}

		public static void SelController(Controller sender) {
			MWin win = MWin.I;
			if (win.ActCont == sender)
				return;
			for (int i = win.ActFrm.Count - 1; i > -1; i--)
				if (!sender.Frames.Contains(win.ActFrm[i])) {
					win.ActFrm[i].IsSelected = false;
					win.ActFrm.Remove(win.ActFrm[i]);
				}
			if (win.ActCont != null)
				win.ActCont.IsSelected = false;
			sender.IsSelected = true;
			win.ActCont = sender;
		}
	}
}
