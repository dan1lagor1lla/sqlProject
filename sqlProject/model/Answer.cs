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
                isCorrect = value;
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
        public Answer(int id, string content, bool isCorrect) : this(content, isCorrect) => ID = id;

        public override string ToString() => Content;
     
        public static Answer PositiveAnswer => new("Да", true);
        public static Answer NegativeAnswer => new("Нет", false);
    }
}
