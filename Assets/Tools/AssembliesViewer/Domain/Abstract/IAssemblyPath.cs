namespace Tools.AssembliesViewer.Domain.Abstract
{
    public interface IAssemblyPath
    {
        public string DirectoryName { get; }
        public string Name { get; }
        public string FullPathName { get; }
        public string Raw { get; }
    }
}