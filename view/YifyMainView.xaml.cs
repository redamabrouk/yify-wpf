using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Yify.viewmodel;

namespace Yify.view
{
    /// <summary>
    /// Interaction logic for YifyMainView.xaml
    /// </summary>
    public partial class YifyMainView : Window
    {
        public YifyMainView()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as YifyMoviesViewModel;
            vm.Search();

        }
    }
}
