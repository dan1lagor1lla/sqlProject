using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
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
using Microsoft.EntityFrameworkCore.Infrastructure;
using sqlProject.model;

namespace sqlProject
{
    /// <summary>
    /// Interaction logic for TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        private ObservableCollection<Test> Tests = new();

        public TeacherWindow()
        {
            InitializeComponent();

            using (DataContext db = new())
                foreach (Test test in db.Tests)
                    Tests.Add(test);

            TestList.ItemsSource = Tests;
        }


        private void AddTest(object sender, RoutedEventArgs e)
        {
            using (DataContext db = new())
            {
                db.Tests.Add(Test.DefaultTest);
                db.SaveChanges();
                Tests.Add(db.Tests.OrderBy(test => test.ID).Last());
            }
        }

        private void RemoveTest(object sender, RoutedEventArgs e) // to do : add approve popup
        {
            using (DataContext db = new())
            {
                while (TestList.SelectedItem is not null)
                {
                    Test selectedTest = (Test)TestList.SelectedItem;
                    db.Tests.Remove(selectedTest);
                    Tests.Remove(selectedTest);
                }
                db.SaveChanges();
            }
        }

        private void TestSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Test? selectedTest = TestList.SelectedItem as Test;
            
            if (selectedTest is null)
                return;

            using (DataContext db = new())
            {
                db.Tests.Update(selectedTest);
                db.Tests.Entry(selectedTest).Collection(test => test.Questions).Load();
                for (int i = 0; i < db.Questions.Local.Count; ++i)
                {
                    Question question = db.Questions.Local.ToList()[i]; 
                    db.Questions.Entry(question).Collection(answer => answer.Answers).Load();
                }
            }
            TestChanger.DataContext = selectedTest;
        }

        private void AddQuestion(object sender, RoutedEventArgs e)
        {
            Test? selectedTest = TestList.SelectedItem as Test;
            if (selectedTest is null)
                return;
            using (DataContext db = new())
            {
                db.Tests.Update(selectedTest);
                selectedTest.Questions.Add(Question.DefaultQuestion);
                db.SaveChanges();
            }
        }

        private void RemoveQuestion(object sender, RoutedEventArgs e) // to do : add approve popup
        {
            using (DataContext db = new())
            {
                while (QuestionList.SelectedItem is not null)
                {
                    db.Questions.Remove((Question)QuestionList.SelectedItem);
                    db.SaveChanges();
                }
            }
        }
        private void TestListContextMenuOpened(object sender, RoutedEventArgs e) => MenuItemDisable((ContextMenu)sender, TestList, 1);
        private void QuestionListContextMenuOpened(object sender, RoutedEventArgs e) => MenuItemDisable((ContextMenu)sender, QuestionList, 1);

        private void MenuItemDisable(ContextMenu contextMenu, ListBox contextMenuOwner, int itemIndex)
        {
            if (contextMenuOwner.SelectedItem is null)
                ((MenuItem)contextMenu.Items[itemIndex]).IsEnabled = false;
            else
                ((MenuItem)contextMenu.Items[itemIndex]).IsEnabled = true;
        }
    }
}
