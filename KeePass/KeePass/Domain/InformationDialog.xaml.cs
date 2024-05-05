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
    /// Interaction logic for InformationDialog.xaml
    /// </summary>
    public partial class InformationDialog : UserControl
    {
        public InformationDialog()
        {
            InitializeComponent();
        }

        private string message;
        public string MessageTxt
        {
            get { return message; }
            set { message = value; Message.Text = message; }

        }
    }
}
