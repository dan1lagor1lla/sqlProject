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
using Microsoft.EntityFrameworkCore;
using sqlProject.model;

namespace sqlProject
{
    // to do : личный кабинет with new window password/email change
    //       : some statistic

    public partial class StudentWindow : Window
    {
        private User student;

        internal StudentWindow(User student)
        {
            InitializeComponent();

            GreetingTextBlock.DataContext = this.student = student;
            Closing += ToLog;

            using (DatabaseContext db = new())
                ListOfTests.ItemsSource = db.Tests.ToList();
            CheckFilter();
        }

        private void SearchFilterChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchBox = (TextBox)sender;
            
            using (DatabaseContext db = new())
                ListOfTests.ItemsSource = db.Tests.Where(test => searchBox.Text == "" || EF.Functions.Like(test.Name, $"%{searchBox.Text}%")).ToList();
            CheckFilter();
        }

        private void GetTested(object sender, SelectionChangedEventArgs e) => new TestWindow(student, (Test)ListOfTests.SelectedItem).ShowDialog();

        private void ToLog(object? sender, EventArgs e)
        {
            using (DatabaseContext db = new())
            {
                db.Users.Update(student);
                db.Logging.Add(new Logging(student, db.LoggingTypes.Find(2)!));
                db.SaveChanges();
            }
        }

        private void LogOut(object sender, RoutedEventArgs e)
        {
            new SignInWindow().Show();
            Close();
        }

        private void CheckFilter()
        {
            if (ListOfTests.Items.Count == 0)
                FilterFailedTextBlock.Opacity = 1;
            else
                FilterFailedTextBlock.Opacity = 0;
        }
    }
}
