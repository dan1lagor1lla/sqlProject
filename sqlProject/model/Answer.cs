namespace sqlProject.model
{
    internal class Answer
    {   
        public int ID { get; set; }
        public string Content { get; set; }
        public bool IsCorrect { get; set; }
        public int OwnerQuestionID { get; set; }
        public Question OwnerQuestion { get; set; }
        private List<AchievementLogging> Loggings { get; set; }

        private Answer(string content, bool isCorrect)
        {
            Content = content;
            IsCorrect = isCorrect;
        }
        public Answer(string content, bool isCorrect, Question ownerQuestion) : this(content, isCorrect) => OwnerQuestion = ownerQuestion;

        public override string ToString() => Content;
    }
}
