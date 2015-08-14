using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;

using LEDX.Utils;

namespace LEDX.Components {
	/// <summary>
	/// Interaction logic for Controller.xaml
	/// </summary>
	public partial class UiController {

		private double _plTim;
		public Model.Controller Contr { get { return (Model.Controller)DataContext; } }

		public double PlayTime {
			get {
				try {
					_plTim = sBrd.GetCurrentTime(this).GetValueOrDefault().TotalSeconds;
				} catch {
				}
				return _plTim;
			}
		}

		public UiController() {
			InitializeComponent();
		}

		private void spContr_PreviewMouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e) {
			Selectors.SelController(Contr);
		}

		private void spContr_Loaded(object sender, RoutedEventArgs e) {
			Contr.AnimationChanged += Contr_AnimationChanged;
			Contr.PlayPauseAnimation += Contr_AnimationPlay;
			Contr.AnimationSeek += Contr_AnimationSeek;
			Contr.PlayTimeCheck += Contr_PlayTimeCheck;
			Contr.ModeChanged += Contr_ModeChanged;
		}

		private void Contr_ModeChanged(object sender, EventArgs e) {
			Model.Controller.Modes md = (Model.Controller.Modes)sender;
			switch (md) {
				case Model.Controller.Modes.Edit:
					btAdd.Visibility = Visibility.Visible;
					tgbSel.Visibility = Visibility.Collapsed;
					break;
				case Model.Controller.Modes.Draw:
					btAdd.Visibility = Visibility.Collapsed;
					tgbSel.Visibility = Visibility.Collapsed;
					break;
				case Model.Controller.Modes.Flash:
					btAdd.Visibility = Visibility.Collapsed;
					tgbSel.Visibility = Visibility.Visible;
					break;
				default:
					throw new Exception("Invalid value for Mode");
			}
		}

		#region анимация
		private void Contr_AnimationChanged(object sender, EventArgs e) {
			double maxSeconds = Contr.MaxLength;
			clAni.KeyFrames.Clear();
			clAni.Duration = TimeSpan.FromSeconds(maxSeconds);
			Model.Controller cn = Contr;
			if (cn.Frames.Count > 0) {
				//double endTime = MaxSeconds - cn.Length;
				Model.Frame frm = cn.Frames[0];
				clAni.KeyFrames.Add(GetFrame(frm.BegColor, 0));
				double offs = 0;
				for (int i = 0; i < cn.Frames.Count; i++) {
					frm = cn.Frames[i];
					if (i > 0 && cn.Frames[i - 1].EndColor != frm.BegColor)
						clAni.KeyFrames.Add(GetFrame(frm.BegColor, offs));
					offs += frm.Length;
					clAni.KeyFrames.Add(GetFrame(frm.EndColor, offs));
				}
				//frm = cn.Frames[cn.Frames.Count - 1];
				clAni.KeyFrames.Add(GetFrame(Colors.Black, offs));
			} else {
				clAni.KeyFrames.Add(GetFrame(Colors.Black, 0));
				clAni.KeyFrames.Add(GetFrame(Colors.Black, maxSeconds));
			}
			TimeSpan prg = TimeSpan.FromSeconds(MWin.I.sldTime.Value);
			sBrd.Begin(this, true);
			if (!PlayerUtil.I.IsPlay)
				sBrd.Pause(this);
			sBrd.Seek(this, prg, TimeSeekOrigin.BeginTime);
		}

		private LinearColorKeyFrame GetFrame(Color cl, double time) {
			return new LinearColorKeyFrame(cl, KeyTime.FromTimeSpan(TimeSpan.FromSeconds(time)));
		}

		private void Contr_AnimationSeek(object sender, EventArgs e) {
			sBrd.SeekAlignedToLastTick(this, TimeSpan.FromSeconds((double)sender), TimeSeekOrigin.BeginTime);
		}

		private void Contr_PlayTimeCheck(object sender, EventArgs e) {
			Contr.PlayTime = PlayTime;
		}

		private void Contr_AnimationPlay(object sender, EventArgs e) {
			if (sender == null) {
				sBrd.Pause(this);
				Contr_AnimationSeek(.0, null);
				return;
			}
			bool st = (bool)sender;
			if (st)
				sBrd.Resume(this);
			else
				sBrd.Pause(this);
		}

		private void SpContr_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e) {
			Contr_AnimationChanged(null, null);
			IsVisibleChanged -= SpContr_IsVisibleChanged;
		}
		#endregion
	}
}
