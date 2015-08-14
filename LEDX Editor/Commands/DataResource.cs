using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using System.Windows;
using System.Windows.Markup;

namespace LEDX.Commands {
	public class DataResource : Freezable {
		/// <summary>
		/// Identifies the <see cref="BindingTarget"/> dependency property.
		/// </summary>
		/// <value>
		/// The identifier for the <see cref="BindingTarget"/> dependency property.
		/// </value>
		public static readonly DependencyProperty BindingTargetProperty = DependencyProperty.Register("BindingTarget", typeof(object), typeof(DataResource), new UIPropertyMetadata(null));

		/// <summary>
		/// Gets or sets the binding target.
		/// </summary>
		/// <value>The binding target.</value>
		public object BindingTarget {
			get { return GetValue(BindingTargetProperty); }
			set { SetValue(BindingTargetProperty, value); }
		}

		/// <summary>
		/// Creates an instance of the specified type using that type's default constructor.
		/// </summary>
		/// <returns>
		/// A reference to the newly created object.
		/// </returns>
		protected override Freezable CreateInstanceCore() {
			return (Freezable)Activator.CreateInstance(GetType());
		}

		/// <summary>
		/// Makes the instance a clone (deep copy) of the specified <see cref="Freezable"/>
		/// using base (non-animated) property values.
		/// </summary>
		/// <param name="sourceFreezable">
		/// The object to clone.
		/// </param>
		protected sealed override void CloneCore(Freezable sourceFreezable) {
			base.CloneCore(sourceFreezable);
		}
	}

	public class DataResourceBindingExtension : MarkupExtension {
		private object _mTargetObject;
		private object _mTargetProperty;
		private DataResource _mDataResouce;

		/// <summary>
		/// Gets or sets the data resource.
		/// </summary>
		/// <value>The data resource.</value>
		public DataResource DataResource {
			get {
				return _mDataResouce;
			}
			set {
				if (!Equals(_mDataResouce, value)) {
					if (_mDataResouce != null)
						_mDataResouce.Changed -= DataResource_Changed;
					_mDataResouce = value;
					if (_mDataResouce != null)
						_mDataResouce.Changed += DataResource_Changed;
				}
			}
		}

		/// <summary>
		/// When implemented in a derived class, returns an object that is set as the value of the target property for this markup extension.
		/// </summary>
		/// <param name="serviceProvider">Object that can provide services for the markup extension.</param>
		/// <returns>
		/// The object value to set on the property where the extension is applied.
		/// </returns>
		public override object ProvideValue(IServiceProvider serviceProvider) {
			IProvideValueTarget target = (IProvideValueTarget)serviceProvider.GetService(typeof(IProvideValueTarget));

			_mTargetObject = target.TargetObject;
			_mTargetProperty = target.TargetProperty;

			// mTargetProperty can be null when this is called in the Designer.
			Debug.Assert(_mTargetProperty != null || DesignerProperties.GetIsInDesignMode(new DependencyObject()));

			if (DataResource.BindingTarget == null && _mTargetProperty != null) {
				PropertyInfo propInfo = _mTargetProperty as PropertyInfo;
				if (propInfo != null) {
					try {
						return Activator.CreateInstance(propInfo.PropertyType);
					} catch (MissingMethodException) {
						// there isn't a default constructor
					}
				}

				DependencyProperty depProp = _mTargetProperty as DependencyProperty;
				if (depProp != null) {
					DependencyObject depObj = (DependencyObject)_mTargetObject;
					return depObj.GetValue(depProp);
				}
			}

			return DataResource.BindingTarget;
		}

		private void DataResource_Changed(object sender, EventArgs e) {
			// Ensure that the bound object is updated when DataResource changes.
			DataResource dataResource = (DataResource)sender;
			DependencyProperty depProp = _mTargetProperty as DependencyProperty;

			if (depProp != null) {
				DependencyObject depObj = (DependencyObject)_mTargetObject;
				object value = Convert(dataResource.BindingTarget, depProp.PropertyType);
				depObj.SetValue(depProp, value);
			} else {
				PropertyInfo propInfo = _mTargetProperty as PropertyInfo;
				if (propInfo != null) {
					object value = Convert(dataResource.BindingTarget, propInfo.PropertyType);
					propInfo.SetValue(_mTargetObject, value, new object[0]);
				}
			}
		}

		private object Convert(object obj, Type toType) {
			try {
				return System.Convert.ChangeType(obj, toType);
			} catch (InvalidCastException) {
				return obj;
			}
		}
	}
}
