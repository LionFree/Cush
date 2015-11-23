namespace Cush.Windows.Services
{
    /// <summary>
    ///     An interface for generating <see cref="ServiceHarness" />es around <see cref="T:WindowsService" />s.
    /// </summary>
    public interface IServiceWrapper
    {
        /// <summary>
        ///     Create a new <see cref="ServiceHarness" /> from the given <see cref="WindowsService" />.
        /// </summary>
        ServiceHarness WrapService(WindowsService service);
    }
}