using BLL.Services;
using Domain.Models;
using KeePass.View;
using KeePass.View.Pages;
using KeePass.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace KeePass
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User _currentUser;
        public MainWindow(AuthorizeWindow window, ExplorerPage explorerPage, User currentUser, CollectionViewModel collectionViewModel)
        {
            InitializeComponent();

            this._currentUser = currentUser;

            Authorize(window);

            explorerPage.OnCollectionChangeSubscribe(collectionViewModel.ChangeCollection);
            
            CollectionFrame.Navigate(new CollectionPage(collectionViewModel));
            ExplorerFrame.Navigate(explorerPage);
        }


        public void Authorize(AuthorizeWindow window)
        {
            window.ShowDialog();

            if (window.IsOk)
            {
                _currentUser.Id = window.UserId;
            }
            else
            {
                MessageBox.Show("cannot work without autorization", "authorize");
                this.Close();
            }
        }


        

    }
}
