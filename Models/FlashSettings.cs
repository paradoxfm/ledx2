namespace LEDX.Model {
	/// <summary>
	/// Description of FlashSettings.
	/// </summary>
	public class FlashSettings : BaseModel {
		
		private bool _issync;
		private int _speed = 115200;
		private string _port;
		
		public bool IsSync { get { return _issync;}
			set{
				if (value == _issync)
					return;
				_issync = value;
				OnPropertyChanged("IsSync");
			}
		}
		
		public int Speed { get { return _speed; }
			set{
				if (value == _speed)
					return;
				_speed = value;
				OnPropertyChanged("Speed");
			}
		}
		
		public string Port { get { return _port; }
			set{
			if (value == _port)
					return;
				_port = value;
				OnPropertyChanged("Port");
			}
		}
	}
}
