using System.ComponentModel;
using System.IO;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using CefSharp.WinForms;
using System.Windows;
using CefSharp;
using FrameworksAndDrivers.Gui.Wpf.CefUtils;
using FrameworksAndDrivers.Gui.Wpf.Interfaces;
using FrameworksAndDrivers.Gui.Wpf.ViewModel.HtmlViewModel;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for ManufacturerView.xaml
    /// </summary>
    public partial class ClassicTestHtmlView : Window, ICanClose
    {
        #region Event-Handler

        private void ClassicTestHtmlView_ReloadHtmlRequest(object sender, string html)
        {
            this.BrowserContainer.Child = null;
            _browser.Dispose();
            _browser = null;
            _viewModel.FillTranslation();
            InitBrowser();
        }
        #endregion

        private ClassicTestHtmlViewModelBase _viewModel;
        private ChromiumWebBrowser _browser;

        public ClassicTestHtmlView(ClassicTestHtmlViewModelBase classicTestHtmlViewViewModel)
		{
            InitializeComponent();
            _viewModel = classicTestHtmlViewViewModel;
            DataContext = _viewModel;

            InitBrowser();
           
            _viewModel.ReloadHtmlRequest += ClassicTestHtmlView_ReloadHtmlRequest;
            _viewModel.ShowPrintDialog += _viewModel_ShowPrintDialog;
        }

        private void InitBrowser()
        {
            var browser = new ChromiumWebBrowser("");
            const string resourcename = "http://classicTest.html";
            var memorystream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(_viewModel.HtmlDocument));
            browser.RegisterResourceHandler(resourcename, memorystream);
            browser.Load(resourcename);
            browser.UnRegisterResourceHandler(resourcename);

            browser.MenuHandler = new NoMenuEventHandler();
            this.BrowserContainer.Child = browser;
            _browser = browser;
        }

        private void _viewModel_ShowPrintDialog(object sender, System.EventArgs e)
        {
            _browser.Print();
        }


        public bool CanClose()
        {
            if (DataContext is ICanClose canClose)
            {
                return canClose.CanClose();
            }
            else
            {
                return true;
            }
        }

        private void ClassicTestHtmlView_OnClosing(object sender, CancelEventArgs e)
        {
            _browser.Dispose();
            BrowserContainer.Dispose();
        }
    }
}
