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
        private DatabaseContext db = new();
        private AchievementLogging log;
        private List<Question> questions = new List<Question>();
        public static DependencyProperty CurrentQuestionNumberProperty;

        public int CurrentQuestionNumber
        {
            get => (int)GetValue(CurrentQuestionNumberProperty);
            set => SetValue(CurrentQuestionNumberProperty, value);
        }

        static TestWindow()
        {
            CurrentQuestionNumberProperty = DependencyProperty.Register("CurrentQuestionNumber", typeof(int), typeof(TestWindow));
        }

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

        private void ToAnswer(object sender, RoutedEventArgs e)
        {
            List<Answer> answers = new List<Answer>();
            foreach (Answer answer in ListOfAnswers.SelectedItems)
                answers.Add(answer);
            log.Answers.AddRange(answers);
            if (CurrentQuestionNumber != questions.Count)
                DataContext = questions[CurrentQuestionNumber++];
            db.SaveChanges();   
        }
    }
}
