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
using System.Windows.Shapes;
using sqlProject.model;

namespace sqlProject
{
    public partial class SignUpWindow : Window
    {
        private const int MinimalPasswordLength = 8;

        public SignUpWindow()
        {
            InitializeComponent();
        }

        private void TrySignUp(object sender, RoutedEventArgs e)
        {
            using (DataContext db = new())
            {
                if (db.Users.SingleOrDefault(user => user.Name == NameInput.Text) is not null)
                {
                    MessageBox.Show("Пользователь с этим именем уже существует!"); // to do : replace
                    return;
                }
                User newUser = new User(NameInput.Text, PasswordInput.Text, db.UserTypes.Single(type => type.ID == 2));
                db.Users.Add(newUser);
                db.SaveChanges();
                MessageBox.Show("Вы успешно зарегистрировались!"); // to do : replace
                new SignInWindow().Show();
                Close();
            }
        }

        private void BackToSignInWindow(object sender, MouseButtonEventArgs e)
        {
            new SignInWindow().Show();
            Close();
        }

        private void InputChanged(object sender, TextChangedEventArgs e)
        {
            if (NameInput.Text.Length == 0)
            {
                NameInputAdvice.Text = $"Заполните поле";
                NameInputAdvice.Visibility = Visibility.Visible;
            }
            else if (NameInput.Text.Length < MinimalPasswordLength)
            {
                NameInputAdvice.Text = $"Пароль должен быть не меньше {MinimalPasswordLength} символов";
                NameInputAdvice.Visibility = Visibility.Visible;
            }
            else
            {
                NameInputAdvice.Visibility = Visibility.Collapsed;
            }
        }
    }
}
