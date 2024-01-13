namespace sqlProject.model
{
    internal class LoggingType
    {
        public int ID { get; set; }
        public string TypeName { get; set; }
        public List<Logging> Loggings { get; set; }

        public LoggingType(string typeName) => TypeName = typeName;
        public LoggingType(int id, string typeName) : this(typeName) => ID = id;
    }
}
