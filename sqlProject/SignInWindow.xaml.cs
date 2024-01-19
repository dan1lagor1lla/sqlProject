using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using sqlProject.model;

namespace sqlProject
{
    // to do : open/close password
  
    public partial class SignInWindow : Window
    {
        private byte unsuccessfulAttemptsToLogIn = 0;
        
        public SignInWindow() => InitializeComponent();

        private void TryLogIn(object sender, RoutedEventArgs e)
        {
            if (LoginInput.Text == "" || LoginInput.Text.Trim() == "")
            {
                new NotificationWindow("Введите логин!").ShowDialog();
                return;
            }
            if (PasswordInput.Text == "" || PasswordInput.Text.Trim() == "")
            {
                new NotificationWindow("Введите пароль!").ShowDialog();
                return;
            }
            using (DatabaseContext db = new())
            {
                User? user = db.Users.FirstOrDefault(user => user.Login == LoginInput.Text);
                if (user is null)
                {
                    new NotificationWindow("Пользователя с данным логином не найдено!").ShowDialog();
                    RegisterUnsuccessfulAttemptToLogin();
                    return;
                }
                if (user.Password != PasswordInput.Text)
                {
                    new NotificationWindow("Неверный пароль!").ShowDialog();
                    RegisterUnsuccessfulAttemptToLogin();
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

        private void RegisterUnsuccessfulAttemptToLogin()
        {
            if (++unsuccessfulAttemptsToLogIn == 3)
            {
                new NotificationWindow("Попробуйте снова через 30 секунд.").ShowDialog();
                IsEnabled = false;
                Thread.Sleep(30000);
                IsEnabled = true;
                unsuccessfulAttemptsToLogIn = 0;
            }
        }

        private void ChangeTheme(object sender, RoutedEventArgs e) => ((App)App.Current).ChangeTheme();
    }
}