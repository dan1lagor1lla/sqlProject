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
                    new NotificationWindow("Ошибка!").ShowDialog();
                    return;
                }
                name = value;
                UpdateInDatabase();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Name)));
            }
        }
        public bool IsQuestionsOrderRandom
        {
            get => isQuestionsOrderRandom;
            set
            {
                isQuestionsOrderRandom = value;
                UpdateInDatabase();
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsQuestionsOrderRandom)));
            }
        }
        public ObservableCollection<Question> Questions { get; set; } = new();

        public Test(string name, bool isQuestionsOrderRandom = true)
        {
            this.name = name;
            this.isQuestionsOrderRandom = isQuestionsOrderRandom;
        }

        private void UpdateInDatabase()
        {
            using (DatabaseContext db = new())
            {
                db.Tests.Update(this);
                db.SaveChanges();
            }
        }
        public override string ToString() => Name;
    }
}
