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

namespace KeePass.Domain
{
    /// <summary>
    /// Interaction logic for PasswordParameterDialog.xaml
    /// </summary>
    public partial class PasswordParameterDialog : UserControl
    {
        public PasswordParameterDialog()
        {
            InitializeComponent();
        }


        public int Length
        {
            get { return (int)LengthSlider.Value; }
        }
        public bool UseUppercase
        {
            get { return UseUperCheck.IsChecked ?? false; }
        }
        public bool UseNumbers
        {
            get { return UseNumbersCheck.IsChecked ?? false; }
        }
        public bool UseSpecialSymbols
        {
            get { return UseSpecialCheck.IsChecked ?? false; }
        }
    }
}
