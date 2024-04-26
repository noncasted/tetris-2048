namespace Tools.AssembliesViewer.Domain.Abstract
{
    public interface IAssemblyToggles
    {
        bool AllowUnsafeCode { get; set; }
        bool OverrideReferences { get; set; }
        bool AutoReference { get; set; }
        bool NoEngineReferences { get; set; }

        string ToString();
    }
}