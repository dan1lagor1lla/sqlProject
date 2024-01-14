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
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using sqlProject.model;

// to do : tool tip on unenabled button
//         some requierments for input & advices what matter with incorrect inputs
//         all answers cannot be correct? or can be

namespace sqlProject
{
    /// <summary>
    /// Interaction logic for TeacherWindow.xaml
    /// </summary>
    public partial class TeacherWindow : Window
    {
        private DataContext databaseContext = new();
        
        public TeacherWindow()
        {
            InitializeComponent();

            databaseContext.Tests.Load();
            ListOfTests.ItemsSource = databaseContext.Tests.Local.ToObservableCollection();
        }

        private void AddTest(object sender, RoutedEventArgs e)
        {
            databaseContext.Tests.Add(new Test("Новый тест") 
            { 
                Questions = [new Question("Вопрос") 
                { 
                    Answers = [new Answer("Верный ответ", true), new Answer("Неверный ответ", false)] 
                }] 
            });
            databaseContext.SaveChanges();
        }

        private void RemoveSelectedTests(object sender, RoutedEventArgs e) // to do : add approve popup
        {
            while (ListOfTests.SelectedItem is not null)
                databaseContext.Tests.Remove((Test)ListOfTests.SelectedItem);
            databaseContext.SaveChanges();
        }
        
        private void TestSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ListOfTests.SelectedItems.Count != 1)
            {
                ListOfQuestions.ContextMenu = null;
                TestSettingsViewer.DataContext = null;
                QuestionRandomnessToggleButton.IsEnabled = false;
                return;
            }
            Test selectedTest = (Test)ListOfTests.SelectedItem;
            databaseContext.Tests.Entry(selectedTest).Collection(test => test.Questions).Load();
            
            ListOfQuestions.ContextMenu = (ContextMenu)Resources["ListOfQuestionsContextMenu"];
            TestSettingsViewer.DataContext = selectedTest;
            QuestionRandomnessToggleButton.IsEnabled = true;
        }

        private void ListOfTestsContextMenuOpened(object sender, RoutedEventArgs e)
        {
            ContextMenu menuSender = (ContextMenu)sender;
            ((MenuItem)menuSender.Items[1]).IsEnabled = ListOfTests.SelectedItem is not null;
        }

        private void AddQuestion(object sender, RoutedEventArgs e)
        {
            ((Test)TestSettingsViewer.DataContext).Questions.Add(new Question("Вопрос") 
            { 
                Answers = [new Answer("Верный ответ", true), new Answer("Неверный ответ", false)] 
            });
            databaseContext.SaveChanges();
        }

        private void RemoveSelectedQuestions(object sender, RoutedEventArgs e) // to do : add approve popup
        {
            Test test = (Test)TestSettingsViewer.DataContext;
            if (test.Questions.Count - ListOfQuestions.SelectedItems.Count == 0)
            {
                MessageBox.Show("Тест должен иметь хотя бы один вопрос!"); // to do : replace
                return;
            }
            while (ListOfQuestions.SelectedItem is not null)
            {
                databaseContext.Questions.Remove((Question)ListOfQuestions.SelectedItem);
                databaseContext.SaveChanges();
            }
            if (test.Questions.Count(question => question.IsUsing) == 0)
            {
                test.Questions[0].IsUsing = true;
                databaseContext.SaveChanges();
            }
        }

        private void ListOfQuestionsContextMenuOpened(object sender, RoutedEventArgs e)
        {
            ContextMenu menuSender = (ContextMenu)sender;
            ((MenuItem)menuSender.Items[1]).IsEnabled = ListOfQuestions.SelectedItem is not null;
        }

        private void ListOfAnswersExpanded(object sender, RoutedEventArgs e)
        {
            Question expandedQuestion = (Question)((Expander)sender).DataContext;
            databaseContext.Questions.Entry(expandedQuestion).Collection(question => question.Answers).Load();
        }

        private void UpdateSourceOfTextProperty(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
                return;
            ((TextBox)sender).GetBindingExpression(TextBox.TextProperty).UpdateSource();
            Keyboard.ClearFocus();
        }

        private void AddAnswer(object sender, RoutedEventArgs e)
        {
            ((Question)((FrameworkElement)sender).DataContext).Answers.Add(new Answer("Неверный ответ", false));
            databaseContext.SaveChanges();
        }

        private void RemoveAnswer(object sender, RoutedEventArgs e)
        {
            Answer removingAnswer = (Answer)((FrameworkElement)sender).DataContext;
            if (removingAnswer.OwnerQuestion.Answers.Count == 2)
            {
                MessageBox.Show("Вопрос должен иметь хотя бы два ответа!"); // to do : replace 
                return;
            }
            Question question = removingAnswer.OwnerQuestion;
            databaseContext.Answers.Remove(removingAnswer);
            databaseContext.SaveChanges();
            
            if (question.Answers.Count(answer => answer.IsCorrect) == 0)
            {
                question.Answers[0].IsCorrect = true;
                databaseContext.SaveChanges();
            }
        }

        private void SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((ListBox)sender).SelectedItem = null;
        }
    }
}
