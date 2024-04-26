namespace Internal.Scopes.Abstract.Loggers
{
    public interface ILogHeader
    {
        public string Name { get; }
        public bool IsColored { get; }
        public string Color { get; }
    }
}