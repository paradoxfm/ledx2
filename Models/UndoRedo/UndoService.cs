using System;
using System.Collections.Generic;

namespace LEDX.Model.UndoRedo {

	public class UndoService {

		#region Static Members

		private static UndoService _current;
		private static IDictionary<Type, WeakReference> _currentRootInstances;

		/// <summary>
		/// Get (or create) the singleton instance of the UndoService.
		/// </summary>
		public static UndoService Current {
			get { return _current ?? (_current = new UndoService()); }
		}

		/// <summary>
		/// Stores the "Current Instance" of a given object or document so that the rest of the model can access it.
		/// </summary>
		/// <typeparam name="T">The type of the root instance to store.</typeparam>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1004:GenericMethodsShouldProvideTypeParameter")]
		public static object GetCurrentDocumentInstance<T>() where T : class {
			if (null == _currentRootInstances)
				return null;

			var type = typeof(T);
			if (_currentRootInstances.ContainsKey(type)) {
				var wr = _currentRootInstances[type];

				if (null == wr || !wr.IsAlive) {
					_currentRootInstances.Remove(type);
					return null;
				}

				return wr.Target;
			}
			return null;
		}

		/// <summary>
		/// Stores the "Current Instance" of a given object or document so that the rest of the model can access it.
		/// </summary>
		/// <typeparam name="T">The type of the root instance to store.</typeparam>
		/// <param name="instance">The document or object instance that is the "currently active" instance.</param>
		public static void SetCurrentDocumentInstance<T>(T instance) where T : class {
			var type = typeof(T);

			if (null == _currentRootInstances) {
				if (null != instance)   // The instance can be null if it is being cleared.
					_currentRootInstances = new Dictionary<Type, WeakReference> { { type, new WeakReference(instance) } };
			} else {
				var existing = GetCurrentDocumentInstance<T>();
				if (null == existing && null != instance)
					_currentRootInstances.Add(type, new WeakReference(instance));
				else if (null != instance)
					_currentRootInstances[type] = new WeakReference(instance);
				else
					_currentRootInstances.Remove(type);
			}
		}

		#endregion

		#region Member Variables

		private readonly IDictionary<object, UndoRoot> _roots;

		#endregion

		#region Constructors

		public UndoService() {
			_roots = new Dictionary<object, UndoRoot>();
		}

		#endregion

		#region Public Properties

		/// <summary>
		/// Get (or create) an UndoRoot for the specified object or document instance.
		/// </summary>
		/// <param name="root">The object that represents the root of the document or object hierarchy.</param>
		/// <returns>An UndoRoot instance for this object.</returns>
		public UndoRoot this[object root] {
			get {
				if (null == root)
					return null;

				UndoRoot ret = null;

				if (_roots.ContainsKey(root))
					ret = _roots[root];

				if (null == ret) {
					ret = new UndoRoot(root);
					_roots.Add(root, ret);
				}

				return ret;
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Clear the cached UndoRoots.
		/// </summary>
		public void Clear() {
			_roots.Clear();
		}

		#endregion

	}
}
