using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Media;
using System.Xml.Serialization;

namespace LEDX.Model {

	public class Controller : BaseModel, UndoRedo.ISupportsUndo {

		public enum Modes {
			Edit, Draw, Flash
		}

		bool _isflash;
		bool _selected;
		bool? _isplay = false;
		int _button = 1;
		int _num = 1;
		double _maxlen;
		double _width;
		double _playtime;
		double _animtime;
		string _title = "Контроллер";
		object _mode;
		LinearGradientBrush _icon;
		Document _root;
		ObservableCollection<Frame> _frame;
		public event EventHandler AnimationChanged;
		public event EventHandler PlayPauseAnimation;
		public event EventHandler AnimationSeek;
		public event EventHandler PlayTimeCheck;
		public event EventHandler WidthChanged;
		public event EventHandler ModeChanged;

		#region свойства
		[XmlArray("Frames")]
		[XmlArrayItem("Frame")]
		public ObservableCollection<Frame> Frames {
			get { return _frame; }
			set { _frame = value; }
		}

		public Guid Id { get; set; }

		[XmlIgnore]
		public Document Root {
			get { return _root; }
			set {
				if (value == _root)
					return;
				UndoRedo.DefaultChangeFactory.OnChanging(this, "Root", _root, value);
				_root = value;
				OnPropertyChanged("Root");
			}
		}

		public int Number {
			get { return _num; }
			set {
				if (value == _num)
					return;
				if (value < 1 || value > 50)
					return;
				_num = value;
				UndoRedo.DefaultChangeFactory.OnChanging(this, "Number", _num, value);
				OnPropertyChanged("Number");
			}
		}

		public int Button {
			get { return _button; }
			set {
				if (value == _button)
					return;
				if (value < 1 || value > 5)
					return;
				_button = value;
				UndoRedo.DefaultChangeFactory.OnChanging(this, "Button", _button, value);
				OnPropertyChanged("Button");
			}
		}

		public string Title {
			get { return _title; }
			set {
				if (value == _title)
					return;
				if (string.IsNullOrEmpty(value))
					return;
				UndoRedo.DefaultChangeFactory.OnChanging(this, "Title", _title, value);
				_title = value;
				OnPropertyChanged("Title");
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

		[XmlIgnore]
		public object Mode {
			get { return _mode; }
			set {
				if (value == _mode)
					return;
				_mode = value;
				OnPropertyChanged("Mode");
				if (ModeChanged != null)
					ModeChanged(value, EventArgs.Empty);
			}
		}

		[XmlIgnore]
		public bool IsFlash {
			get { return _isflash; }
			set {
				if (value == _isflash)
					return;
				_isflash = value;
				OnPropertyChanged("IsFlash");
			}
		}

		[XmlIgnore]
		public bool? IsPlay {
			get { return _isplay; }
			set {
				if (value == _isplay)
					return;
				_isplay = value;
				if (PlayPauseAnimation != null)
					PlayPauseAnimation(value, EventArgs.Empty);
			}
		}

		[XmlIgnore]
		public double PlayTime {
			get {
				PlayTimeCheck(this, null);
				return _playtime;
			}
			set { _playtime = value; }
		}

		[XmlIgnore]
		public double AnimationTime {
			get { return _animtime; }
			set {
				_animtime = value;
				AnimationSeek(value, new EventArgs());
			}
		}

		[XmlIgnore]
		public double MaxLength {
			get { return _maxlen; }
			set {
				_maxlen = value;
				if (AnimationChanged != null)
					AnimationChanged(this, EventArgs.Empty);
			}
		}

		[XmlIgnore]
		public double Width {
			get { return _width; }
			set {
				if (Equals(value, _width))
					return;
				_width = value;
				OnPropertyChanged("Width");
				if (WidthChanged != null)
					WidthChanged(this, EventArgs.Empty);
			}
		}

		[XmlIgnore]
		public double Length {
			get {
				return _frame.Sum(t => t.Length);
			}
		}

		[XmlIgnore]
		public LinearGradientBrush IconLarge {
			get { return _icon ?? (_icon = GetIcon()); }
			set { _icon = value; }
		}
		#endregion

		public Controller() {
			_frame = new ObservableCollection<Frame>();
			_frame.CollectionChanged += Frames_CollectionChanged;
		}

		public Controller(int num)
			: this() {
			_num = num;
		}

		public void UpdateAnimation() {
			AnimationChanged(this, EventArgs.Empty);
		}

		private void Frames_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
			if (e.Action == NotifyCollectionChangedAction.Add)
				foreach (Frame item in e.NewItems)
					item.Root = this;
			else if (e.Action == NotifyCollectionChangedAction.Remove)
				foreach (Frame item in e.OldItems)
					item.Root = null;
			if (e.Action == NotifyCollectionChangedAction.Add || e.Action == NotifyCollectionChangedAction.Remove)
				OnPropertyChanged("Length");
			UndoRedo.DefaultChangeFactory.OnCollectionChanged(this, "Frames", Frames, e);
		}

		public object GetUndoRoot() {
			return _root;
		}

		private LinearGradientBrush GetIcon() {
			LinearGradientBrush rez = new LinearGradientBrush();
			if (_frame.Count == 0)
				return rez;
			rez.EndPoint = new System.Windows.Point(1, .5);
			rez.StartPoint = new System.Windows.Point(0, .5);
			rez.GradientStops.Add(new GradientStop(_frame[0].BegColor, 0));
			for (int i = 0; i < _frame.Count; i++) {
				if (i > 0 && _frame[i].EndColor == _frame[i].BegColor)
					rez.GradientStops.Add(new GradientStop(_frame[i].BegColor, ((double)i) / _frame.Count));
				rez.GradientStops.Add(new GradientStop(_frame[i].EndColor, ((double)i + 1) / _frame.Count));
			}
			rez.Freeze();
			return rez;
		}

		public void InsertFrames(int pos, List<Frame> frames) {
			using (new UndoRedo.UndoBatch(this, "PasteFrames", true)) {
				for (int i = 0; i < frames.Count; i++, pos++)
					_frame.Insert(pos, (Frame)frames[i].Clone());
			}
			OnPropertyChanged("Length");
		}

		public void UpdateLength() {
			OnPropertyChanged("Length");
		}
	}
}
