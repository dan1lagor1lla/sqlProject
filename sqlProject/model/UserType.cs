namespace sqlProject.model
{
    internal class UserType
    {
        public int ID { get; set; }
        public string TypeName { get; set; }
        public List<User> Users { get; set; }

        private UserType(string typeName) => TypeName = typeName;            
        private UserType(int id, string typeName) : this(typeName) => ID = id;

        public static UserType Admin => new UserType(1, "Админ");
        public static UserType Student => new UserType(2, "Студент");

    }
}
