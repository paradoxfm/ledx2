using System;
using System.Collections.Generic;
using System.Windows.Ink;
using System.Windows.Controls;

namespace LEDX.Components.InKRender {
	sealed class CommandStack {

		/// <param name="strokes"></param>
		public CommandStack(StrokeCollection strokes) {
			if (strokes == null)
				throw new ArgumentNullException("strokes");
			_strokeCollection = strokes;
			_undoStack = new Stack<CommandItem>();
			_redoStack = new Stack<CommandItem>();
			_disableChangeTracking = false;
		}

		/// <summary>
		/// StrokeCollection to track changes for
		/// </summary>
		public StrokeCollection StrokeCollection { get { return _strokeCollection; } }

		/// <summary>
		/// StrokeCollection to track changes for
		/// </summary>
		public UIElementCollection ElementCollection { get { return _elementCollection; } }


		/// <summary>
		/// Only undo if there are more items in the stack to step back into.
		/// </summary>
		public bool CanUndo { get { return (_undoStack.Count > 0); } }

		/// <summary>
		/// Only undo if one or more steps back in the stack.
		/// </summary>
		public bool CanRedo { get { return (_redoStack.Count > 0); } }

		/// <summary>
		/// Add an item to the top of the command stack
		/// </summary>
		public void Undo() {
			if (!CanUndo)
				throw new InvalidOperationException("No actions to undo");
			CommandItem item = _undoStack.Pop();
			// Invoke the undo operation, with change-tracking temporarily suspended.
			_disableChangeTracking = true;
			try {
				item.Undo();
			} finally {
				_disableChangeTracking = false;
			}

			//place this item on the redo stack
			_redoStack.Push(item);
		}

		/// <summary>
		/// Take the top item off the command stack.
		/// </summary>
		public void Redo() {
			if (!CanRedo)
				throw new InvalidOperationException();
			CommandItem item = _redoStack.Pop();
			// Invoke the redo operation, with change-tracking temporarily suspended.
			_disableChangeTracking = true;
			try {
				item.Redo();
			} finally {
				_disableChangeTracking = false;
			}
			//place this item on the undo stack
			_undoStack.Push(item);
		}

		/// <summary>
		/// Add a command item to the stack.
		/// </summary>
		/// <param name="item"></param>
		public void Enqueue(CommandItem item) {
			if (item == null)
				throw new ArgumentNullException("item");
			// Ensure we don't enqueue new items if we're being changed programmatically.
			if (_disableChangeTracking)
				return;
			// Check to see if this new item can be merged with previous.
			bool merged = false;
			if (_undoStack.Count > 0) {
				CommandItem prev = _undoStack.Peek();
				merged = prev.Merge(item);
			}
			// If not, append the new command item
			if (!merged)
				_undoStack.Push(item);
			//clear the redo stack
			if (_redoStack.Count > 0)
				_redoStack.Clear();
		}

		/// <summary>
		/// Implementation
		/// </summary>
		private StrokeCollection _strokeCollection;
		private UIElementCollection _elementCollection;


		private Stack<CommandItem> _undoStack;
		private Stack<CommandItem> _redoStack;


		bool _disableChangeTracking; // reentrancy guard: disables tracking of programmatic changes 
		// (eg, in response to undo/redo ops)
	}

	/// <summary>
	/// Derive from this class for every undoable/redoable operation you wish to support.
	/// </summary>
	abstract class CommandItem {
		public static Guid STROKE_INDEX_PROPERTY = Guid.NewGuid();

		// Interface
		public abstract void Undo();
		public abstract void Redo();

		// Allows multiple subsequent commands of the same type to roll-up into one 
		// logical undoable/redoable command -- return false if newitem is incompatable.
		public abstract bool Merge(CommandItem newitem);

		// Implementation
		protected CommandStack _commandStack;

		protected CommandItem(CommandStack commandStack) {
			_commandStack = commandStack;
		}
	}
}
