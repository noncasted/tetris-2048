namespace Internal.Scopes.Abstract.Options
{
    public interface IOptions
    {
        T GetOptions<T>() where T : class, IOptionsEntry;
    }
}