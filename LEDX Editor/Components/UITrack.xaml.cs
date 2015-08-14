using System;
using System.Windows;
using System.Windows.Input;

using LEDX.Utils;

namespace LEDX.Components {
	/// <summary>
	/// Interaction logic for Track.xaml
	/// </summary>
	public partial class UiTrack {

		//private StackPanel _spn;
		private Model.Controller _con;

		public UiTrack() {
			InitializeComponent();
		}

		private void spTrack_PreviewMouseDown(object sender, MouseButtonEventArgs e) {
			Model.Controller cn = (Model.Controller)DataContext;
			if (!cn.IsSelected)
				Selectors.SelController((Model.Controller)DataContext);
		}

		private void spFrames_SizeChanged(object sender, SizeChangedEventArgs e) {
			if (e.WidthChanged && _con != null) {
				_con.Width = e.NewSize.Width;
				e.Handled = true;
			}
		}

		private void ItemsControl_Loaded(object sender, RoutedEventArgs e) {
			//_spn = (StackPanel)sender;
			_con = (Model.Controller)DataContext;
			_con.ModeChanged += Contr_ModeChanged;
		}

		private void Contr_ModeChanged(object sender, EventArgs e) {
			Model.Controller.Modes md = (Model.Controller.Modes)sender;
			IsEnabled = md == Model.Controller.Modes.Edit;
		}

	}
}
