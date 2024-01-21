namespace sqlProject.model
{
    internal class User
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public string? Email { get; set; }
        public string Password { get; set; }
        public int UserTypeID { get; set; }
        public UserType UserType { get; set; }

        private User(string login, string password, string? email = null)
        {
            Login = login;
            Email = email;
            Password = password;
        }
        public User(string login, string password, UserType userType, string? email = null) : this(login, password, email) => UserType = userType;
        public User(string login, string password, int userTypeID, string? email = null) : this(login, password, email) => UserTypeID = userTypeID;
    }
}
