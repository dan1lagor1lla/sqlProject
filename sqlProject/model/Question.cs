namespace sqlProject.model
{
    internal class Question
    {
        public int ID { get; set; }
        public string Content { get; set; }
        public bool IsUsing { get; set; }
        public int OwnerTestID { get; set; }
        public Test OwnerTest { get; set; }
        public List<Answer> Answers { get; set; }

        private Question(string content, bool isUsing = true)
        {
            Content = content;
            IsUsing = isUsing;
        }
        public Question(string content, Test ownerTest, bool isUsing = true) : this(content, isUsing) => OwnerTest = ownerTest;

    }
}
