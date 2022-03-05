using Microsoft.Xaml.Behaviors;
using System.Collections.Generic;
using System.Windows;

namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    public class BehaviorBinding : Behavior<DependencyObject>
    {
        private static readonly DependencyProperty BehaviorsProperty =
            DependencyProperty.Register("Behaviors", typeof(List<Behavior>), typeof(BehaviorBinding), new PropertyMetadata(new PropertyChangedCallback(BehaviorsChangedCallback)));
        public List<Behavior> Behaviors
        {
            get { return (List<Behavior>)GetValue(BehaviorsProperty); }
            set { SetValue(BehaviorsProperty, value); }
        }
        private static void BehaviorsChangedCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var objectBehaviors = Interaction.GetBehaviors((d as BehaviorBinding).AssociatedObject);

            if(e.OldValue != null)
            {
                foreach (var b in (List<Behavior>)e.OldValue)
                {
                    if (objectBehaviors.Contains(b))
                    {
                        objectBehaviors.Remove(b); 
                    }
                }
            }

            if (e.NewValue != null)
            {
                foreach (var b in (List<Behavior>)e.NewValue)
                {
                    if (!objectBehaviors.Contains(b))
                    {
                        objectBehaviors.Add(b); 
                    }
                }
            }
        }
    }
}
