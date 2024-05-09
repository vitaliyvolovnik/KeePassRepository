using BLL.Models.Dtos;
using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;

namespace KeePass.Domain
{
    public class PasswordBehavior : Behavior<PasswordBox>
    {

        public static DependencyProperty SecurePasswordProperty =
            DependencyProperty.Register("SecurePassword", typeof(SecurePassword), typeof(PasswordBehavior));

        private bool _skipUpdate;


        public SecurePassword SecurePassword
        {
            get { return (SecurePassword)GetValue(SecurePasswordProperty); }
            set { SetValue(SecurePasswordProperty, value); }
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

            if (e.Property == SecurePasswordProperty)
            {
                if (!_skipUpdate && AssociatedObject != null)
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
            SecurePassword.Password = AssociatedObject.Password as string;
            _skipUpdate = false;
        }


    }
}
