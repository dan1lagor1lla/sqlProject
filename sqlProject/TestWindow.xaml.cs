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
using Microsoft.EntityFrameworkCore.Storage.Json;
using sqlProject.model;

namespace sqlProject
{
    public partial class TestWindow : Window
    {
        public static DependencyProperty CurrentQuestionNumberProperty;

        private DatabaseContext db = new();
        private AchievementLogging log;
        private List<Question> questions = new List<Question>();

        public int CurrentQuestionNumber
        {
            get => (int)GetValue(CurrentQuestionNumberProperty);
            set => SetValue(CurrentQuestionNumberProperty, value);
        }

        static TestWindow() => CurrentQuestionNumberProperty = DependencyProperty.Register("CurrentQuestionNumber", typeof(int), typeof(TestWindow));
        
        internal TestWindow(User user, Test test)
        {
            InitializeComponent();
            
            db.UpdateRange(user, test);

            db.Questions.Where(q => q.OwnerTest == test && q.IsUsing).Include(q => q.Answers).Load();
            questions = db.Questions.Local.ToList();
            
            log = new AchievementLogging(user, test);
            db.AchievementLogging.Add(log);
            db.SaveChanges();

            CurrentQuestionNumber = 1;
            QuantityOfQuestions.Text = questions.Count.ToString();
            Title = test.Name;
            DataContext = questions[0];
        }

        private void Answer(object sender, RoutedEventArgs e)
        {
            for (int i = 0; i < ListOfAnswers.SelectedItems.Count; ++i)
                log.Answers.Add((Answer)ListOfAnswers.SelectedItems[i]!);

            if (CurrentQuestionNumber == questions.Count)
            {
                log.Finish();
                db.SaveChanges();
                db.Dispose();
                new NotificationWindow("Вы прошли тест.").ShowDialog();
                new StudentWindow(log.Student).Show();
                Close();
            }
            else
            {
                DataContext = questions[CurrentQuestionNumber++];
                db.SaveChanges();
            }
        }
    }
}
