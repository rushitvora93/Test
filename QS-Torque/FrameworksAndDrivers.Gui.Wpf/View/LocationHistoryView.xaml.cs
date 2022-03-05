using Core;
using Core.Entities;
using FrameworksAndDrivers.Gui.Wpf.Model;
using FrameworksAndDrivers.Gui.Wpf.TreeStructure;
using FrameworksAndDrivers.Gui.Wpf.ViewModel;
using FrameworksAndDrivers.Localization;
using InterfaceAdapters.Models;
using Syncfusion.Pdf.Parsing;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.Windows.PdfViewer;
using Syncfusion.Windows.Tools.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FrameworksAndDrivers.Gui.Wpf.View
{
    /// <summary>
    /// Interaction logic for LocationHistoryView.xaml
    /// </summary>
    public partial class LocationHistoryView : UserControl
    {
        private LocationHistoryViewModel _viewModel;
        private LocalizationWrapper _localization;

        private ExtensionTreeStructure<LocationModel, long> _locationTreeStructure;
        private ElementTree<LocationDirectoryHumbleModel> _locationDirectoryTreeStructure;
        private TreeViewItemAdv _selectedTreeItem;
        private ILocationDisplayFormatter _locationDisplayFormatter;

        public TreeViewItemAdv SelectedTreeItem
        {
            get => _selectedTreeItem;
            set
            {
                _selectedTreeItem = value;
                if (value is StructureTreeViewItemAdv<LocationModel> locationDisplayMember)
                {
                    _viewModel.SelectedLocation = locationDisplayMember.DisplayMember.Item;
                    _viewModel.SelectedDirectory = null;
                }
                else if (value is StructureTreeViewItemAdv<LocationDirectoryHumbleModel> directoryDisplayMember)
                {
                    _viewModel.SelectedLocation = null;
                    _viewModel.SelectedDirectory = directoryDisplayMember.DisplayMember.Item;
                }
            }
        }


        public LocationHistoryView(LocationHistoryViewModel viewModel, LocalizationWrapper localization, ILocationDisplayFormatter locationDisplayFormatter)
        {
            InitializeComponent();
            this.DataContext = _viewModel = viewModel;
            _localization = localization;
            _locationDisplayFormatter = locationDisplayFormatter;

            ChangesDataGrid.LocalizationWrapper = _localization;
            _localization.Subscribe(ChangesDataGrid);
            _viewModel.SetGuiDospatcher(this.Dispatcher);

            _viewModel.InitializeLocationTreeRequest += ViewModel_InitializeLocationTreeRequest;
            _viewModel.MessageBoxRequest += ViewModel_MessageBoxRequest;
        }

        private void ViewModel_MessageBoxRequest(object sender, EventArgs.MessageBoxEventArgs e)
        {
            e.Show();
        }

        private void ViewModel_InitializeLocationTreeRequest(object sender, System.EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                _locationDirectoryTreeStructure = new ElementTree<LocationDirectoryHumbleModel>(_viewModel.LocationTree,
                    x => x.Name,
                    LocationTree,
                    new BitmapImage(new Uri("pack://application:,,,/Resources;component/Icons/folder.png")),
                    FeatureToggles.FeatureToggles.Pikachu_532_SortLocationTree,
                    new List<string> { nameof(LocationDirectoryHumbleModel.ParentId) });

                var directoryToDirectoryIdTreeConverter = new TreeConverter<long, LocationDirectoryHumbleModel>(
                    _locationDirectoryTreeStructure,
                    (id, dir) => dir.Id == id,
                    id => new LocationDirectoryHumbleModel(new LocationDirectory()) { Id = id });

                _locationTreeStructure = new ExtensionTreeStructure<LocationModel, long>(
                    _viewModel.LocationTree.LocationModels,
                    new LeafLevel<LocationModel>(x => new DisplayMemberModel<LocationModel>(x, y => _locationDisplayFormatter.Format(y.Entity)),
                        new BitmapImage(new Uri("pack://application:,,,/Resources;component/Icons/screw2.png"))),
                    directoryToDirectoryIdTreeConverter,
                    x => x.ParentId,
                    new List<string>() { nameof(LocationModel.ParentId) },
                    LocationTree,
                    true,
                    FeatureToggles.FeatureToggles.Pikachu_532_SortLocationTree);

                LocationTree.ItemsSource = _locationDirectoryTreeStructure.Source;
            });
        }

        private void PrintButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement if QSTV8E-324 is done
            //var options = new PdfExportingOptions();
            //options.ExportDetailsView = true;
            //var document = ChangesDataGrid.ExportToPdf(options);
            //var pdfViewer = new PdfViewerControl();
            //var stream = new MemoryStream();
            //document.Save(stream);
            //var ldoc = new PdfLoadedDocument(stream);
            //pdfViewer.Load(ldoc);
            //// if you want to  show the pdf viewer window. Please enable the below line,
            ////MainWindow pdfPage = new MainWindow();
            ////pdfPage.Content = pdfViewer;
            ////pdfPage.Show();
            //pdfViewer.Print(true);
        }
    }
}
