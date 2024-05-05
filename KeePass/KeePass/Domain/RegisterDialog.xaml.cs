using Humanizer;
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

namespace KeePass.Domain
{
    /// <summary>
    /// Interaction logic for RegisterDialog.xaml
    /// </summary>
    public partial class RegisterDialog : UserControl
    {
        public RegisterDialog()
        {
            InitializeComponent();
        }

        public string Password
        {
            get { return RegisterPasswordBox.Password; }

        }
        private string RepeatPassword
        {
            get { return RepeatPasswordBox.Password; }
        }



        public void OnPasswordChange(object sender, RoutedEventArgs e)
        {
            if(Password.Equals(RepeatPassword) && 
                Regex.IsMatch(Password, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*\\W).{8,}$"))
                AcceptBtn.IsEnabled = true;
            else AcceptBtn.IsEnabled = false;
        }

        
    }
}
