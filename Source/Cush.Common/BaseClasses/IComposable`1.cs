namespace Cush.Common
{
    public interface IComposable<out T>
    {
        T ComposeObjectGraph();
    }
}