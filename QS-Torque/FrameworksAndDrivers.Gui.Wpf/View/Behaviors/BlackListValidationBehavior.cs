namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    public class BlackListValidationBehavior : ValidationBehavior
    {
        public string BlackList { get; set; }

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
            if (string.IsNullOrWhiteSpace(this.AssociatedObject.Text))
            {
                return;
            }

            foreach (var change in e.Changes)
            {
                if(change.AddedLength == 0)
                {
                    continue;
                }

                var changedString = this.AssociatedObject.Text.Substring(change.Offset, change.AddedLength);

                foreach(var c in changedString)
                {
                    if(BlackList.Contains(c))
                    {
                        this.ShowWarning();
                        break;
                    }
                    else
                    {
                        this.HideWarning();
                    }
                }
            }
        }
    }
}
