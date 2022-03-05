using Microsoft.Xaml.Behaviors;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    public class ScrollIntoViewBehaviour : Behavior<ListBox>
    {
        #region Overrides
        protected override void OnAttached()
        {
            base.OnAttached();
            
            (this.AssociatedObject.Items as INotifyCollectionChanged).CollectionChanged += AssociatedObject_CollectionChanged;
        }

        protected override void OnDetaching()
        {
            (this.AssociatedObject.Items as INotifyCollectionChanged).CollectionChanged -= AssociatedObject_CollectionChanged;

            base.OnDetaching();
        }
        #endregion


        #region Methods
        private static ScrollViewer GetScrollViewerOfListBox(DependencyObject listBox)
        {
            // Iterate through all immediate children
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(listBox); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(listBox, i);

                if (child != null && child is ScrollViewer)
                {
                    return (ScrollViewer)child;
                }
                else
                {
                    ScrollViewer childOfChild = GetScrollViewerOfListBox(child);

                    if (childOfChild != null)
                        return childOfChild;
                }
            }

            return null;
        }
        #endregion


        #region Event-Handler
        private void AssociatedObject_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (e.Action != NotifyCollectionChangedAction.Move && e.Action != NotifyCollectionChangedAction.Add)
            {
                return;
            }

            VirtualizingStackPanel vsp = (VirtualizingStackPanel)typeof(ItemsControl).InvokeMember("_itemsHost",
                                         System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.NonPublic, null,
                                         this.AssociatedObject, null);
            if (vsp != null && vsp.ScrollOwner != null)
            {
                double scrollHeight = vsp.ScrollOwner.ScrollableHeight;
                double offset = scrollHeight * e.NewStartingIndex / this.AssociatedObject.Items.Count;

                vsp.SetVerticalOffset(offset);
            }
        }
        #endregion
    }
}
