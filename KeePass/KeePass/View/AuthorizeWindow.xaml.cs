using BLL.Services;
using KeePass.View.Pages;
using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
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

namespace KeePass.View
{
    /// <summary>
    /// Interaction logic for AuthorizeWindow.xaml
    /// </summary>
    public partial class AuthorizeWindow : Window
    {


        public bool IsOk { get;private set; } =false;
        public int UserId { get; set; } = 0;

        private UserService _userService;

        public AuthorizeWindow(UserService userService)
        {
            InitializeComponent();
            _userService = userService;

            InitializeAsync();
        }

        private async Task InitializeAsync()
        {
            bool isRegistered = await _userService.isRegisteredAsync();

            if (isRegistered)
            {
                MainFrame.Navigate(new LoginPage(_userService, onAuthorize));
            }
            else
            {
                MainFrame.Navigate(new RegisterPage(_userService, onAuthorize));
            }
        }

        public void onAuthorize(int id)
        {
            UserId = id;

            IsOk = true;
            this.Close();
        }
    }
}
