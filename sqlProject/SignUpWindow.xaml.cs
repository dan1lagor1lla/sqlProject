using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
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
        private const string requirementsKey = "Requirements";
        private const int MinimalPasswordLength = 8;
        private const int MinimalLoginLength = 8;

        private char[] SpecialSymbols = ['@', '#', '%', '&', '*', '$'];


        public SignUpWindow()
        {
            InitializeComponent();

            Predicate<string> notEmptyRequirement = (str) => str.Length != 0;
            Predicate<string> notContainsSpacesRequirement = (str) => !str.Contains(' ');

            LoginInputBox.Resources[requirementsKey] = new Dictionary<Predicate<string>, string>()
            {
                [notEmptyRequirement] = "Введите логин.",
                [(str) => str.Length >= MinimalLoginLength] = $"Логин должен содержать не менее {MinimalLoginLength} символов.",
                [(str) => str.All(chr => char.IsLetterOrDigit(chr) || chr == '-')] = "Логин может состоять из букв, цифр и знака дефис.",
                [
                    (str) => {
                                using (DatabaseContext db = new())
                                    return db.Users.SingleOrDefault(u => u.Login == str) == null;
                             }
                ] = "Пользователь с этим логином уже существует."
            };
            EmailInputBox.Resources[requirementsKey] = new Dictionary<Predicate<string>, string>()
            {
                [notEmptyRequirement] = "Введите электронную почту.",
                [str => str.Contains('@')] = "Не забудьте '@'.",
                [str => str.EndsWith("mail.ru") || str.EndsWith("gmail.com")] = "Почта должна содержать доменное имя.",
                [notContainsSpacesRequirement] = "Адрес электронной почты не может содержать пробелов."
            };
            PasswordInputBox.Resources[requirementsKey] = new Dictionary<Predicate<string>, string>()
            {
                [notEmptyRequirement] = "Введите пароль.",
                [notContainsSpacesRequirement] = "Пароль не может содержать пробелов.",
                [str => str.Length >= MinimalPasswordLength] = $"Пароль должен содержать не менее {MinimalPasswordLength} символов.",
                [str => str.Intersect(SpecialSymbols).Count() != 0] = $"Пароль должен содержать хотя бы один специальный символ - {string.Concat(SpecialSymbols)}.",
                [str => str.Any(character => char.IsDigit(character))] = "Пароль должен содержать хотя бы одну цифру."
            };
            PasswordInputRepeatitionBox.Resources[requirementsKey] = new Dictionary<Predicate<string>, string>()
            {
                [notEmptyRequirement] = "Повторите пароль.",
                [str => str == PasswordInputBox.Text] = "Пароли не совпадают."
            };
        }

        private void TrySignUp(object sender, RoutedEventArgs e)
        {
            using (DatabaseContext db = new())
            {
                db.Users.Add(new User(LoginInputBox.Text, PasswordInputBox.Text, db.UserTypes.Single(type => type.ID == 2), EmailInputBox.Text));
                db.SaveChanges();
                new NotificationWindow("Вы успешно зарегистрировались!").ShowDialog();
                new SignInWindow().Show();
                Close();
            }
        }

        private void DataInputChanged(object sender, TextChangedEventArgs e)
        {
            TextBox sndr = (TextBox)sender;
            TextBox[] inputBoxes = [LoginInputBox, EmailInputBox, PasswordInputBox, PasswordInputRepeatitionBox];
            bool isAllDataEnteredCorrectly = true;

            foreach (TextBox inputBox in inputBoxes)
                if (!СheckСompliance(inputBox, sndr == inputBox || (sndr == PasswordInputBox && inputBox == PasswordInputRepeatitionBox)))
                    isAllDataEnteredCorrectly = false;

            TrySignUpButton.IsEnabled = isAllDataEnteredCorrectly;
        }

        private void BackToSignInWindow(object sender, MouseButtonEventArgs e)
        {
            new SignInWindow().Show();
            Close();
        }

        private bool СheckСompliance(TextBox inputBox, bool toHighlight = false)
        {
            var warningsToRequirements = (Dictionary<Predicate<string>, string>)inputBox.Resources[requirementsKey];
            
            foreach (Predicate<string> requirement in warningsToRequirements.Keys)
            {
                if (requirement(inputBox.Text))
                    continue;
                if (toHighlight)
                {
                    inputBox.DataContext = warningsToRequirements[requirement];
                    inputBox.BorderBrush = (Brush)App.Current.Resources["AlarmBrush"];
                }
                return false;
            }
            inputBox.DataContext = "";
            inputBox.ClearValue(TextBox.BorderBrushProperty);
            return true;
        }
    }
}