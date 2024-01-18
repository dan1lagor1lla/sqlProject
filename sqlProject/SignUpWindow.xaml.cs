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
        private const int MinimalLoginLength = 8;

        private char[] Digits = ['1', '2', '3', '4', '5', '6', '7', '8', '9', '0'];
        private char[] SpecialSymbols = ['@', '#', '%', '&', '*', '$'];
        
        public SignUpWindow()
        {
            InitializeComponent();
        }

        private void TrySignUp(object sender, RoutedEventArgs e)
        {
            using (DatabaseContext db = new())
            {
                if (db.Users.SingleOrDefault(user => user.Login == LoginInput.Text) is not null)
                {
                    new NotificationWindow("Пользователь с этим именем уже существует!").ShowDialog();
                    return;
                }
                User newUser = new User(LoginInput.Text, EmailInput.Text, PasswordInput.Text, db.UserTypes.Single(type => type.ID == 2));
                db.Users.Add(newUser);
                db.SaveChanges();
                new NotificationWindow("Вы успешно зарегистрировались!").ShowDialog();
                new SignInWindow().Show();
                Close();
            }
        }

        private void BackToSignInWindow(object sender, MouseButtonEventArgs e)
        {
            new SignInWindow().Show();
            Close();
        }

        private bool СheckСomplianceWithRequirements(TextBox textbox, TextBlock adviceTextBlock, Dictionary<Predicate<string>, string> requirements, bool effectAdviceTextBlock = false)
        {
            foreach (var requirement in requirements.Keys)
            {
                if (requirement(textbox.Text))
                {
                    if (effectAdviceTextBlock)
                    {
                        adviceTextBlock.Visibility = Visibility.Visible;
                        adviceTextBlock.Text = requirements[requirement];
                        textbox.BorderBrush = (Brush)App.Current.Resources["AlarmBrush"];
                    }
                    return false;
                }
            }
            if (effectAdviceTextBlock)
            {
                adviceTextBlock.Visibility = Visibility.Collapsed;
                textbox.ClearValue(TextBox.BorderBrushProperty);
            }
            return true;
        }

        private void LoginInputChanged(object sender, TextChangedEventArgs e)
        {
            TrySignUpButton.IsEnabled = СheckСomplianceWithRequirements(LoginInput, LoginInputAdvice, new Dictionary<Predicate<string>, string>()
            {
                [str => str.Length == 0] = "Заполните поле",
                [str => str.Length < MinimalPasswordLength] = $"Логин должен быть не меньше {MinimalLoginLength} символов"
            }, true) & СheckСomplianceWithRequirements(EmailInput, EmailInputAdvice, new Dictionary<Predicate<string>, string>()
            {
                [str => str.Length == 0] = "Заполните поле",
                [str => !str.Contains('@')] = "Не забудьте '@'",
                [str => !str.Split('@')[1].Contains(".com") && !str.Split('@')[1].Contains(".ru")] = "Почта должна содержать доменное имя",
            }) & СheckСomplianceWithRequirements(PasswordInput, PasswordInputAdvice, new Dictionary<Predicate<string>, string>()
            {
                [str => str.Length == 0] = "Заполните поле",
                [str => str.Length < MinimalPasswordLength] = $"Пароль должен быть не меньше {MinimalPasswordLength} символов",
                [str => !ContainsAtLeastOneOf(str, SpecialSymbols)] = $"Пароль должен содержать хотя бы один специальный символ - {string.Concat(SpecialSymbols)}",
                [str => !ContainsAtLeastOneOf(str, Digits)] = "Пароль должен содержать хотя бы одну цифру"
            }) & СheckСomplianceWithRequirements(PasswordInputCheck, PasswordInputCheckAdvice, new Dictionary<Predicate<string>, string>()
            {
                [str => str.Length == 0] = "Заполните поле",
                [str => str != PasswordInput.Text] = "Пароли не совпадают"
            });
        }

        private void EmailInputChanged(object sender, TextChangedEventArgs e)
        {
            TrySignUpButton.IsEnabled = СheckСomplianceWithRequirements(LoginInput, LoginInputAdvice, new Dictionary<Predicate<string>, string>()
            {
                [str => str.Length == 0] = "Заполните поле",
                [str => str.Length < MinimalPasswordLength] = $"Логин должен быть не меньше {MinimalLoginLength} символов"
            }) & СheckСomplianceWithRequirements(EmailInput, EmailInputAdvice, new Dictionary<Predicate<string>, string>()
            {
                [str => str.Length == 0] = "Заполните поле",
                [str => !str.Contains('@')] = "Не забудьте '@'",
                [str => !str.Split('@')[1].Contains(".com") && !str.Split('@')[1].Contains(".ru")] = "Почта должна содержать доменное имя",
            }, true) & СheckСomplianceWithRequirements(PasswordInput, PasswordInputAdvice, new Dictionary<Predicate<string>, string>()
            {
                [str => str.Length == 0] = "Заполните поле",
                [str => str.Length < MinimalPasswordLength] = $"Пароль должен быть не меньше {MinimalPasswordLength} символов",
                [str => !ContainsAtLeastOneOf(str, SpecialSymbols)] = $"Пароль должен содержать хотя бы один специальный символ - {string.Concat(SpecialSymbols)}",
                [str => !ContainsAtLeastOneOf(str, Digits)] = "Пароль должен содержать хотя бы одну цифру"
            }) & СheckСomplianceWithRequirements(PasswordInputCheck, PasswordInputCheckAdvice, new Dictionary<Predicate<string>, string>()
            {
                [str => str.Length == 0] = "Заполните поле",
                [str => str != PasswordInput.Text] = "Пароли не совпадают"
            });
        }

        private void PasswordInputChanged(object sender, TextChangedEventArgs e)
        {
            TrySignUpButton.IsEnabled = СheckСomplianceWithRequirements(LoginInput, LoginInputAdvice, new Dictionary<Predicate<string>, string>()
            {
                [str => str.Length == 0] = "Заполните поле",
                [str => str.Length < MinimalPasswordLength] = $"Логин должен быть не меньше {MinimalLoginLength} символов"
            }) & СheckСomplianceWithRequirements(EmailInput, EmailInputAdvice, new Dictionary<Predicate<string>, string>()
            {
                [str => str.Length == 0] = "Заполните поле",
                [str => !str.Contains('@')] = "Не забудьте '@'",
                [str => !str.Split('@')[1].Contains(".com") && !str.Split('@')[1].Contains(".ru")] = "Почта должна содержать доменное имя",
            }) & СheckСomplianceWithRequirements(PasswordInput, PasswordInputAdvice, new Dictionary<Predicate<string>, string>()
            {
                [str => str.Length == 0] = "Заполните поле",
                [str => str.Length < MinimalPasswordLength] = $"Пароль должен быть не меньше {MinimalPasswordLength} символов",
                [str => !ContainsAtLeastOneOf(str, SpecialSymbols)] = $"Пароль должен содержать хотя бы один специальный символ - {string.Concat(SpecialSymbols)}",
                [str => !ContainsAtLeastOneOf(str, Digits)] = "Пароль должен содержать хотя бы одну цифру"
            }, true) & СheckСomplianceWithRequirements(PasswordInputCheck, PasswordInputCheckAdvice, new Dictionary<Predicate<string>, string>()
            {
                [str => str.Length == 0] = "Заполните поле",
                [str => str != PasswordInput.Text] = "Пароли не совпадают"
            });
        }

        private void PasswordInputCheckChanged(object sender, TextChangedEventArgs e)
        {
            TrySignUpButton.IsEnabled = СheckСomplianceWithRequirements(LoginInput, LoginInputAdvice, new Dictionary<Predicate<string>, string>()
            {
                [str => str.Length == 0] = "Заполните поле",
                [str => str.Length < MinimalPasswordLength] = $"Логин должен быть не меньше {MinimalLoginLength} символов"
            }) & СheckСomplianceWithRequirements(EmailInput, EmailInputAdvice, new Dictionary<Predicate<string>, string>()
            {
                [str => str.Length == 0] = "Заполните поле",
                [str => !str.Contains('@')] = "Не забудьте '@'",
                [str => !str.Split('@')[1].Contains(".com") && !str.Split('@')[1].Contains(".ru")] = "Почта должна содержать доменное имя",
            }) & СheckСomplianceWithRequirements(PasswordInput, PasswordInputAdvice, new Dictionary<Predicate<string>, string>()
            {
                [str => str.Length == 0] = "Заполните поле",
                [str => str.Length < MinimalPasswordLength] = $"Пароль должен быть не меньше {MinimalPasswordLength} символов",
                [str => !ContainsAtLeastOneOf(str, SpecialSymbols)] = $"Пароль должен содержать хотя бы один специальный символ - {string.Concat(SpecialSymbols)}",
                [str => !ContainsAtLeastOneOf(str, Digits)] = "Пароль должен содержать хотя бы одну цифру"
            }) & СheckСomplianceWithRequirements(PasswordInputCheck, PasswordInputCheckAdvice, new Dictionary<Predicate<string>, string>()
            {
                [str => str.Length == 0] = "Заполните поле",
                [str => str != PasswordInput.Text] = "Пароли не совпадают"
            }, true);
        }

        private bool ContainsAtLeastOneOf(string str, params char[] symbols)
        {
            foreach (char symbol in symbols)
                if (str.Contains(symbol))
                    return true;
            return false;
        }
    }
}
