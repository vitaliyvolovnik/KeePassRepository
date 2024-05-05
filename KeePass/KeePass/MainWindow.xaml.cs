using BLL.Services;
using BLL.Services.Interfaces;
using Domain.Models;
using KeePass.Domain;
using KeePass.View;
using KeePass.View.Pages;
using KeePass.ViewModel;
using MaterialDesignThemes.Wpf;
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
        private IUserService _userService;
        public MainWindow(ExplorerPage explorerPage, User currentUser, CollectionViewModel collectionViewModel, IUserService userService)
        {
            InitializeComponent();

            this._currentUser = currentUser;
            _userService = userService;

            Initialize(explorerPage, collectionViewModel);


        }

        private async void Initialize(ExplorerPage explorerPage, CollectionViewModel collectionViewModel)
        {
            await Authorize();

            explorerPage.OnCollectionChangeSubscribe(collectionViewModel.ChangeCollection);

            CollectionFrame.Navigate(new CollectionPage(collectionViewModel));
            ExplorerFrame.Navigate(explorerPage);
        }
        public async Task Authorize()
        {

            bool isRegistered = await _userService.IsRegisteredAsync();
            var result = false;
            if (!isRegistered)
            {
                var regDialog = new RegisterDialog();

                result = false;

                result = (bool)(await DialogHost.Show(regDialog, "RootDialog") ?? false);
                if (!result)
                {
                    Close();
                }
                await _userService.RegisterAsync(regDialog.Password);
            }

            var logDialog = new LoginControl();
            result = false;
            while (true)
            {
                result = (bool)(await DialogHost.Show(logDialog, "RootDialog") ?? false);

                if (!result)
                {
                    Close();
                }
                var user = await _userService.LoginAsync(logDialog.Password);

                if (user is not null)
                {
                    _currentUser.MasterPassword = user.MasterPassword;
                    _currentUser.Id = user.Id;

                    break;
                }

                var infDialog = new InformationDialog();
                infDialog.MessageTxt = "Entered incorrect password";

                await DialogHost.Show(infDialog, "RootDialog");

            }




        }




    }
}
