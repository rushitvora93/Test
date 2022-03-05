using System.Windows;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    public static class HideUnusedPartsUtil
    {
        public static Visibility UnusedFunctionVisibility { get; private set; }


        static HideUnusedPartsUtil()
        {
            UnusedFunctionVisibility = FeatureToggles.FeatureToggles.HideUnusedParts ? Visibility.Collapsed : Visibility.Visible;
        }
    }
}
