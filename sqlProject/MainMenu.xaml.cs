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
    // to do : search
    //       : testing
    //       : password/email change
    //       : some statistic 
    public partial class MainMenu : Window
    {
        internal MainMenu(User student)
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
            DataContext = student;
        }
    }
}
