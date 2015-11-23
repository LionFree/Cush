namespace Cush.Windows
{
    public interface IProcessStarter
    {
        /// <summary>
        ///     Starts a process resource by specifying the name of a document or application file and associates the resource with
        ///     a new <see cref="T:IProcess" /> component.
        /// </summary>
        /// <returns>
        ///     A new <see cref="T:IProcess" /> component that is associated with the process resource, or null,
        ///     if no process resource is started (for example, if an existing process is reused).
        /// </returns>
        IProcess Start(string fileName);
    }
}