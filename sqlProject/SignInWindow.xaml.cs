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
            using (DataContext db = new())
            {
                User? user = db.Users.FirstOrDefault(user => user.Login == LoginInput.Text);
                if (user is null)
                {
                    new NotificationWindow("Пользователя с данным логином не найдено!").ShowDialog();
                    return;
                }
                if (user.Password != ((PasswordConverter)Resources["PasswordConverter"]).ConvertBack(PasswordInput.Text, null, PasswordInput, null))
                {
                    new NotificationWindow("Неверный пароль!").ShowDialog();
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
            TryLogInButton.IsEnabled = LoginInput.Text.Length < 6 || PasswordInput.Text.Length < 6 ? false : true;
        }
    }
}