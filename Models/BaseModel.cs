using System.ComponentModel;

namespace LEDX.Model {
	public abstract class BaseModel : INotifyPropertyChanged {
		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;

		#endregion

		protected void OnPropertyChanged(string propertyName) {
			if (null != PropertyChanged)
				PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
