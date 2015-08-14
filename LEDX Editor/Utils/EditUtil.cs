using System;
using System.Collections.Generic;
using System.Linq;
using LEDX.Dialogs;
using LEDX.Model;
using LEDX.Properties;

namespace LEDX.Utils {
	public class EditUtil {

		public static bool IsAllowEdit { get; set; }

		static EditUtil() {
			IsAllowEdit = true;
		}

		public static void Help() {
			AboutDialog dlg = new AboutDialog { Owner = MWin.I };
			dlg.ShowDialog();
		}

		internal static bool Changed() {
			return MWin.I != null && MWin.I.Doc.Changed;
		}

		internal static bool CanUndo() {
			if (MWin.I == null)
				return false;
			if (Equals(MWin.I.ribbon.SelectedTabItem, MWin.I.tbPalyer))
				return true;
			return Equals(MWin.I.ribbon.SelectedTabItem, MWin.I.tbEdit) && MWin.I.Doc.CanUndo;
		}

		internal static bool CanRedo() {
			if (MWin.I != null)
				if (Equals(MWin.I.ribbon.SelectedTabItem, MWin.I.tbPalyer))
					return true;
				else if (Equals(MWin.I.ribbon.SelectedTabItem, MWin.I.tbEdit))
					return MWin.I.Doc.CanRedo;
			return false;
		}

		internal static void Undo() {
			if (Equals(MWin.I.ribbon.SelectedTabItem, MWin.I.tbEdit))
				MWin.I.Doc.Undo();
			//else if (MWin.I.ribbon.SelectedTabItem == MWin.I.tbPalyer)
			//  MWin.I.root.Undo();
		}

		internal static void Redo() {
			if (Equals(MWin.I.ribbon.SelectedTabItem, MWin.I.tbEdit))
				MWin.I.Doc.Redo();
			//else if (MWin.I.ribbon.SelectedTabItem == MWin.I.tbPalyer)
			//  MWin.I.root.Redo();
		}

		internal static void AddController(Controller controller) {
			if (!IsAllowEdit)
				return;
			Controller cn = new Controller { Number = GetIndexController(1, true) };
			cn.WidthChanged += cn_WidthChanged;
			MWin.I.Doc.Controller.Add(cn);
			Selectors.SelController(cn);
		}

		internal static void DelController(bool undo) {
			if (!IsAllowEdit)
				return;
			int ind = MWin.I.Doc.Controller.IndexOf(MWin.I.ActCont);
			MWin.I.Doc.Controller.Remove(MWin.I.ActCont);
			MWin.I.ActCont = null;
			MWin.I.ActFrm.Clear();
			if (MWin.I.Doc.Controller.Count - 1 >= ind)
				Selectors.SelController(MWin.I.Doc.Controller[ind]);
			else if (MWin.I.Doc.Controller.Count > 0)
				Selectors.SelController(MWin.I.Doc.Controller[ind - 1]);
			if (MWin.I.Doc.Controller.Count == 0)
				MWin.I.uiMan.IsPlay = false;
		}

		public static int GetIndexController(int start, bool up) {
			if (up) {
				for (int y = start; y < 100; y++)
					if (MWin.I.Doc.Controller.All(cn => y != cn.Number))
						return y;
			} else {
				for (int y = start; y > 0; y--)
					if (MWin.I.Doc.Controller.All(cn => y != cn.Number))
						return y;
			}
			return start;
		}

		internal static bool CanDelContr() {
			return MWin.I != null && MWin.I.ActCont != null;
		}

		internal static void AddFrame(object sender) {
			if (MWin.I.ActCont == null || !IsAllowEdit)
				return;
			Frame frm = MWin.I.ActCont.Frames.Count > 0 ? new Frame(MWin.I.ActCont.Frames[MWin.I.ActCont.Frames.Count - 1].EndColor) : new Frame();
			MWin.I.ActCont.Frames.Add(frm);
		}

