﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace LEDX.Model.UndoRedo {

	/// <summary>
	/// Tracks the ChangeSets and behavior for a single root object (or document).
	/// </summary>
	public class UndoRoot {

		#region Member Variables

		// WeakReference because we don't want the undo stack to keep something locked in memory.
		private readonly WeakReference _root;

		// The list of undo / redo actions.
		private readonly Stack<ChangeSet> _undoStack;
		private readonly Stack<ChangeSet> _redoStack;

		// Tracks whether a batch (or batches) has been started.
		private int _isInBatchCounter;

		// Determines whether the undo framework will consolidate (or de-dupe) changes to the same property within the batch.
		private bool _consolidateChangesForSameInstance;

		// When in a batch, changes are grouped into this ChangeSet.
		private ChangeSet _currentBatchChangeSet;

		// Is the system currently undoing or redoing a changeset.
		private bool _isUndoingOrRedoing;

		#endregion

		#region Events

		public event EventHandler UndoStackChanged;

		public event EventHandler RedoStackChanged;

		#endregion

		#region Constructors

		/// <summary>
		/// Create a new UndoRoot to track undo / redo actions for a given instance / document.
		/// </summary>
		/// <param name="root">The "root" instance of the object hierarchy. All changesets will
		/// need to passs a reference to this instance when they track changes.</param>
		public UndoRoot(object root) {
			_root = new WeakReference(root);
			_undoStack = new Stack<ChangeSet>();
			_redoStack = new Stack<ChangeSet>();
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// The instance that represents the root (or document) for this set of changes.
		/// </summary>
		/// <remarks>
		/// This is needed so that a single instance of the application can track undo histories
		/// for multiple "root" or "document" instances at the same time. These histories should not
		/// overlap or show in the same undo history.
		/// </remarks>
		public object Root {
			get {
				if (null != _root && _root.IsAlive)
					return _root.Target;
				return null;
			}
		}

		/// <summary>
		/// A collection of undoable change sets for the current Root.
		/// </summary>
		public IEnumerable<ChangeSet> UndoStack {
			get { return _undoStack; }
		}

		/// <summary>
		/// A collection of redoable change sets for the current Root.
		/// </summary>
		public IEnumerable<ChangeSet> RedoStack {
			get { return _redoStack; }
		}

		/// <summary>
		/// Is this UndoRoot currently collecting changes as part of a batch.
		/// </summary>
		public bool IsInBatch {
			get {
				return _isInBatchCounter > 0;
			}
		}

		/// <summary>
		/// Is this UndoRoot currently undoing or redoing a change set.
		/// </summary>
		public bool IsUndoingOrRedoing {
			get {
				return _isUndoingOrRedoing;
			}
		}

		/// <summary>
		/// Should changes to the same property be consolidated (de-duped).
		/// </summary>
		public bool ConsolidateChangesForSameInstance {
			get {
				return _consolidateChangesForSameInstance;
			}
		}

		public bool CanUndo {
			get {
				return _undoStack.Count > 0;
			}
		}

		public bool CanRedo {
			get {
				return _redoStack.Count > 0;
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Tells the UndoRoot that all subsequent changes should be part of a single ChangeSet.
		/// </summary>
		public void BeginChangeSetBatch(string batchDescription, bool consolidateChangesForSameInstance) {
			// We don't want to add additional changes representing the operations that happen when undoing or redoing a change.
			if (_isUndoingOrRedoing)
				return;

			_isInBatchCounter++;

			if (_isInBatchCounter == 1) {
				_consolidateChangesForSameInstance = consolidateChangesForSameInstance;
				_currentBatchChangeSet = new ChangeSet(this, batchDescription, null);
				_undoStack.Push(_currentBatchChangeSet);
				OnUndoStackChanged();
			}
		}

		/// <summary>
		/// Tells the UndoRoot that it can stop collecting Changes into a single ChangeSet.
		/// </summary>
		public void EndChangeSetBatch() {
			_isInBatchCounter--;

			if (_isInBatchCounter < 0)
				_isInBatchCounter = 0;

			if (_isInBatchCounter == 0) {
				_consolidateChangesForSameInstance = false;
				_currentBatchChangeSet = null;
			}
		}

		/// <summary>
		/// Undo the first available ChangeSet.
		/// </summary>
		public void Undo() {
			var last = _undoStack.FirstOrDefault();
			if (null != last)
				Undo(last);
		}


		/// <summary>
		/// Undo all changesets up to and including the lastChangeToUndo.
		/// </summary>
		public void Undo(ChangeSet lastChangeToUndo) {
			if (IsInBatch)
				throw new InvalidOperationException("Cannot perform an Undo when the Undo Service is collecting a batch of changes. The batch must be completed first.");

			if (!_undoStack.Contains(lastChangeToUndo))
				throw new InvalidOperationException("The specified change does not exist in the list of undoable changes. Perhaps it has already been undone.");

			System.Diagnostics.Debug.WriteLine("Starting UNDO: " + lastChangeToUndo.Description);

			bool done = false;
			_isUndoingOrRedoing = true;

			try {
				do {
					var changeSet = _undoStack.Pop();
					OnUndoStackChanged();

					if (changeSet == lastChangeToUndo || _undoStack.Count == 0)
						done = true;

					changeSet.Undo();

					_redoStack.Push(changeSet);
					OnRedoStackChanged();

				} while (!done);
			} finally {
				_isUndoingOrRedoing = false;
			}

		}

		/// <summary>
		/// Redo the first available ChangeSet.
		/// </summary>
		public void Redo() {
			var last = _redoStack.FirstOrDefault();
			if (null != last)
				Redo(last);
		}

		/// <summary>
		/// Redo ChangeSets up to and including the lastChangeToRedo.
		/// </summary>
		public void Redo(ChangeSet lastChangeToRedo) {
			if (IsInBatch)
				throw new InvalidOperationException("Cannot perform a Redo when the Undo Service is collecting a batch of changes. The batch must be completed first.");

			if (!_redoStack.Contains(lastChangeToRedo))
				throw new InvalidOperationException("The specified change does not exist in the list of redoable changes. Perhaps it has already been redone.");

			System.Diagnostics.Debug.WriteLine("Starting REDO: " + lastChangeToRedo.Description);

			bool done = false;
			_isUndoingOrRedoing = true;
			try {
				do {
					var changeSet = _redoStack.Pop();
					OnRedoStackChanged();

					if (changeSet == lastChangeToRedo || _redoStack.Count == 0)
						done = true;

					changeSet.Redo();

					_undoStack.Push(changeSet);
					OnUndoStackChanged();

				} while (!done);
			} finally {
				_isUndoingOrRedoing = false;
			}
		}

		/// <summary>
		/// Add a change to the Undo history. The change will be added to the existing batch, if in a batch.
		/// Otherwise, a new ChangeSet will be created.
		/// </summary>
		/// <param name="change">The change to add to the history.</param>
		/// <param name="description">The description of this change.</param>
		public void AddChange(Change change, string description) {
			// System.Diagnostics.Debug.WriteLine("Starting AddChange: " + description);

			// We don't want to add additional changes representing the operations that happen when undoing or redoing a change.
			if (_isUndoingOrRedoing)
				return;

			//  If batched, add to the current ChangeSet, otherwise add a new ChangeSet.
			if (IsInBatch) {
				_currentBatchChangeSet.AddChange(change);
				//System.Diagnostics.Debug.WriteLine("AddChange: BATCHED " + description);
			} else {
				_undoStack.Push(new ChangeSet(this, description, change));
				OnUndoStackChanged();
				//System.Diagnostics.Debug.WriteLine("AddChange: " + description);
			}

			// Prune the RedoStack
			_redoStack.Clear();
			OnRedoStackChanged();
		}

		/// <summary>
		/// Adds a new changeset to the undo history. The change set will be added to the existing batch, if in a batch.
		/// </summary>
		/// <param name="changeSet">The ChangeSet to add.</param>
		public void AddChange(ChangeSet changeSet) {
			// System.Diagnostics.Debug.WriteLine("Starting AddChange: " + description);

			// We don't want to add additional changes representing the operations that happen when undoing or redoing a change.
			if (_isUndoingOrRedoing)
				return;

			//  If batched, add to the current ChangeSet, otherwise add a new ChangeSet.
			if (IsInBatch) {
				foreach (var chg in changeSet.Changes) {
					_currentBatchChangeSet.AddChange(chg);
					//System.Diagnostics.Debug.WriteLine("AddChange: BATCHED " + description);
				}
			} else {
				_undoStack.Push(changeSet);
				OnUndoStackChanged();
				//System.Diagnostics.Debug.WriteLine("AddChange: " + description);
			}

			// Prune the RedoStack
			_redoStack.Clear();
			OnRedoStackChanged();
		}

		public void Clear() {
			if (IsInBatch || _isUndoingOrRedoing)
				throw new InvalidOperationException("Unable to clear the undo history because the system is collecting a batch of changes, or is in the process of undoing / redoing a change.");

			_undoStack.Clear();
			_redoStack.Clear();
			OnUndoStackChanged();
			OnRedoStackChanged();
		}

		#endregion

		#region Internal

		private void OnUndoStackChanged() {
			if (null != UndoStackChanged)
				UndoStackChanged(this, EventArgs.Empty);
		}

		private void OnRedoStackChanged() {
			if (null != RedoStackChanged)
				RedoStackChanged(this, EventArgs.Empty);
		}

		#endregion

	}
}
