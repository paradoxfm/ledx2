using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LEDX.Components {

	public class InkCanvasChildrenChangedEventArgs : EventArgs {
		public ReadOnlyCollection<UIElement> Added;
		public ReadOnlyCollection<UIElement> Removed;
	}

	/// <summary>
	/// Interaction logic for IncRender.xaml
	/// </summary>
	public partial class IncRender : InkCanvas {

		private bool isPlaying = false;
		private Image bkg = new Image();
		List<IAtimate> ani = new List<IAtimate>();

		public static readonly DependencyProperty IsBackLoadedProperty = DependencyProperty.Register("IsBackLoaded", typeof(bool), typeof(IncRender), new PropertyMetadata(false));
		public bool IsBackLoaded {
			get { return (bool)GetValue(IsBackLoadedProperty); }
			set { SetValue(IsBackLoadedProperty, value); }
		}

		public IncRender() {
			bkg.Tag = "Back";
			bkg.Stretch = Stretch.Fill;
			Children.Insert(0, bkg);
			InkCanvas.SetTop(bkg, 0);
			InkCanvas.SetLeft(bkg, 0);
			this.DefaultDrawingAttributes = new DrawingAttributes() { Color = Colors.Black, Width = 1, Height = 1, FitToCurve = true };

			HorizontalAlignment = HorizontalAlignment.Stretch;
			VerticalAlignment = VerticalAlignment.Stretch;
			EditingMode = InkCanvasEditingMode.Select;

			CommandBinding deleteBinding = new CommandBinding(ApplicationCommands.Delete,
															  new ExecutedRoutedEventHandler(InkCanvasDelete), new CanExecuteRoutedEventHandler(CanInkCanvasDelete));
			this.CommandBindings.Add(deleteBinding);
		}

		protected override void OnSelectionChanging(InkCanvasSelectionChangingEventArgs e) {
			bool selBak = Keyboard.Modifiers == ModifierKeys.Control;
			ReadOnlyCollection<UIElement> col = e.GetSelectedElements();
			List<UIElement> rez = new List<UIElement>();
			foreach (UIElement el in col)
				if (selBak && el == bkg)
					rez.Add(el);
				else if (!selBak && el != bkg)
					rez.Add(el);
			e.SetSelectedElements(rez);
		}

		protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved) {
			if (visualRemoved == bkg)
				visualRemoved = null;
			base.OnVisualChildrenChanged(visualAdded, visualRemoved);
		}

		#region commands
		public event EventHandler<InkCanvasChildrenChangedEventArgs> InkCanvasChildrenChanged;

		protected virtual void OnInkCanvasChildrenChanged(InkCanvasChildrenChangedEventArgs e) {
			if (InkCanvasChildrenChanged != null)
				InkCanvasChildrenChanged(this, e);
		}

		void CanInkCanvasDelete(object sender, CanExecuteRoutedEventArgs e) {
			e.CanExecute = EditingMode == InkCanvasEditingMode.Select;
		}

		void InkCanvasDelete(object sender, ExecutedRoutedEventArgs e) {
			ReadOnlyCollection<UIElement> selectedElements = GetSelectedElements();
			List<UIElement> removedElements = new List<UIElement>();

			for (int i = selectedElements.Count - 1; i >= 0; i--)
				if (selectedElements[i] != bkg) {
					removedElements.Add(selectedElements[i]);
					Children.Remove(selectedElements[i]);
				}

			InkCanvasChildrenChangedEventArgs args = new InkCanvasChildrenChangedEventArgs {
				Removed = new ReadOnlyCollection<UIElement>(removedElements),
				Added = new ReadOnlyCollection<UIElement>(new UIElement[] { })
			};
			OnInkCanvasChildrenChanged(args);
			OnSelectionChanged(new EventArgs());
		}
		#endregion

		#region ISupportsUndo Members

		public object GetUndoRoot() {
			return this;
		}

		#endregion

		#region IAnimation
		public bool? IsPlay {
			get { return isPlaying; }
			set {
				ani.Clear();
				for (int i = 0; i < Children.Count; i++) {
					IAtimate anElm = Children[i] as IAtimate;
					if (anElm != null) {
						ani.Add((IAtimate)Children[i]);
						((IAtimate)Children[i]).IsPlay = value;
					}
				}
				isPlaying = value.GetValueOrDefault();
			}
		}

		public double PlayTime {
			get {
				return ani.Count > 0 ? ani[0].PlayTime : 0;
			}
			set {
				foreach (IAtimate an in ani)
					an.PlayTime = value;
			}
		}

		public void RefreshAnimation() {
			for (int i = 0; i < Children.Count; i++) {
				IAtimate anmt = Children[i] as IAtimate;
				if (anmt != null)
					anmt.UpdateAnimation();
			}
		}

		public List<IAtimate> GetAnimables() {
			ani.Clear();
			for (int i = 0; i < Children.Count; i++) {
				IAtimate anmt = Children[i] as IAtimate;
				if (anmt != null)
					ani.Add(anmt);
			}
			return ani;
		}
		#endregion

		public void SetBackground(BitmapImage img) {
			IsBackLoaded = true;
			bkg.Source = img;
			bkg.Width = img.PixelWidth;
			bkg.Height = img.PixelHeight;
		}

		public void AddAnimationChild(UIElement elem) {
			IAtimate anElm = elem as IAtimate;
			if (anElm != null)
				ani.Add(anElm);
			Children.Add(elem);
		}
	}
}
