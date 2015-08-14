using System;
using System.Windows.Media;
using System.Xml.Serialization;

namespace LEDX.Model {
	public class Frame : BaseModel, UndoRedo.ISupportsUndo, ICloneable {

		private bool _selected;
		private double _length;
		private Color _begColor;
		private Color _endColor;
		private Controller _root;
		private UndoRedo.UndoBatch _bth;
		public event EventHandler StyleChanged;

		public Frame() {
			_length = 1;
			_begColor = Colors.Black;
			_endColor = Colors.White;
		}

		public Frame(Color begColor)
			: this() {
			_begColor = begColor;
		}

		private Frame(Color begColor, Color endColor, double length) {
			_begColor = begColor;
			_endColor = endColor;
			_length = length;
		}

		#region prop
		public Color BegColor {
			get { return _begColor; }
			set {
				if (value == _begColor)
					return;
				UndoRedo.DefaultChangeFactory.OnChanging(this, "BegColor", _begColor, value);
				_begColor = value;
				OnPropertyChanged("BegColor");
			}
		}

		public Color EndColor {
			get { return _endColor; }
			set {
				if (value == _endColor)
					return;
				UndoRedo.DefaultChangeFactory.OnChanging(this, "EndColor", _endColor, value);
				_endColor = value;
				OnPropertyChanged("EndColor");
			}
		}

		public double Length {
			get { return _length; }
			set {
				if (value == _length)
					return;
				UndoRedo.DefaultChangeFactory.OnChanging(this, "Length", _length, value);
				_length = value;
				OnPropertyChanged("Length");
				if (_root != null && !_root.Root.IsInBatch)
					_root.UpdateLength();
			}
		}

		[XmlIgnore]
		public Controller Root {
			get { return _root; }
			set {
				if (value == _root)
					return;
				UndoRedo.DefaultChangeFactory.OnChanging(this, "Root", _root, value);
				_root = value;
				OnPropertyChanged("Root");
			}
		}

		[XmlIgnore]
		public bool IsSelected {
			get { return _selected; }
			set {
				if (value == _selected)
					return;
				_selected = value;
				OnPropertyChanged("IsSelected");
			}
		}
		#endregion

		public void UpdateStyle() {
			if (StyleChanged != null)
				StyleChanged(this, EventArgs.Empty);
		}
		
		public object GetUndoRoot() {
			return _root != null ? _root.Root : null;
		}

		public void UpdateScale() {
			OnPropertyChanged("Length");
		}

		public void SetColors(Color color, Color color2) {
			using (new UndoRedo.UndoBatch(this, "Set Colors", true)) {
				BegColor = color;
				EndColor = color2;
			}
		}

		public void StartLengthBatch() {
			_bth = new UndoRedo.UndoBatch(this, "Set Length", true);
		}

		public void EndLengthBatch() {
			_bth.Dispose();
			_root.UpdateLength();
		}

		public object Clone() {
			return new Frame(_begColor, _endColor, _length);
		}
	}
}
