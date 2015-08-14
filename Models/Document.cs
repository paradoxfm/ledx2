using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Xml.Serialization;

namespace LEDX.Model {
	public class Document : BaseModel, UndoRedo.ISupportsUndo {

		private int _und, _red;
		private readonly UndoRedo.UndoRoot _root;
		private readonly ObservableCollection<Controller> _cont;

		[XmlArray("Controllers")]
		[XmlArrayItem("Controller")]
		public ObservableCollection<Controller> Controller {
			get { return _cont; }
		}

		public event EventHandler OnChange {
			add {
				_root.RedoStackChanged += value;
				_root.UndoStackChanged += value;
			}
			remove {
				_root.RedoStackChanged -= value;
				_root.UndoStackChanged -= value;
			}
		}

		[XmlIgnore]
		public FlashSettings Flash { get; set; }

		[XmlIgnore]
		public bool Changed {
			get {
				return _und != _root.UndoStack.Count() || _red != _root.RedoStack.Count();
			}
			set {
				_und = _root.UndoStack.Count();
				_red = _root.RedoStack.Count();
			}
		}

		[XmlIgnore]
		public string Path { get; set; }

		[XmlIgnore]
		public bool IsInBatch { get { return _root.IsInBatch; } }

		public Document() {
			Flash = new FlashSettings();
			_cont = new ObservableCollection<Controller>();
			_root = UndoRedo.UndoService.Current[this];
			_cont.CollectionChanged += Controllers_CollectionChanged;
		}

		private void Controllers_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e) {
			if (e.Action == NotifyCollectionChangedAction.Add)
				foreach (Controller item in e.NewItems)
					item.Root = this;
			else if (e.Action == NotifyCollectionChangedAction.Remove)
				foreach (Controller item in e.OldItems)
					item.Root = null;
			UndoRedo.DefaultChangeFactory.OnCollectionChanged(this, "Controller", Controller, e);
		}

		#region undo/redo
		public bool CanUndo { get { return _root.CanUndo; } }

		public bool CanRedo { get { return _root.CanRedo; } }

		public void Undo() {
			_root.Undo();
		}

		public void Redo() {
			_root.Redo();
		}

		public object GetUndoRoot() {
			return this;
		}

		public void Clear() {
			_root.Clear();
		}
		#endregion

		#region ненужная шляпа
		[XmlIgnore]
		public int Count { get { return _cont.Count; } }

		[XmlIgnore]
		public Controller this[int item] {
			get { return _cont[item]; }
			set { _cont[item] = value; }
		}

		public void Add(Controller item) {
			_cont.Add(item);
		}

		public bool Remove(Controller item) {
			return _cont.Remove(item);
		}
		#endregion

	}
}
