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
        private DatabaseContext db = new();

        internal StudentWindow(User student)
        {
            InitializeComponent();
            Closed += LogOut;
            
            db.Users.Update(student);
            db.Logging.Add(new Logging(student, db.LoggingTypes.Single(logType => logType.ID == 1)));
            db.SaveChanges();

            db.Tests.Load();
            ListOfTests.ItemsSource = db.Tests.Local.ToObservableCollection();

            DataContext = student;
        }

        private void SearchFilterChanged(object sender, TextChangedEventArgs e)
        {
            TextBox searchBox = (TextBox)sender;
            ListOfTests.ItemsSource = db.Tests.Where(test => searchBox.Text == "" || test.Name.Contains(searchBox.Text)).ToList();
        }

        private void DoTest(object sender, SelectionChangedEventArgs e) => new TestWindow((User)DataContext, (Test)ListOfTests.SelectedItem).ShowDialog();

        private void LogOut(object? sender, EventArgs e)
        {
            db.Logging.Add(new Logging((User)DataContext, db.LoggingTypes.Single(logType => logType.ID == 2)));
            db.SaveChanges();
            db.Dispose();
        }

        private void CloseStudentWindow(object sender, RoutedEventArgs e)
        {
            new SignInWindow().Show();
            Close();
        }
    }
}
