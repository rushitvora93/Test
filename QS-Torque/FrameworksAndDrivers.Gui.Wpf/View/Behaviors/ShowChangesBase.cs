using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.View.Themes;
using State;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace FrameworksAndDrivers.Gui.Wpf.View.Behaviors
{
    public abstract class ShowChangesBase<T> : BehaviorBase<T> where T : Control
    {
        private const string ChangedFieldBrushKey = "ChangedFieldBrush";
        protected const string DefaultChangedFieldColor = "#FF21409A";


        #region Properties
        /// <summary>
        /// Value according to which the new value will changed (-> NewValue == BaseValue: not changed)
        /// </summary>
        protected object BaseValue { get; set; }
        
        private Brush _changedBorderBrush;

        private static readonly DependencyProperty ClearShownChangesParentProperty =
            DependencyProperty.Register("ClearShownChangesParent", typeof(IClearShownChanges), typeof(ShowChangesBase<T>), new PropertyMetadata(new PropertyChangedCallback(ClearShownChangesParentChangeCallback)));
        public IClearShownChanges ClearShownChangesParent
        {
            get { return (IClearShownChanges)GetValue(ClearShownChangesParentProperty); }
            set { SetValue(ClearShownChangesParentProperty, value); }
        }
        private static void ClearShownChangesParentChangeCallback(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as ShowChangesBase<T>).ClearShownChangesParentChanged(e.OldValue as IClearShownChanges, e.NewValue as IClearShownChanges);
        }
        #endregion

        public ShowChangesBase() : base()
        {
            // Get default Border
            var themeDictionary = Application.Current.Resources[ResourceKeys.ApplicationThemeDictionaryKey] as ThemeDictionary;
            _changedBorderBrush = themeDictionary[ChangedFieldBrushKey] as Brush ?? new SolidColorBrush((Color)ColorConverter.ConvertFromString(DefaultChangedFieldColor));
        }

        #region Methods
        protected void ShowControlAsChanged()
        {
            if(this.AssociatedObject == null)
            {
                return;
            }
            
            this.AssociatedObject.BorderBrush = _changedBorderBrush;
        }

        protected void ShowControlAsNormal()
        {
            if (this.AssociatedObject == null)
            {
                return;
            }

            this.AssociatedObject.BorderBrush = null;
        }
        protected override void OnSetup()
        {
            base.OnSetup();
            (Application.Current.Resources[ResourceKeys.ApplicationThemeDictionaryKey] as ThemeDictionary).ThemeChanged += ThemeDictionaryOnThemeChanged;
        }


        protected override void OnCleanup()
        {
            (Application.Current.Resources[ResourceKeys.ApplicationThemeDictionaryKey] as ThemeDictionary).ThemeChanged -= ThemeDictionaryOnThemeChanged;
            base.OnCleanup();
        }

        protected virtual void ClearShownChangesParentChanged(IClearShownChanges oldValue, IClearShownChanges newValue)
        {
            if (oldValue != null)
            {
                oldValue.ClearShownChanges -= OnClearShownChanges; 
            }

            if (newValue != null)
            {
                newValue.ClearShownChanges += OnClearShownChanges; 
            }
        }
        #endregion

        #region EventHandlers
        private void OnClearShownChanges(object sender, System.EventArgs e)
        {
            Dispatcher.Invoke(() => ShowControlAsNormal() );
        }

        private void ThemeDictionaryOnThemeChanged(object sender, Theme e)
        {
            var themeDictionary = Application.Current.Resources[ResourceKeys.ApplicationThemeDictionaryKey] as ThemeDictionary;
            _changedBorderBrush = themeDictionary[ChangedFieldBrushKey] as Brush ?? new SolidColorBrush((Color)ColorConverter.ConvertFromString(DefaultChangedFieldColor));
        }
        #endregion

    }
}
