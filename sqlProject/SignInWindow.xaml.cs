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
                if (user.Password != PasswordInput.Text)
                {
                    new NotificationWindow("Неверный пароль!").ShowDialog();
                    return;
                }
                switch (user.TypeID)
                {
                    case 1:
                        new TeacherWindow().Show();
                        break;
                    case 2:
                        new MainMenu(user).Show();
                        break;
                }
                Close();
            }
        }

        private void SignUp(object sender, MouseButtonEventArgs e)
        {
            new SignUpWindow().Show();
            Close();
        }
    }
}