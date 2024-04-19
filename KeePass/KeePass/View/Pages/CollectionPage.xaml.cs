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

namespace KeePass.View.Pages
{
    /// <summary>
    /// Interaction logic for CollectionPage.xaml
    /// </summary>
    public partial class CollectionPage : Page
    {
        private readonly CollectionViewModel _collectionViewModel;

        public CollectionPage(CollectionViewModel collectionViewModel)
        {
            this._collectionViewModel = collectionViewModel;
            this.DataContext = this._collectionViewModel;

            InitializeComponent();
        }
    }
}
