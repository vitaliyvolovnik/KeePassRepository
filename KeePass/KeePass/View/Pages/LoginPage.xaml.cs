using BLL.Services;
using KeePass.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {


        private readonly UserService _userService;


        private event Action<int> _onLogin;

        public LoginPage(UserService userService, Action<int> onLogin)
        {
            InitializeComponent();
            _userService = userService;
            _onLogin += onLogin;
            LoginBtn.Command = new AsyncRelayCommand(Login_Btn_ExexuteAsync, Login_Btn_CanExecute);
        }


        public async Task Login_Btn_ExexuteAsync(object param)
        {
                    var id = (await _userService.LoginAsync(MasterPasswordPasswordBox.Password))?.Id ?? 0;
            if (id == 0)
                MessageBox.Show("Errr");
            else
                _onLogin.Invoke(id);
        }

        public bool Login_Btn_CanExecute(object param) => Regex.IsMatch(MasterPasswordPasswordBox.Password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*\\W).{8,}$");




    }
}