		internal static void DelFrame() {
			if (!IsAllowEdit)
				return;
			MWin win = MWin.I;
			foreach (Frame fr in win.ActFrm) {
				fr.IsSelected = false;
				win.ActCont.Frames.Remove(fr);
			}
			win.ActFrm.Clear();
		}

		internal static void AddSample(object all) {
			if (!IsAllowEdit)
				return;
			Controller con = new Controller { Id = Guid.NewGuid() };
			Controller cn = MWin.I.ActCont;
			//List<Frame> frms = new List<Frame>();
			bool prm = bool.Parse(all.ToString());
			for (int i = 0; i < cn.Frames.Count; i++) {
				Frame frm = cn.Frames[i];
				if (prm || frm.IsSelected) {
					Frame fr = (Frame)frm.Clone();
					fr.Length = 1;
					con.Frames.Add(fr);
				}
			}
			//string path = FileOperations.SaveSample(con);
			List<Controller> sour = (List<Controller>)MWin.I.grSampl.ItemsSource;
			sour.Add(con);
			MWin.I.grSampl.Items.Refresh();
		}

		public static void InsertSample(object sampl) {
			if (!IsAllowEdit)
				return;
			Controller cntl = (Controller)MWin.I.grSampl.SelectedItem;
			List<Frame> frms = cntl.Frames.Select(t => (Frame)t.Clone()).ToList();
			int ind = MWin.I.ActCont.Frames.Count;
			if (MWin.I.ActFrm.Count > 0)
				ind = MWin.I.ActCont.Frames.IndexOf(MWin.I.ActFrm[MWin.I.ActFrm.Count - 1]) + 1;
			MWin.I.ActCont.InsertFrames(ind, frms);
			RefreshSeekBar();
		}

		internal static void DelSample() {
			if (MWin.I.grSampl.SelectedItem != null) {
				Controller cn = (Controller)MWin.I.grSampl.SelectedItem;
				List<Controller> sour = (List<Controller>)MWin.I.grSampl.ItemsSource;
				sour.Remove(cn);
				MWin.I.grSampl.Items.Refresh();
				FileOperations.DeleteSample(cn.Id.ToString());
			}
		}

		internal static void UpgradeDrawSettings() {
			Settings.Default.Save();
			Settings.Default.Upgrade();
			IList<Controller> cns = MWin.I.Doc.Controller;
			foreach (Controller c in cns)
				foreach (Frame fr in c.Frames)
					fr.UpdateStyle();
		}

		internal static void cn_WidthChanged(object sender, EventArgs e) {
			IList<Controller> cns = MWin.I.Doc.Controller;
			double wid = cns.Select(t => t.Width).Concat(new double[] { 0 }).Max();
			MWin.I.sldTime.Width = wid;
		}

		internal static void RefreshSeekBar() {
			IList<Controller> cns = MWin.I.Doc.Controller;
			double len = cns.Select(t => t.Length).Concat(new double[] { 0 }).Max();
			MWin.I.sldTime.Maximum = len;
			foreach (Controller c in cns)
				c.MaxLength = len;
		}

		internal static void ReplaceFrame(Frame draged, Frame trackFrame, bool after) {
			int oldInd = MWin.I.ActCont.Frames.IndexOf(draged);
			int newInd = MWin.I.ActCont.Frames.IndexOf(trackFrame);
			if (oldInd < newInd)
				newInd += after ? 0 : -1;
			else
				newInd += after ? 1 : 0;
			if (newInd != oldInd)
				MWin.I.ActCont.Frames.Move(oldInd, newInd);
		}

		internal static void SetScale(double x) {
			if (MWin.I == null)
				return;
			Converters.WidtToLength.Scale = x;
			IList<Controller> cns = MWin.I.Doc.Controller;
			foreach (Controller c in cns)
				foreach (Frame fr in c.Frames)
					fr.UpdateScale();
		}

		internal static void SetScaleDefault() {
			MWin.I.uiScaleSlider.Value = 1;
		}

		internal static void Error() {
			throw new Exception("Тестовая ошибка для проверки!");
		}
	}
}
