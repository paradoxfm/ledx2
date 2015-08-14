using System.Globalization;

namespace LEDX.Model.UndoRedo {

	public class ChangeKey<TKey, TValue> {
		private readonly TKey _mOne;
		private readonly TValue _mTwo;

		public TKey Item1 { get { return _mOne; } }
		public TValue Item2 { get { return _mTwo; } }

		public ChangeKey(TKey item1, TValue item2) {
			_mOne = item1;
			_mTwo = item2;
		}

		public override bool Equals(object obj) {
			if (obj == null)
				return false;
			ChangeKey<TKey, TValue> tuple = obj as ChangeKey<TKey, TValue>;
			if (tuple == null)
				return false;
			return Equals(_mOne, tuple._mOne) && Equals(_mTwo, tuple._mTwo);
		}

		public override int GetHashCode() {
			return CombineHashCodes(_mOne.GetHashCode(), _mTwo.GetHashCode());
		}

		public override string ToString() {
			return string.Format(CultureInfo.CurrentCulture, "Tuple of '{0}', '{1}'", _mOne, _mTwo);
		}

		internal static int CombineHashCodes(int h1, int h2) {
			return ((h1 << 5) + h1) ^ h2;
		}

	}

	public class ChangeKey<TKey, TValue, TAny> {
		private readonly TKey _mOne;
		private readonly TValue _mTwo;
		private readonly TAny _mThree;

		public TKey Item1 { get { return _mOne; } }
		public TValue Item2 { get { return _mTwo; } }
		public TAny Item3 { get { return _mThree; } }

		public ChangeKey(TKey item1, TValue item2, TAny item3) {
			_mOne = item1;
			_mTwo = item2;
			_mThree = item3;
		}

		public override bool Equals(object obj) {
			if (obj == null)
				return false;
			ChangeKey<TKey, TValue, TAny> tuple = obj as ChangeKey<TKey, TValue, TAny>;
			if (tuple == null)
				return false;
			if (Equals(_mOne, tuple._mOne) && Equals(_mTwo, tuple._mTwo))
				return Equals(_mThree, tuple._mThree);
			return false;
		}

		public override int GetHashCode() {
			return CombineHashCodes(_mOne.GetHashCode(), CombineHashCodes(_mTwo.GetHashCode(), _mThree.GetHashCode()));
		}

		public override string ToString() {
			return string.Format(CultureInfo.CurrentCulture, "Tuple of '{0}', '{1}', '{2}'", _mOne, _mTwo, _mThree);
		}

		internal static int CombineHashCodes(int h1, int h2) {
			return ((h1 << 5) + h1) ^ h2;
		}

	}
}
