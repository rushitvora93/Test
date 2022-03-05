using System.IO;
using System.Windows;
using Microsoft.Win32;
using UnlockTool.DataAccess;
using UnlockTool.UseCases;
using UnlockTool.Views;
using UnlockToolShared.DataAccess;
using UnlockToolShared.UseCases;

namespace UnlockerTool
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel vm;
        public MainWindow()
        {
            InitializeComponent();
            vm = this.DataContext as MainWindowViewModel;
            IUnlockRequestDataAccess dareq = new UnlockRequestDataAccess();
            vm.UnlockRequestUseCase = new UnlockRequestUseCase(dareq, vm);
            IUnlockResponseWriteDataAccess daresw = new UnlockResponseWriteDataAccess();
            IUnlockResponseReadDataAccess daresr = new UnlockResponseReadDataAccess();
            vm.UnlockResponseUseCase = new UnlockResponseUseCase(daresw, daresr, vm);
        }

        private void OpenFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ReadOnlyChecked = true;
            dialog.CheckFileExists = true;
            if (dialog.ShowDialog() == true)
            {
                using (var fs = File.OpenRead(dialog.FileName))
                {
                    vm.LoadFile(fs);
                }
                
            }
        }

        private void SaveFileButton_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog(); ;
            dialog.OverwritePrompt = true;
            if (dialog.ShowDialog() == true)
            {
                using (var fs = File.Create(dialog.FileName))
                {
                    vm.SaveFile(fs);
                }
            }
        }

        private void OpenResponseFileButton_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ReadOnlyChecked = true;
            dialog.CheckFileExists = true;
            if (dialog.ShowDialog() == true)
            {
                using (var fs = File.OpenRead(dialog.FileName))
                {
                    vm.LoadResponseFile(fs);
                }

            }
        }
    }
}
