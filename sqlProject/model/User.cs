namespace sqlProject.model
{
    internal class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int TypeID { get; set; }
        public UserType Type { get; set; }

        private User(string name, string password)
        {
            Name = name;
            Password = password;
        }
        public User(string name, string password, UserType type) : this(name, password) => Type = type;
    }
}
