namespace sqlProject.model
{
    internal class UserType
    {
        public int ID { get; set; }
        public string TypeName { get; set; }
        public List<User> Users { get; set; }

        public UserType(string typeName) => TypeName = typeName;            
        public UserType(int id, string typeName) : this(typeName) => ID = id;

    }
}
