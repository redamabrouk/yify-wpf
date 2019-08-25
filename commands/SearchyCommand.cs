using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Yify.viewmodel;

namespace Yify.commands
{
    internal class SearchyCommand:ICommand
    {
        YifyMoviesViewModel _YifyMoviesViewModel;
        public SearchyCommand(YifyMoviesViewModel yifyMoviesViewModel)
        {
            _YifyMoviesViewModel = yifyMoviesViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            _YifyMoviesViewModel.Search();
        }
    }
}
