using System;
using System.Collections.Generic;

namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    public class ConditionValidationBehavior : ValidationBehavior
    {
        public Predicate<string> Condition { get; set; }


        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.TextChanged += ValidateValue;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.TextChanged -= ValidateValue;
            base.OnDetaching();
        }


        private void ValidateValue(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (Condition(this.AssociatedObject.Text))
            {
                this.ShowWarning();
            }
            else
            {
                this.HideWarning();
            }
        }
    }
    public class ConditionsValidationBehavior : ValidationBehavior
    {
        public List<ConditionValidationBehavior> Conditions { get; set; }


        protected override void OnAttached()
        {
            base.OnAttached();
            this.AssociatedObject.TextChanged += ValidateValue;
        }

        protected override void OnDetaching()
        {
            this.AssociatedObject.TextChanged -= ValidateValue;
            base.OnDetaching();
        }


        private void ValidateValue(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            foreach (var condition in Conditions)
            {
                if (condition.Condition(this.AssociatedObject.Text))
                {
                    this.WarningText = condition.WarningText;
                    this.ShowWarning();
                    return;
                }
            }
            this.HideWarning();
        }
    }
}
