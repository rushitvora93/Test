// Type: Microsoft.Expression.Interactivity.Layout.FluidMoveBehavior
// Assembly: Microsoft.Expression.Interactions, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// Assembly location: D:\Devel\HJT.NVR\NVRConnect5\MAIN\Source\UI\WPF\Desktop.Nvr\Dependencies\Expression\Microsoft.Expression.Interactions.dll

using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Xaml.Behaviors.Layout;

namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    public sealed class FluidMoveBehaviorNoLeak : FluidMoveBehaviorBaseNoLeak
    {
        public static readonly DependencyProperty DurationProperty = DependencyProperty.Register("Duration", typeof(Duration), typeof(FluidMoveBehaviorNoLeak), new PropertyMetadata((object)new Duration(TimeSpan.FromSeconds(1.0))));
        public static readonly DependencyProperty InitialTagProperty = DependencyProperty.Register("InitialTag", typeof(TagType), typeof(FluidMoveBehaviorNoLeak), new PropertyMetadata((object)TagType.Element));
        public static readonly DependencyProperty InitialTagPathProperty = DependencyProperty.Register("InitialTagPath", typeof(string), typeof(FluidMoveBehaviorNoLeak), new PropertyMetadata((object)string.Empty));
        private static readonly DependencyProperty InitialIdentityTagProperty = DependencyProperty.RegisterAttached("InitialIdentityTag", typeof(object), typeof(FluidMoveBehaviorNoLeak), new PropertyMetadata((PropertyChangedCallback)null));
        public static readonly DependencyProperty FloatAboveProperty = DependencyProperty.Register("FloatAbove", typeof(bool), typeof(FluidMoveBehaviorNoLeak), new PropertyMetadata((object)true));
        public static readonly DependencyProperty EaseXProperty = DependencyProperty.Register("EaseX", typeof(IEasingFunction), typeof(FluidMoveBehaviorNoLeak), new PropertyMetadata((PropertyChangedCallback)null));
        public static readonly DependencyProperty EaseYProperty = DependencyProperty.Register("EaseY", typeof(IEasingFunction), typeof(FluidMoveBehaviorNoLeak), new PropertyMetadata((PropertyChangedCallback)null));
        private static readonly DependencyProperty OverlayProperty = DependencyProperty.RegisterAttached("Overlay", typeof(object), typeof(FluidMoveBehaviorNoLeak), new PropertyMetadata((PropertyChangedCallback)null));
        private static readonly DependencyProperty CacheDuringOverlayProperty = DependencyProperty.RegisterAttached("CacheDuringOverlay", typeof(object), typeof(FluidMoveBehaviorNoLeak), new PropertyMetadata((PropertyChangedCallback)null));
        private static readonly DependencyProperty HasTransformWrapperProperty = DependencyProperty.RegisterAttached("HasTransformWrapper", typeof(bool), typeof(FluidMoveBehaviorNoLeak), new PropertyMetadata((object)false));
        private static Dictionary<object, Storyboard> TransitionStoryboardDictionary = new Dictionary<object, Storyboard>();

        public Duration Duration
        {
            get
            {
                return (Duration)this.GetValue(FluidMoveBehaviorNoLeak.DurationProperty);
            }
            set
            {
                this.SetValue(FluidMoveBehaviorNoLeak.DurationProperty, (object)value);
            }
        }

        public TagType InitialTag
        {
            get
            {
                return (TagType)this.GetValue(FluidMoveBehaviorNoLeak.InitialTagProperty);
            }
            set
            {
                this.SetValue(FluidMoveBehaviorNoLeak.InitialTagProperty, (object)value);
            }
        }

        public string InitialTagPath
        {
            get
            {
                return (string)this.GetValue(FluidMoveBehaviorNoLeak.InitialTagPathProperty);
            }
            set
            {
                this.SetValue(FluidMoveBehaviorNoLeak.InitialTagPathProperty, (object)value);
            }
        }

        public bool FloatAbove
        {
            get
            {
                return (bool)this.GetValue(FluidMoveBehaviorNoLeak.FloatAboveProperty);
            }
            set
            {
                this.SetValue(FluidMoveBehaviorNoLeak.FloatAboveProperty, value);
            }
        }

        public IEasingFunction EaseX
        {
            get
            {
                return (IEasingFunction)this.GetValue(FluidMoveBehaviorNoLeak.EaseXProperty);
            }
            set
            {
                this.SetValue(FluidMoveBehaviorNoLeak.EaseXProperty, (object)value);
            }
        }

        public IEasingFunction EaseY
        {
            get
            {
                return (IEasingFunction)this.GetValue(FluidMoveBehaviorNoLeak.EaseYProperty);
            }
            set
            {
                this.SetValue(FluidMoveBehaviorNoLeak.EaseYProperty, (object)value);
            }
        }

        protected override bool ShouldSkipInitialLayout
        {
            get
            {
                if (!base.ShouldSkipInitialLayout)
                    return this.InitialTag == TagType.DataContext;
                else
                    return true;
            }
        }

        static FluidMoveBehaviorNoLeak()
        {
        }

        private static object GetInitialIdentityTag(DependencyObject obj)
        {
            return obj.GetValue(FluidMoveBehaviorNoLeak.InitialIdentityTagProperty);
        }

        private static void SetInitialIdentityTag(DependencyObject obj, object value)
        {
            obj.SetValue(FluidMoveBehaviorNoLeak.InitialIdentityTagProperty, value);
        }

        private static object GetOverlay(DependencyObject obj)
        {
            return obj.GetValue(FluidMoveBehaviorNoLeak.OverlayProperty);
        }

        private static void SetOverlay(DependencyObject obj, object value)
        {
            obj.SetValue(FluidMoveBehaviorNoLeak.OverlayProperty, value);
        }

        private static object GetCacheDuringOverlay(DependencyObject obj)
        {
            return obj.GetValue(FluidMoveBehaviorNoLeak.CacheDuringOverlayProperty);
        }

        private static void SetCacheDuringOverlay(DependencyObject obj, object value)
        {
            obj.SetValue(FluidMoveBehaviorNoLeak.CacheDuringOverlayProperty, value);
        }

        private static bool GetHasTransformWrapper(DependencyObject obj)
        {
            return (bool)obj.GetValue(FluidMoveBehaviorNoLeak.HasTransformWrapperProperty);
        }

        private static void SetHasTransformWrapper(DependencyObject obj, bool value)
        {
            obj.SetValue(FluidMoveBehaviorNoLeak.HasTransformWrapperProperty, value);
        }

        protected override void EnsureTags(FrameworkElement child)
        {
            base.EnsureTags(child);
            if (this.InitialTag != TagType.DataContext || child.ReadLocalValue(FluidMoveBehaviorNoLeak.InitialIdentityTagProperty) is BindingExpression)
                return;
            child.SetBinding(FluidMoveBehaviorNoLeak.InitialIdentityTagProperty, (BindingBase)new Binding(this.InitialTagPath));
        }

        internal override void UpdateLayoutTransitionCore(FrameworkElement child, FrameworkElement root, object tag, FluidMoveBehaviorBaseNoLeak.TagData newTagData)
        {
            bool flag1 = false;
            bool usingBeforeLoaded = false;
            object initialIdentityTag = FluidMoveBehaviorNoLeak.GetInitialIdentityTag((DependencyObject)child);
            FluidMoveBehaviorBaseNoLeak.TagData tagData1;
            bool flag2 = FluidMoveBehaviorBaseNoLeak.TagDictionary.TryGetValue(tag, out tagData1);
            if (flag2 && tagData1.InitialTag != initialIdentityTag)
            {
                flag2 = false;
                FluidMoveBehaviorBaseNoLeak.TagDictionary.Remove(tag);
            }
            Rect rect;
            if (!flag2)
            {
                FluidMoveBehaviorBaseNoLeak.TagData tagData2;
                if (initialIdentityTag != null && FluidMoveBehaviorBaseNoLeak.TagDictionary.TryGetValue(initialIdentityTag, out tagData2))
                {
                    rect = FluidMoveBehaviorBaseNoLeak.TranslateRect(tagData2.AppRect, root, newTagData.Parent);
                    flag1 = true;
                    usingBeforeLoaded = true;
                }
                else
                    rect = Rect.Empty;
                tagData1 = new FluidMoveBehaviorBaseNoLeak.TagData()
                {
                    ParentRect = Rect.Empty,
                    AppRect = Rect.Empty,
                    Parent = newTagData.Parent,
                    Child = child,
                    Timestamp = DateTime.Now,
                    InitialTag = initialIdentityTag
                };
                FluidMoveBehaviorBaseNoLeak.TagDictionary.Add(tag, tagData1);
            }
            else if (tagData1.Parent != VisualTreeHelper.GetParent((DependencyObject)child))
            {
                rect = FluidMoveBehaviorBaseNoLeak.TranslateRect(tagData1.AppRect, root, newTagData.Parent);
                flag1 = true;
            }
            else
                rect = tagData1.ParentRect;
            FrameworkElement originalChild = child;
            if (!FluidMoveBehaviorNoLeak.IsEmptyRect(rect) && !FluidMoveBehaviorNoLeak.IsEmptyRect(newTagData.ParentRect) && (!FluidMoveBehaviorNoLeak.IsClose(rect.Left, newTagData.ParentRect.Left) || !FluidMoveBehaviorNoLeak.IsClose(rect.Top, newTagData.ParentRect.Top)) || child != tagData1.Child && FluidMoveBehaviorNoLeak.TransitionStoryboardDictionary.ContainsKey(tag))
            {
                Rect currentRect = rect;
                bool flag3 = false;
                Storyboard storyboard = (Storyboard)null;
                if (FluidMoveBehaviorNoLeak.TransitionStoryboardDictionary.TryGetValue(tag, out storyboard))
                {
                    object overlay1 = FluidMoveBehaviorNoLeak.GetOverlay((DependencyObject)tagData1.Child);
                    AdornerContainer adornerContainer = (AdornerContainer)overlay1;
                    flag3 = overlay1 != null;
                    FrameworkElement child1 = tagData1.Child;
                    if (overlay1 != null)
                    {
                        Canvas canvas = adornerContainer.Child as Canvas;
                        if (canvas != null)
                            child1 = canvas.Children[0] as FrameworkElement;
                    }
                    if (!usingBeforeLoaded)
                        currentRect = FluidMoveBehaviorNoLeak.GetTransform(child1).TransformBounds(currentRect);
                    FluidMoveBehaviorNoLeak.TransitionStoryboardDictionary.Remove(tag);
                    storyboard.Stop();
                    FluidMoveBehaviorNoLeak.RemoveTransform(child1);
                    if (overlay1 != null)
                    {
                        AdornerLayer.GetAdornerLayer((Visual)root).Remove((Adorner)adornerContainer);
                        FluidMoveBehaviorNoLeak.TransferLocalValue(tagData1.Child, FluidMoveBehaviorNoLeak.CacheDuringOverlayProperty, UIElement.RenderTransformProperty);
                        FluidMoveBehaviorNoLeak.SetOverlay((DependencyObject)tagData1.Child, (object)null);
                    }
                }
                object overlay = (object)null;
                if (flag3 || flag1 && this.FloatAbove)
                {
                    Canvas canvas1 = new Canvas();
                    canvas1.Width = newTagData.ParentRect.Width;
                    canvas1.Height = newTagData.ParentRect.Height;
                    canvas1.IsHitTestVisible = false;
                    Canvas canvas2 = canvas1;
                    Rectangle rectangle1 = new Rectangle();
                    rectangle1.Width = newTagData.ParentRect.Width;
                    rectangle1.Height = newTagData.ParentRect.Height;
                    rectangle1.IsHitTestVisible = false;
                    Rectangle rectangle2 = rectangle1;
                    rectangle2.Fill = (Brush)new VisualBrush((Visual)child);
                    canvas2.Children.Add((UIElement)rectangle2);
                    AdornerContainer adornerContainer = new AdornerContainer((UIElement)child)
                    {
                        Child = (UIElement)canvas2
                    };
                    overlay = (object)adornerContainer;
                    FluidMoveBehaviorNoLeak.SetOverlay((DependencyObject)originalChild, overlay);
                    AdornerLayer.GetAdornerLayer((Visual)root).Add((Adorner)adornerContainer);
                    FluidMoveBehaviorNoLeak.TransferLocalValue(child, UIElement.RenderTransformProperty, FluidMoveBehaviorNoLeak.CacheDuringOverlayProperty);
                    child.RenderTransform = (Transform)new TranslateTransform(-10000.0, -10000.0);
                    canvas2.RenderTransform = (Transform)new TranslateTransform(10000.0, 10000.0);
                    child = (FrameworkElement)rectangle2;
                }
                Rect parentRect = newTagData.ParentRect;
                Storyboard transitionStoryboard = this.CreateTransitionStoryboard(child, usingBeforeLoaded, ref parentRect, ref currentRect);
                FluidMoveBehaviorNoLeak.TransitionStoryboardDictionary.Add(tag, transitionStoryboard);
                transitionStoryboard.Completed += (EventHandler)((sender, e) =>
                {
                    Storyboard local_0;
                    if (!FluidMoveBehaviorNoLeak.TransitionStoryboardDictionary.TryGetValue(tag, out local_0) || local_0 != transitionStoryboard)
                        return;
                    FluidMoveBehaviorNoLeak.TransitionStoryboardDictionary.Remove(tag);
                    transitionStoryboard.Stop();
                    FluidMoveBehaviorNoLeak.RemoveTransform(child);
                    child.InvalidateMeasure();
                    if (overlay == null)
                        return;
                    AdornerLayer.GetAdornerLayer((Visual)root).Remove((Adorner)overlay);
                    FluidMoveBehaviorNoLeak.TransferLocalValue(originalChild, FluidMoveBehaviorNoLeak.CacheDuringOverlayProperty, UIElement.RenderTransformProperty);
                    FluidMoveBehaviorNoLeak.SetOverlay((DependencyObject)originalChild, (object)null);
                });
                transitionStoryboard.Begin();
            }
            tagData1.ParentRect = newTagData.ParentRect;
            tagData1.AppRect = newTagData.AppRect;
            tagData1.Parent = newTagData.Parent;
            tagData1.Child = newTagData.Child;
            tagData1.Timestamp = newTagData.Timestamp;
        }

        private Storyboard CreateTransitionStoryboard(FrameworkElement child, bool usingBeforeLoaded, ref Rect layoutRect, ref Rect currentRect)
        {
            Duration duration = this.Duration;
            Storyboard storyboard = new Storyboard();
            storyboard.Duration = duration;
            double num1 = !usingBeforeLoaded || layoutRect.Width == 0.0 ? 1.0 : currentRect.Width / layoutRect.Width;
            double num2 = !usingBeforeLoaded || layoutRect.Height == 0.0 ? 1.0 : currentRect.Height / layoutRect.Height;
            double num3 = currentRect.Left - layoutRect.Left;
            double num4 = currentRect.Top - layoutRect.Top;
            FluidMoveBehaviorNoLeak.AddTransform(child, (Transform)new TransformGroup()
            {
                Children = {
          (Transform) new ScaleTransform()
          {
            ScaleX = num1,
            ScaleY = num2
          },
          (Transform) new TranslateTransform()
          {
            X = num3,
            Y = num4
          }
        }
            });
            string str = "(FrameworkElement.RenderTransform).";
            TransformGroup transformGroup = child.RenderTransform as TransformGroup;
            if (transformGroup != null && FluidMoveBehaviorNoLeak.GetHasTransformWrapper((DependencyObject)child))
                str = string.Concat(new object[4]
        {
          (object) str,
          (object) "(TransformGroup.Children)[",
          (object) (transformGroup.Children.Count - 1),
          (object) "]."
        });
            if (usingBeforeLoaded)
            {
                if (num1 != 1.0)
                {
                    DoubleAnimation doubleAnimation1 = new DoubleAnimation();
                    doubleAnimation1.Duration = duration;
                    doubleAnimation1.From = new double?(num1);
                    doubleAnimation1.To = new double?(1.0);
                    DoubleAnimation doubleAnimation2 = doubleAnimation1;
                    Storyboard.SetTarget((DependencyObject)doubleAnimation2, (DependencyObject)child);
                    Storyboard.SetTargetProperty((DependencyObject)doubleAnimation2, new PropertyPath(str + "(TransformGroup.Children)[0].(ScaleTransform.ScaleX)", new object[0]));
                    doubleAnimation2.EasingFunction = this.EaseX;
                    storyboard.Children.Add((Timeline)doubleAnimation2);
                }
                if (num2 != 1.0)
                {
                    DoubleAnimation doubleAnimation1 = new DoubleAnimation();
                    doubleAnimation1.Duration = duration;
                    doubleAnimation1.From = new double?(num2);
                    doubleAnimation1.To = new double?(1.0);
                    DoubleAnimation doubleAnimation2 = doubleAnimation1;
                    Storyboard.SetTarget((DependencyObject)doubleAnimation2, (DependencyObject)child);
                    Storyboard.SetTargetProperty((DependencyObject)doubleAnimation2, new PropertyPath(str + "(TransformGroup.Children)[0].(ScaleTransform.ScaleY)", new object[0]));
                    doubleAnimation2.EasingFunction = this.EaseY;
                    storyboard.Children.Add((Timeline)doubleAnimation2);
                }
            }
            if (num3 != 0.0)
            {
                DoubleAnimation doubleAnimation1 = new DoubleAnimation();
                doubleAnimation1.Duration = duration;
                doubleAnimation1.From = new double?(num3);
                doubleAnimation1.To = new double?(0.0);
                DoubleAnimation doubleAnimation2 = doubleAnimation1;
                Storyboard.SetTarget((DependencyObject)doubleAnimation2, (DependencyObject)child);
                Storyboard.SetTargetProperty((DependencyObject)doubleAnimation2, new PropertyPath(str + "(TransformGroup.Children)[1].(TranslateTransform.X)", new object[0]));
                doubleAnimation2.EasingFunction = this.EaseX;
                storyboard.Children.Add((Timeline)doubleAnimation2);
            }
            if (num4 != 0.0)
            {
                DoubleAnimation doubleAnimation1 = new DoubleAnimation();
                doubleAnimation1.Duration = duration;
                doubleAnimation1.From = new double?(num4);
                doubleAnimation1.To = new double?(0.0);
                DoubleAnimation doubleAnimation2 = doubleAnimation1;
                Storyboard.SetTarget((DependencyObject)doubleAnimation2, (DependencyObject)child);
                Storyboard.SetTargetProperty((DependencyObject)doubleAnimation2, new PropertyPath(str + "(TransformGroup.Children)[1].(TranslateTransform.Y)", new object[0]));
                doubleAnimation2.EasingFunction = this.EaseY;
                storyboard.Children.Add((Timeline)doubleAnimation2);
            }
            return storyboard;
        }

        private static void AddTransform(FrameworkElement child, Transform transform)
        {
            TransformGroup transformGroup = child.RenderTransform as TransformGroup;
            if (transformGroup == null)
            {
                transformGroup = new TransformGroup();
                transformGroup.Children.Add(child.RenderTransform);
                child.RenderTransform = (Transform)transformGroup;
                FluidMoveBehaviorNoLeak.SetHasTransformWrapper((DependencyObject)child, true);
            }
            transformGroup.Children.Add(transform);
        }

        private static Transform GetTransform(FrameworkElement child)
        {
            TransformGroup transformGroup = child.RenderTransform as TransformGroup;
            if (transformGroup != null && transformGroup.Children.Count > 0)
                return transformGroup.Children[transformGroup.Children.Count - 1];
            else
                return (Transform)new TranslateTransform();
        }

        private static void RemoveTransform(FrameworkElement child)
        {
            TransformGroup transformGroup = child.RenderTransform as TransformGroup;
            if (transformGroup == null)
                return;
            if (FluidMoveBehaviorNoLeak.GetHasTransformWrapper((DependencyObject)child))
            {
                child.RenderTransform = transformGroup.Children[0];
                FluidMoveBehaviorNoLeak.SetHasTransformWrapper((DependencyObject)child, false);
            }
            else
                transformGroup.Children.RemoveAt(transformGroup.Children.Count - 1);
        }

        private static void TransferLocalValue(FrameworkElement element, DependencyProperty source, DependencyProperty dest)
        {
            object obj = element.ReadLocalValue(source);
            BindingExpressionBase bindingExpressionBase = obj as BindingExpressionBase;
            if (bindingExpressionBase != null)
                element.SetBinding(dest, bindingExpressionBase.ParentBindingBase);
            else if (obj == DependencyProperty.UnsetValue)
                element.ClearValue(dest);
            else
                element.SetValue(dest, element.GetAnimationBaseValue(source));
            element.ClearValue(source);
        }

        private static bool IsClose(double a, double b)
        {
            return Math.Abs(a - b) < 1E-07;
        }

        private static bool IsEmptyRect(Rect rect)
        {
            if (!rect.IsEmpty && !double.IsNaN(rect.Left))
                return double.IsNaN(rect.Top);
            else
                return true;
        }
    }
}
