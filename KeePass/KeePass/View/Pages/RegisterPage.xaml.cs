using BLL.Services;
using BLL.Services.Interfaces;
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
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Page
    {


        private readonly UserService _userService;

        private event Action<int> _onRegister;

        public RegisterPage(UserService userService, Action<int> onRegister)
        {
            InitializeComponent();

            _userService = userService;
            _onRegister = onRegister;
            RegisterBtn.Command = new AsyncRelayCommand(Register_Btn_ExexuteAsync, Register_Btn_CanExecute);
        }



        public async Task Register_Btn_ExexuteAsync(object param)
        {
            var id = (await _userService.RegisterAsync(MasterPasswordPasswordBox.Password))?.Id ?? 0;
            if (id == 0)
                MessageBox.Show("Err");
            else
                _onRegister.Invoke(id);
        }

        public bool Register_Btn_CanExecute(object param) 
            => Regex.IsMatch(MasterPasswordPasswordBox.Password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*\\W).{8,}$") 
            && MasterPasswordPasswordBox.Password.Equals(MasterPasswordRepeatPasswordBox.Password);


        


    }
}
