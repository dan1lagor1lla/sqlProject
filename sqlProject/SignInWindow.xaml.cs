using System.Printing;
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

        public SignInWindow()
        {
            InitializeComponent();
        }

        private void TryLogIn(object sender, RoutedEventArgs e)
        {
            if (!CheckInputEmptiness(LoginInputBox, PasswordInputBox))
                return;

            User? user;
            using (DatabaseContext db = new())
                user = db.Users.FirstOrDefault(user => user.Login == LoginInputBox.Text);

            if (user is null)
            {
                new NotificationWindow("Пользователя с данным логином не найдено!").ShowDialog();
                return;
            }
            if (user.Password != PasswordInputBox.Text)
            {
                new NotificationWindow("Неверный пароль!").ShowDialog();
                RegisterUnsuccessfulAttemptToLogin();
                return;
            }

            switch (user.UserTypeID)
            {
                case 1:
                    new TeacherWindow().Show();
                    break;
                default:
                    new StudentWindow(user).Show();
                    break;
            }
            Close();
        }

        private void GoToSignUpWindow(object sender, MouseButtonEventArgs e)
        {
            new SignUpWindow().Show();
            Close();
        }
        
        private bool CheckInputEmptiness(params TextBox[] inputBoxes)
        {
            foreach (TextBox inputBox in inputBoxes)
                if (inputBox.Text == "" || inputBox.Text.All(chr => chr == ' '))
                {
                    new NotificationWindow($"Введите, {inputBox.Uid.ToLower()}").ShowDialog();
                    return false;
                }
            return true;
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
    }
}