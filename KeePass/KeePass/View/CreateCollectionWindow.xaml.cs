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
using System.Windows.Shapes;

namespace KeePass.View
{
    /// <summary>
    /// Interaction logic for CreateCollectionWindow.xaml
    /// </summary>
    public partial class CreateCollectionWindow : Window
    {
        public string? Name { get; private set; }
        public bool IsOk { get; private set; } = false;

        public CreateCollectionWindow()
        {
            InitializeComponent();

            CreateBtn.Command = new RelayCommand(CreateCollectionExecute, CreateCollectionCanExecute);

        }

        private bool CreateCollectionCanExecute(object arg)
        {
            return Regex.IsMatch(CollectionNameTextBox.Text, @"^[a-zA-Z0-9 .\-_]{2,50}$");
        }

        private void CreateCollectionExecute(object arg)
        {
            this.Name = CollectionNameTextBox.Text;

            this.IsOk = true;
            this.Close();
        }
    }
}
