using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;

namespace LEDX.Components.InKRender {
	/// <summary>
	/// This operation covers collecting new strokes, stroke-erase, and point-erase.
	/// </summary>
	class ElementAddOrRem : CommandItem {
		InkCanvasEditingMode _editingMode;
		UIElementCollection _added, _removed;
		int _editingOperationCount;

		public ElementAddOrRem(CommandStack commandStack, InkCanvasEditingMode editingMode,
			UIElementCollection added, UIElementCollection removed, int editingOperationCount)
			: base(commandStack) {
			_editingMode = editingMode;

			_added = added;
			_removed = removed;

			_editingOperationCount = editingOperationCount;
		}

		public override void Undo() {
			for (int i = _added.Count - 1; i >= 0; i--)
				_commandStack.ElementCollection.Remove(_added[i]);
			//_commandStack.StrokeCollection.Add(_removed);
			AddAllStrokesAtOriginalIndex(_removed);
		}

		private void AddAllStrokesAtOriginalIndex(UIElementCollection toBeAdded) {
			foreach (UIElement elm in toBeAdded) {
				//int strokeIndex = (int)elm.GetPropertyData(STROKE_INDEX_PROPERTY);
				//if (strokeIndex > _commandStack.StrokeCollection.Count)
				//  strokeIndex = _commandStack.StrokeCollection.Count;
				_commandStack.ElementCollection.Add(elm);
			}
		}

		public override void Redo() {
			//_commandStack.StrokeCollection.Add(_added);
			AddAllStrokesAtOriginalIndex(_added);
			for (int i = _added.Count - 1; i >= 0; i--)
				_commandStack.ElementCollection.Remove(_removed[i]);
		}

		public override bool Merge(CommandItem newitem) {
			ElementAddOrRem newitemx = newitem as ElementAddOrRem;
			if (newitemx == null ||
					newitemx._editingMode != _editingMode ||
					newitemx._editingOperationCount != _editingOperationCount) {
				return false;
			}

			// We only implement merging for repeated point-erase operations.
			if (_editingMode != InkCanvasEditingMode.EraseByPoint)
				return false;
			if (newitemx._editingMode != InkCanvasEditingMode.EraseByPoint)
				return false;

			// Note: possible for point-erase to have hit intersection of >1 strokes!
			// For each newly hit stroke, merge results into this command item.
			foreach (UIElement doomed in newitemx._removed)
				if (_added.Contains(doomed))
					_added.Remove(doomed);
				else
					_removed.Add(doomed);
			for (int i = 0; i < newitemx._added.Count; i++)
				_added.Add(newitemx._added[i]);
			return true;
		}
	}

	/// <summary>
	/// This operation covers move and resize operations.
	/// </summary>
	class ElementMovOrRes : CommandItem {
		UIElementCollection _selection;
		Rect _newrect, _oldrect;
		int _editingOperationCount;

		public ElementMovOrRes(CommandStack commandStack, UIElementCollection selection, Rect newrect, Rect oldrect, int editingOperationCount)
			: base(commandStack) {
			_selection = selection;
			_newrect = newrect;
			_oldrect = oldrect;
			_editingOperationCount = editingOperationCount;
		}

		public override void Undo() {
			Matrix m = GetTransformFromRectToRect(_newrect, _oldrect);
			_selection.Transform(m, false);
		}

		public override void Redo() {
			Matrix m = GetTransformFromRectToRect(_oldrect, _newrect);
			_selection.Transform(m, false);
		}

		public override bool Merge(CommandItem newitem) {
			ElementMovOrRes newitemx = newitem as ElementMovOrRes;
			// Ensure items are of the same type.
			if (newitemx == null ||
					newitemx._editingOperationCount != _editingOperationCount ||
					!StrokeCollectionsAreEqual(newitemx._selection, _selection)) {
				return false;
			}
			// Keep former oldrect, latter newrect.
			_newrect = newitemx._newrect;
			return true;
		}

		static Matrix GetTransformFromRectToRect(Rect src, Rect dst) {
			Matrix m = Matrix.Identity;
			m.Translate(-src.X, -src.Y);
			m.Scale(dst.Width / src.Width, dst.Height / src.Height);
			m.Translate(+dst.X, +dst.Y);
			return m;
		}

		static bool StrokeCollectionsAreEqual(UIElementCollection a, UIElementCollection b) {
			if (a == null && b == null)
				return true;
			if (a == null || b == null)
				return false;
			if (a.Count != b.Count)
				return false;

			for (int i = 0; i < a.Count; ++i)
				if (a[i] != b[i])
					return false;

			return true;
		}
	}
}
