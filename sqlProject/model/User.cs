namespace sqlProject.model
{
    internal class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int TypeID { get; set; }
        public UserType Type { get; set; }

        private User(string login, string email, string password)
        {
            Login = login;
            Email = email;
            Password = password;
        }
        public User(string login, string email, string password, UserType type) : this(login, email, password) => Type = type;
    }
}
