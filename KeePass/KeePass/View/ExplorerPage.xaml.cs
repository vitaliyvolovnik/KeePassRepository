using Domain.Models;
using KeePass.Models;
using KeePass.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KeePass.View
{
    /// <summary>
    /// Interaction logic for ExplorerPage.xaml
    /// </summary>
    public partial class ExplorerPage : Page
    {

        private readonly ExplorerViewModel _viewModel;

        public ExplorerPage(ExplorerViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            this.DataContext = _viewModel;



        }
    }
}
