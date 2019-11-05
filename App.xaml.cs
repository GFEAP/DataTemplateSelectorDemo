using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace TemplateSelectorDemo
{
    public partial class App : Application
    {

        private void DemoStartup(object sender, StartupEventArgs e)
        {
            var win = new View.MainWindow();
            var vm = new ViewModel.MainWindowViewModel();
            vm.SetCloseAction(win.Close);
            win.DataContext = vm;
            win.Show();
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
        }
    }
}
