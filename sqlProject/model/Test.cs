namespace sqlProject.model
{
    internal class Test
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public bool IsOrderRandom { get; set; }
        public List<Question> Questions { get; set; }
        
        public Test(string name, bool isOrderRandom = true)
        {
            Name = name;
            IsOrderRandom = isOrderRandom;
        }

        public override string ToString() => Name;
    }
}
