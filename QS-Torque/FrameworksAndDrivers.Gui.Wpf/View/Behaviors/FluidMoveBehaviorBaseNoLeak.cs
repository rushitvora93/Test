// Type: Microsoft.Expression.Interactivity.Layout.FluidMoveBehaviorBase
// Assembly: Microsoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35

using System.Windows.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using Microsoft.Xaml.Behaviors;
using Microsoft.Xaml.Behaviors.Layout;

namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    public abstract class FluidMoveBehaviorBaseNoLeak : Behavior<FrameworkElement>
    {
        public static readonly DependencyProperty AppliesToProperty = DependencyProperty.Register("AppliesTo", typeof(FluidMoveScope), typeof(FluidMoveBehaviorBaseNoLeak), new PropertyMetadata((object)FluidMoveScope.Self));
        public static readonly DependencyProperty IsActiveProperty = DependencyProperty.Register("IsActive", typeof(bool), typeof(FluidMoveBehaviorBaseNoLeak), new PropertyMetadata((object)true));
        public static readonly DependencyProperty TagProperty = DependencyProperty.Register("Tag", typeof(TagType), typeof(FluidMoveBehaviorBaseNoLeak), new PropertyMetadata((object)TagType.Element));
        public static readonly DependencyProperty TagPathProperty = DependencyProperty.Register("TagPath", typeof(string), typeof(FluidMoveBehaviorBaseNoLeak), new PropertyMetadata((object)string.Empty));
        protected static readonly DependencyProperty IdentityTagProperty = DependencyProperty.RegisterAttached("IdentityTag", typeof(object), typeof(FluidMoveBehaviorBaseNoLeak), new PropertyMetadata((PropertyChangedCallback)null));
        internal static Dictionary<object, FluidMoveBehaviorBaseNoLeak.TagData> TagDictionary = new Dictionary<object, FluidMoveBehaviorBaseNoLeak.TagData>();
        private static DateTime NextToLastPurgeTick = DateTime.MinValue;
        private static DateTime LastPurgeTick = DateTime.MinValue;
        private static TimeSpan MinTickDelta = TimeSpan.FromSeconds(0.5);

        public FluidMoveScope AppliesTo
        {
            get
            {
                return (FluidMoveScope)this.GetValue(FluidMoveBehaviorBaseNoLeak.AppliesToProperty);
            }
            set
            {
                this.SetValue(FluidMoveBehaviorBaseNoLeak.AppliesToProperty, (object)value);
            }
        }

        public bool IsActive
        {
            get
            {
                return (bool)this.GetValue(FluidMoveBehaviorBaseNoLeak.IsActiveProperty);
            }
            set
            {
                this.SetValue(FluidMoveBehaviorBaseNoLeak.IsActiveProperty, value);
            }
        }

        public TagType Tag
        {
            get
            {
                return (TagType)this.GetValue(FluidMoveBehaviorBaseNoLeak.TagProperty);
            }
            set
            {
                this.SetValue(FluidMoveBehaviorBaseNoLeak.TagProperty, (object)value);
            }
        }

        public string TagPath
        {
            get
            {
                return (string)this.GetValue(FluidMoveBehaviorBaseNoLeak.TagPathProperty);
            }
            set
            {
                this.SetValue(FluidMoveBehaviorBaseNoLeak.TagPathProperty, (object)value);
            }
        }

        protected virtual bool ShouldSkipInitialLayout
        {
            get
            {
                return this.Tag == TagType.DataContext;
            }
        }

        static FluidMoveBehaviorBaseNoLeak()
        {
        }

        protected static object GetIdentityTag(DependencyObject obj)
        {
            return obj.GetValue(FluidMoveBehaviorBaseNoLeak.IdentityTagProperty);
        }

        protected static void SetIdentityTag(DependencyObject obj, object value)
        {
            obj.SetValue(FluidMoveBehaviorBaseNoLeak.IdentityTagProperty, value);
        }


        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.LayoutUpdated += this.AssociatedObject_LayoutUpdated;
            // Added these two handlers to detect the control being loaded or unloaded.
            this.AssociatedObject.Unloaded += AssociatedObject_Unloaded;
            this.AssociatedObject.Loaded += AssociatedObject_Loaded;

        }
        // FIX ADDIN to TagDictonary Holding Reference
        void AssociatedObject_Loaded(object sender, RoutedEventArgs e)
        {
            // When loaded, make sure IsActive is True so the LayoutUpdated can populate the TagDictonary
            this.IsActive = true;
            AssociatedObject_LayoutUpdated(null, null);
        }

        // FIX ADDIN to TagDictonary Holding Reference
        void AssociatedObject_Unloaded(object sender, RoutedEventArgs e)
        {
            // Set Isactive to false to prevent the LayoutUpdated from populating the TagDictonary again.
            this.IsActive = false;
            // FIX to TagDictonary Holding Reference to your objects when the control is unloaded.
            TagDictionary.Clear();
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            this.AssociatedObject.LayoutUpdated -= new EventHandler(this.AssociatedObject_LayoutUpdated);
        }

        private void AssociatedObject_LayoutUpdated(object sender, System.EventArgs e)
        {
            if (!this.IsActive)
                return;
            if (DateTime.Now - FluidMoveBehaviorBaseNoLeak.LastPurgeTick >= FluidMoveBehaviorBaseNoLeak.MinTickDelta)
            {
                List<object> list = (List<object>)null;
                foreach (KeyValuePair<object, FluidMoveBehaviorBaseNoLeak.TagData> keyValuePair in FluidMoveBehaviorBaseNoLeak.TagDictionary)
                {
                    if (keyValuePair.Value.Timestamp < FluidMoveBehaviorBaseNoLeak.NextToLastPurgeTick)
                    {
                        if (list == null)
                            list = new List<object>();
                        list.Add(keyValuePair.Key);
                    }
                }
                if (list != null)
                {
                    foreach (object key in list)
                        FluidMoveBehaviorBaseNoLeak.TagDictionary.Remove(key);
                }
                FluidMoveBehaviorBaseNoLeak.NextToLastPurgeTick = FluidMoveBehaviorBaseNoLeak.LastPurgeTick;
                FluidMoveBehaviorBaseNoLeak.LastPurgeTick = DateTime.Now;
            }
            if (this.AppliesTo == FluidMoveScope.Self)
            {
                this.UpdateLayoutTransition(this.AssociatedObject);
            }
            else
            {
                Panel panel = this.AssociatedObject as Panel;
                if (panel == null)
                    return;
                foreach (FrameworkElement child in panel.Children)
                    this.UpdateLayoutTransition(child);
            }
        }

        private void UpdateLayoutTransition(FrameworkElement child)
        {
            if ((child.Visibility == Visibility.Collapsed || !child.IsLoaded) && this.ShouldSkipInitialLayout)
                return;
            FrameworkElement visualRoot = FluidMoveBehaviorBaseNoLeak.GetVisualRoot(child);
            FluidMoveBehaviorBaseNoLeak.TagData newTagData = new FluidMoveBehaviorBaseNoLeak.TagData();
            newTagData.Parent = VisualTreeHelper.GetParent((DependencyObject)child) as FrameworkElement;
            newTagData.ParentRect = GetLayoutRect(child); // FIX ADDIN - Use this instead of the internal method the oringal one has.
            newTagData.Child = child;
            newTagData.Timestamp = DateTime.Now;
            try
            {
                newTagData.AppRect = FluidMoveBehaviorBaseNoLeak.TranslateRect(newTagData.ParentRect, newTagData.Parent, visualRoot);
            }
            catch (ArgumentException)
            {
                if (this.ShouldSkipInitialLayout)
                    return;
            }
            this.EnsureTags(child);
            object tag = FluidMoveBehaviorBaseNoLeak.GetIdentityTag((DependencyObject)child) ?? (object)child;
            this.UpdateLayoutTransitionCore(child, visualRoot, tag, newTagData);
        }

        // FIX ADDIN - Added so we can access this method. It exists in a different class with the same access modifiers, easier to just bring the code over.
        internal static Rect GetLayoutRect(FrameworkElement element)
        {
            double num1 = element.ActualWidth;
            double num2 = element.ActualHeight;
            if (element is Image || element is MediaElement)
            {
                if (element.Parent.GetType() == typeof(Canvas))
                {
                    num1 = double.IsNaN(element.Width) ? num1 : element.Width;
                    num2 = double.IsNaN(element.Height) ? num2 : element.Height;
                }
                else
                {
                    num1 = element.RenderSize.Width;
                    num2 = element.RenderSize.Height;
                }
            }
            double width = element.Visibility == Visibility.Collapsed ? 0.0 : num1;
            double height = element.Visibility == Visibility.Collapsed ? 0.0 : num2;
            Thickness margin = element.Margin;
            Rect layoutSlot = LayoutInformation.GetLayoutSlot(element);
            double x = 0.0;
            double y = 0.0;
            switch (element.HorizontalAlignment)
            {
                case HorizontalAlignment.Left:
                    x = layoutSlot.Left + margin.Left;
                    break;
                case HorizontalAlignment.Center:
                    x = (layoutSlot.Left + margin.Left + layoutSlot.Right - margin.Right) / 2.0 - width / 2.0;
                    break;
                case HorizontalAlignment.Right:
                    x = layoutSlot.Right - margin.Right - width;
                    break;
                case HorizontalAlignment.Stretch:
                    x = Math.Max(layoutSlot.Left + margin.Left, (layoutSlot.Left + margin.Left + layoutSlot.Right - margin.Right) / 2.0 - width / 2.0);
                    break;
            }
            switch (element.VerticalAlignment)
            {
                case VerticalAlignment.Top:
                    y = layoutSlot.Top + margin.Top;
                    break;
                case VerticalAlignment.Center:
                    y = (layoutSlot.Top + margin.Top + layoutSlot.Bottom - margin.Bottom) / 2.0 - height / 2.0;
                    break;
                case VerticalAlignment.Bottom:
                    y = layoutSlot.Bottom - margin.Bottom - height;
                    break;
                case VerticalAlignment.Stretch:
                    y = Math.Max(layoutSlot.Top + margin.Top, (layoutSlot.Top + margin.Top + layoutSlot.Bottom - margin.Bottom) / 2.0 - height / 2.0);
                    break;
            }
            return new Rect(x, y, width, height);
        }

        internal abstract void UpdateLayoutTransitionCore(FrameworkElement child, FrameworkElement root, object tag, FluidMoveBehaviorBaseNoLeak.TagData newTagData);

        protected virtual void EnsureTags(FrameworkElement child)
        {
            if (this.Tag != TagType.DataContext || child.ReadLocalValue(FluidMoveBehaviorBaseNoLeak.IdentityTagProperty) is BindingExpression)
                return;
            child.SetBinding(FluidMoveBehaviorBaseNoLeak.IdentityTagProperty, (BindingBase)new Binding(this.TagPath));
        }

        private static FrameworkElement GetVisualRoot(FrameworkElement child)
        {
            while (true)
            {
                FrameworkElement frameworkElement = VisualTreeHelper.GetParent((DependencyObject)child) as FrameworkElement;
                if (frameworkElement != null && AdornerLayer.GetAdornerLayer((Visual)frameworkElement) != null)
                    child = frameworkElement;
                else
                    break;
            }
            return child;
        }

        internal static Rect TranslateRect(Rect rect, FrameworkElement from, FrameworkElement to)
        {
            if (from == null || to == null)
                return rect;
            Point point1 = new Point(rect.Left, rect.Top);
            Point point2 = from.TransformToVisual((Visual)to).Transform(point1);
            return new Rect(point2.X, point2.Y, rect.Width, rect.Height);
        }

        internal class TagData
        {
            public FrameworkElement Child { get; set; }

            public FrameworkElement Parent { get; set; }

            public Rect ParentRect { get; set; }

            public Rect AppRect { get; set; }

            public DateTime Timestamp { get; set; }

            public object InitialTag { get; set; }
        }
    }
}
