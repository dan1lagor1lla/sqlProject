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
    /// <summary>
    /// Interaction logic for MainMenu.xaml
    /// </summary>
    public partial class MainMenu : Window
    {
        private DataContext databaseContext = new();
        private User student;

        internal MainMenu(User student)
        {
            InitializeComponent();

            this.student = student;
            databaseContext.Tests.Load();
            ListOfTests.ItemsSource = databaseContext.Tests.Local.ToObservableCollection();
        }

        private void ChangeTheme(object sender, RoutedEventArgs e)
        {
        }

    }
}
