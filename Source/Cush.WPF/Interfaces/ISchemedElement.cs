namespace Cush.WPF.Interfaces
{
    public interface ISchemedElement : IResourceContainer
    {
        IColorScheme CurrentScheme { get; set; }
    }
}