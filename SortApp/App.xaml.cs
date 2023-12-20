using System;
using System.Windows;

namespace SortApp
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            MainWindow mainWindow = new MainWindow();
            MainWindowModel mainModel = new MainWindowModel();
            MainWindowViewModel viewModel = new MainWindowViewModel(mainModel);
            mainWindow.DataContext = viewModel;

            mainWindow.Show();
        }
    }
}
