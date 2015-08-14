using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;

namespace LEDX.Utils {
	public class ClipboardUtil {
		private static object _data;

		public static void CopyFrame() {
			if (MWin.I.ActFrm.Count == 0)
				return;
			List<Model.Frame> dat = MWin.I.ActCont.Frames.Where(f => f.IsSelected).Select(f => (Model.Frame)f.Clone()).ToList();
			_data = dat;
		}

		public static void CutFrame() {
			CopyFrame();
			EditUtil.DelFrame();
		}

		public static void CopyColor() {
			Model.Frame fr = MWin.I.ActFrm[0];
			_data = new[] { fr.BegColor, fr.EndColor };
		}

		internal static void PasteObject() {
			MWin win = MWin.I;
			if (_data.GetType() == typeof(Color[])) {
				Color[] col = (Color[])_data;
				win.ActFrm[0].SetColors(col[0], col[1]);
			} else if (_data.GetType() == typeof(List<Model.Frame>)) {
				win.ActCont.InsertFrames(GetInsertPosition(false), (List<Model.Frame>)_data);
				EditUtil.RefreshSeekBar();
			}
		}

		internal static void PasteBefore() {
			MWin.I.ActCont.InsertFrames(GetInsertPosition(true), (List<Model.Frame>)_data);
			EditUtil.RefreshSeekBar();
		}

		private static int GetInsertPosition(bool before) {
			//int posEnd = MWin.I.ActCont.Frames.Count;
			for (int i = MWin.I.ActCont.Frames.Count - 1; i > -1; i--)
				if (MWin.I.ActCont.Frames[i].IsSelected)
					return i + (before ? 0 : 1);
			return MWin.I.ActCont.Frames.Count;
		}

		public static bool CanPaste(bool before) {
			if (_data == null)
				return false;
			if (!before) {
				if (_data.GetType() == typeof(Color[]) && MWin.I.ActFrm.Count == 1)
					return true;
				if (_data.GetType() == typeof(List<Model.Frame>) && (MWin.I.ActFrm.Count > 0 || MWin.I.ActCont != null))
					return true;
			} else {
				if (_data.GetType() == typeof(List<Model.Frame>) && MWin.I.ActFrm.Count > 0)
					return true;
			}
			return false;
		}

	}
}
