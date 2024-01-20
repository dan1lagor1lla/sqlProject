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
    // to do : new window password/email change
    //       : some statistic

    public partial class StudentWindow : Window
    {
        private User user;

        internal StudentWindow(User student)
        {
            InitializeComponent();

            using (DatabaseContext db = new())
            {
                db.Users.Update(student);
                db.Logging.Add(new Logging(student, db.LoggingTypes.Single(logType => logType.ID == 1)));
                db.SaveChanges();
                db.Tests.Load();
                ListOfTests.ItemsSource = db.Tests.Local.ToObservableCollection();
            }
            DataContext = user = student;
        }

        private void SearchFilterChanged(object sender, TextChangedEventArgs e)
        {
            using (DatabaseContext db = new())
            {
                db.Tests.Where(test => ((TextBox)sender).Text == "" || test.Name.Contains(((TextBox)sender).Text)).Load();
                ListOfTests.ItemsSource = db.Tests.Local.ToObservableCollection();
            }
        }

        private void DoTest(object sender, SelectionChangedEventArgs e)
        {
            new TestWindow(user, (Test)ListOfTests.SelectedItem).Show();
            Close();
        }
    }
}
