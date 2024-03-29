﻿using Microsoft.EntityFrameworkCore;
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
                    new NotificationWindow("Ошибка").ShowDialog();
                    return;
                }
                content = value;
                UpdateInDatabase();
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
                    new NotificationWindow("Тест должен содержать хотя бы один вопрос!").ShowDialog();
                    return;
                }
                isUsing = value;
                UpdateInDatabase();
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

        private void UpdateInDatabase()
        {
            using (DatabaseContext db = new())
            {
                db.Questions.Update(this);
                db.SaveChanges();
            }
        }
    }
}
