﻿using KeePass.Core;
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
    /// Interaction logic for CreateFolderWindow.xaml
    /// </summary>
    public partial class CreateFolderWindow : Window
    {

        public string? Name { get; private set; }
        public bool IsOk { get; private set; } = false;

        public CreateFolderWindow()
        {
            InitializeComponent();

            CreateBtn.Command = new RelayCommand(CreateFolderExecute, CreateFoderCanExecute);

        }

        private bool CreateFoderCanExecute(object arg)
        {
            return Regex.IsMatch(FolderNameTextBox.Text, @"^[a-zA-Z0-9 .\-_]{2,50}$");
        }

        private void CreateFolderExecute(object arg)
        {
            this.Name = FolderNameTextBox.Text;

            this.IsOk = true;
            this.Close();
        } 
    }
}
