namespace sqlProject.model
{
    internal class LoggingType
    {
        public int ID { get; set; }
        public string TypeName { get; set; }
        public List<Logging> Loggings { get; set; }

        private LoggingType(string typeName) => TypeName = typeName;
        private LoggingType(int id, string typeName) : this(typeName) => ID = id;

        public static LoggingType Login => new LoggingType(1, "Вход");
        public static LoggingType Logout => new LoggingType(2, "Выход");
    }
}
