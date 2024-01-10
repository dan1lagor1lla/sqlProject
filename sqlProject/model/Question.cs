using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using sqlProject.model.configurations;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace sqlProject.model
{
    [EntityTypeConfiguration(typeof(QuestionConfiguration))]
    internal class Question : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string content;
        private bool isUsing;

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
                    db.Questions.Update(this);
                    db.SaveChanges();
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Content)));
            }
        }
        public bool IsUsing
        {
            get => isUsing;
            set
            {
                if (value == false && OwnerTest.Questions.Count(question => question.IsUsing) == 1)
                {
                    MessageBox.Show("Тест должен содержать хотя бы один вопрос!"); // to do : replace
                    return;
                }
                isUsing = value;
                using (DataContext db = new())
                {
                    db.Questions.Update(this);
                    db.SaveChanges();
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsUsing)));
            }
        }
        public int OwnerTestID { get; set; }
        public Test OwnerTest { get; set; }
        public ObservableCollection<Answer> Answers { get; set; } = new();

        public Question(string content, bool isUsing = true)
        {
            this.content = content;
            this.isUsing = isUsing;
            Answers = new();
        }
        public Question(int id, string content, bool isUsing = true) : this(content, isUsing) => ID = id;
        public static Question DefaultQuestion => new("Новый вопрос") { Answers = [Answer.PositiveAnswer, Answer.NegativeAnswer] };

    }
}
