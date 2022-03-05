using Microsoft.Xaml.Behaviors;
using System.Windows;

namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    /// <summary>
    /// What does it do:  You can add Behavior(s) within a Style (it doesn't delete any Behavior)
    /// How to use:       Just take "BehaviorSetter.Behavior" as Property of a normal Style Setter and enter your Behavior as Value
    /// Condition:        The Behavior you want to set has to override the method Freezable.CloneCore(), see MaxValueValidationBehavior for example (otherwise spezific attributes would vanish while the behavior is set)
    /// </summary>
    public class BehaviorSetter
    {
        private static readonly DependencyProperty BehaviorProperty =
            DependencyProperty.RegisterAttached("Behavior", typeof(Behavior), typeof(BehaviorSetter), new UIPropertyMetadata(BehaviorSetter.BehaviorChanged));
        public static Behavior GetBehavior(DependencyObject obj)
        {
            return (Behavior)obj.GetValue(BehaviorProperty);
        }
        public static void SetBehavior(DependencyObject obj, Behavior value)
        {
            obj.SetValue(BehaviorProperty, value);
        }


        private static void BehaviorChanged(DependencyObject target, DependencyPropertyChangedEventArgs e)
        {
            if(e.NewValue == null)
            {
                return;
            }

            var objectBehaviors = Interaction.GetBehaviors(target);

            // All Behaviors in e.NewValue are sealed/frozen -> you have to clone them
            objectBehaviors.Add((e.NewValue as Freezable).Clone() as Behavior);
        }
    }
}
