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
        private List<Question> questions = new List<Question>();
        private int CurrentQuestionNumber = 1;

        internal TestWindow(User user, Test test)
        {
            InitializeComponent();
            Title = test.Name;
            using (DatabaseContext db = new())
            {
                db.UpdateRange(user, test);
                db.Tests.Where(t => t.ID == test.ID).Include(t => t.Questions).Load();
                questions = db.Questions.Local.ToList();
                db.AchievementLogging.Add(new AchievementLogging(user, test));
                db.SaveChanges();
            }
            DataContext = questions[0];
        }
    }
}
