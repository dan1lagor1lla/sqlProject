using Microsoft.EntityFrameworkCore;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace sqlProject.model
{
    [EntityTypeConfiguration(typeof(TestConfiguration))]
    internal class Test : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private string name;
        private bool isQuestionsOrderRandom;
        public int ID { get; set; }
        public string Name
        {
            get => name;
            set
            {
                if (value == "")
                {
                    MessageBox.Show("Ошибка!"); // todo : replace
                    return;
                }
                name = value;
                using (DataContext db = new())
                {
                    db.Tests.Update(this);
                    db.SaveChanges();
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
        public bool IsQuestionsOrderRandom
        {
            get => isQuestionsOrderRandom;
            set
            {
                IsQuestionsOrderRandom = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsQuestionsOrderRandom)));
            }
        }
        public ObservableCollection<Question> Questions { get; set; } = new();

        public Test(string name, bool isQuestionsOrderRandom = true)
        {
            this.name = name;
            this.isQuestionsOrderRandom = isQuestionsOrderRandom;
        }
        public Test(int id, string name, bool isQuestionsOrderRandom = true) : this(name, isQuestionsOrderRandom) => ID = id;

        public override string ToString() => Name;

        public static Test DefaultTest => new("Новый тест") { Questions = [Question.DefaultQuestion] };
    }
}
