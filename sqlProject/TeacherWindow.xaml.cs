using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using Microsoft.Win32;
using sqlProject.model;

// to do : если остался 1 вопрос при удалении вопросов он должен использоваться (IsUsing = true)
//         same thing with answers
//         all answers cannot be correct

namespace sqlProject
{
    /// <summary>
    /// Interaction logic for TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        private ListBox ListOfAnswers;
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
            if (TestList.SelectedItems.Count != 1)
            {
                TestChanger.DataContext = null;
                return;
            }
            Test selectedTest = (Test)TestList.SelectedItem;

            using (DataContext db = new())
            {
                db.Tests.Update(selectedTest);
                db.Tests.Entry(selectedTest).Collection(test => test.Questions).Load();
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
            if (((Question)QuestionList.SelectedItem).OwnerTest.Questions.Count - QuestionList.SelectedItems.Count <= 0)
            {
                MessageBox.Show("Тест должен иметь хотя бы 1 вопрос!"); // to do : replace
                return;
            }
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
        private void AnswerListContextMenuOpened(object sender, RoutedEventArgs e) => MenuItemDisable((ContextMenu)sender, ListOfAnswers, 1);

        private void MenuItemDisable(ContextMenu contextMenu, ListBox contextMenuOwner, int itemIndex)
        {
            if (contextMenuOwner.SelectedItem is null)
                ((MenuItem)contextMenu.Items[itemIndex]).IsEnabled = false;
            else
                ((MenuItem)contextMenu.Items[itemIndex]).IsEnabled = true;
        }

        private void AnswerListExpanded(object sender, RoutedEventArgs e)
        {
            Question expandedQuestion = (Question)((Expander)sender).DataContext;
            using (DataContext db = new())
            {
                db.Questions.Update(expandedQuestion);
                db.Questions.Entry(expandedQuestion).Collection(question => question.Answers).Load();
            }
        }

        private void TextBoxKeyDown(object sender, KeyEventArgs e)
        {
            TextBox sndr = (TextBox)sender;
            if (e.Key != Key.Enter)
                return;
            sndr.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            Keyboard.ClearFocus();
        }

        private void AddAnswer(object sender, RoutedEventArgs e)
        {
            Question question = (Question)((MenuItem)sender).DataContext;
            using (DataContext db = new())
            {
                db.Questions.Update(question);
                question.Answers.Add(Answer.CorrectAnswer);
                db.SaveChanges();
            }
        }

        private void RemoveAnswer(object sender, RoutedEventArgs e)
        {

        }

        private void ListOfAnswersLoaded(object sender, RoutedEventArgs e) => ListOfAnswers = (ListBox)sender;
    }
}
