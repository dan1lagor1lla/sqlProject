using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using sqlProject.model;

namespace sqlProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SignInWindow : Window
    {
        public SignInWindow() => InitializeComponent();

        private void TryLogIn(object sender, RoutedEventArgs e)
        {
            //((App)Application.Current).ChangeTheme();
            using (DataContext db = new())
            {
                User? user = db.Users.FirstOrDefault(user => user.Name == NameInput.Text);
                if (user is null)
                {
                    MessageBox.Show("Пользователя с данным именем не найдено!"); // to do : replace 
                    return;
                }
                if (user.Password != PasswordInput.Text)
                {
                    MessageBox.Show("Неверный пароль!"); // to do : replace 
                    return;
                }
                new MainMenu(user).Show();
                Close();
            }
        }

        private void SignUp(object sender, MouseButtonEventArgs e)
        {
            new SignUpWindow().Show();
            Close();
        }

        private void InputChanged(object sender, TextChangedEventArgs e)
        {
            TryLogInButton.IsEnabled = NameInput.Text.Length < 6 || PasswordInput.Text.Length < 6 ? false : true;
        }
    }
}