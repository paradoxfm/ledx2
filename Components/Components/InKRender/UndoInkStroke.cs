using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Media;

namespace LEDX.Components.InKRender {
	/// <summary>
	/// This operation covers collecting new strokes, stroke-erase, and point-erase.
	/// </summary>
	class StrokesAddedOrRemovedStroke : CommandItem {
		InkCanvasEditingMode _editingMode;
		StrokeCollection _added, _removed;
		int _editingOperationCount;

		public StrokesAddedOrRemovedStroke(CommandStack commandStack, InkCanvasEditingMode editingMode, StrokeCollection added, StrokeCollection removed, int editingOperationCount)
			: base(commandStack) {
			_editingMode = editingMode;

			_added = added;
			_removed = removed;

			_editingOperationCount = editingOperationCount;
		}

		public override void Undo() {
			_commandStack.StrokeCollection.Remove(_added);
			//_commandStack.StrokeCollection.Add(_removed);
			AddAllStrokesAtOriginalIndex(_removed);
		}

		private void AddAllStrokesAtOriginalIndex(StrokeCollection toBeAdded) {
			foreach (Stroke stroke in toBeAdded) {
				int strokeIndex = (int)stroke.GetPropertyData(STROKE_INDEX_PROPERTY);
				if (strokeIndex > _commandStack.StrokeCollection.Count)
					strokeIndex = _commandStack.StrokeCollection.Count;
				_commandStack.StrokeCollection.Insert(strokeIndex, stroke);
			}
		}

		public override void Redo() {
			//_commandStack.StrokeCollection.Add(_added);
			AddAllStrokesAtOriginalIndex(_added);
			_commandStack.StrokeCollection.Remove(_removed);
		}

		public override bool Merge(CommandItem newitem) {
			StrokesAddedOrRemovedStroke newitemx = newitem as StrokesAddedOrRemovedStroke;
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
			foreach (Stroke doomed in newitemx._removed)
				if (_added.Contains(doomed))
					_added.Remove(doomed);
				else
					_removed.Add(doomed);
			_added.Add(newitemx._added);
			return true;
		}
	}

	/// <summary>
	/// This operation covers move and resize operations.
	/// </summary>
	class SelectionMovedOrResizedStroke : CommandItem {
		StrokeCollection _selection;
		Rect _newrect, _oldrect;
		int _editingOperationCount;

		public SelectionMovedOrResizedStroke(CommandStack commandStack, StrokeCollection selection, Rect newrect, Rect oldrect, int editingOperationCount)
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
			SelectionMovedOrResizedStroke newitemx = newitem as SelectionMovedOrResizedStroke;
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

		static bool StrokeCollectionsAreEqual(StrokeCollection a, StrokeCollection b) {
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
