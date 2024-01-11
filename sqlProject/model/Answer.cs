using Microsoft.EntityFrameworkCore;
using sqlProject.model.configurations;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace sqlProject.model
{
    [EntityTypeConfiguration(typeof(AnswerConfiguration))]
    internal class Answer : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string content;
        private bool isCorrect;

        public int ID { get; set; }
        public string Content
        {
            get => content;
            set
            {
                if (value == "")
                {
                    MessageBox.Show("Ошибка"); // to do : replace 
                    return;
                }
                content = value;
                using (DataContext db = new())
                {
                    db.Answers.Update(this);
                    db.SaveChanges();
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Content)));
            }
        }
        public bool IsCorrect
        {
            get => isCorrect;
            set
            {
                if (value == false && OwnerQuestion.Answers.Count(answer => answer.IsCorrect) == 1)
                {
                    MessageBox.Show("Вопрос должен иметь хотя бы один правильный ответ!"); // to do : replace
                    return;
                }
                isCorrect = value;
                using (DataContext db = new())
                {
                    db.Answers.Update(this);
                    db.SaveChanges();
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsCorrect)));
            }
        }
        public int OwnerQuestionID { get; set; }
        public Question OwnerQuestion { get; set; }
        private ObservableCollection<AchievementLogging> Loggings { get; set; }

        public Answer(string content, bool isCorrect)
        {
            this.content = content;
            this.isCorrect = isCorrect;
        }

        public override string ToString() => Content;
    }
}
