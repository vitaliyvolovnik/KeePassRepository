using BLL.Models.Dtos;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace KeePass.Domain
{
    class PasswordBehavior:Behavior<PasswordBox>
    {

        public static DependencyProperty PasswordProp =
            DependencyProperty.Register("Password", typeof(SecurePassword), typeof(PasswordBehavior));
        
        private bool _skipUpdate;

        public SecurePassword Password
        {
            get { return (SecurePassword)GetValue(PasswordProp); }
            set { SetValue(PasswordProp, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();

            AssociatedObject.PasswordChanged += OnPasswordChanged;
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PasswordChanged -= OnPasswordChanged;
        }


        protected override void OnPropertyChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnPropertyChanged(e);

            if (e.Property == PasswordProp)
            {
                if (!_skipUpdate)
                {
                    _skipUpdate = true;
                    AssociatedObject.Password = (e.NewValue as SecurePassword)?.Password;
                    _skipUpdate = false;
                }
            }
        }

        private void OnPasswordChanged(object sender, RoutedEventArgs e)
        {
            _skipUpdate = true;
            Password.Password = AssociatedObject.Password as string;
            _skipUpdate = false;
        }


    }
}
