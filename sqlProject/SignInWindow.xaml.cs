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
        private DataContext databaseContext = new();
        public SignInWindow()
        {
            InitializeComponent();
        }

        private void TryLogIn(object sender, RoutedEventArgs e)
        {
            if (databaseContext.Users.Count(user => user.Name == NameInput.Name) == 0)
            {
                MessageBox.Show("Пользователя с данным именем не найдено!");
                return;
            }
            User user = databaseContext.Users.First(user => user.Name == NameInput.Name);
            if (user.Password != PasswordInput.Text)
            {
                MessageBox.Show("Неверный пароль!");
                return;
            }
            MainMenu mainMenu = new(user);
            mainMenu.Show();
            Close();
        }

        private void DragWindow(object sender, MouseButtonEventArgs e) => DragMove();
    }
}