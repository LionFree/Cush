namespace meh.Infrastructure
{
    /// <summary>
    /// Interface that composes the object graph of an application.
    /// The application type must have a public, parameterless constructor.
    /// </summary>
    /// <typeparam name="T">The application type.  Must have a public, parameterless constructor.</typeparam>
    public interface ICompositionRoot<out T>
    {
        /// <summary>
        /// Composes the object graph of an application.
        /// The application type must have a public, parameterless constructor.
        /// </summary>
        /// <typeparam name="T">The application type.  Must have a public, parameterless constructor.</typeparam>
        T ComposeObjectGraph();
    }

    public interface IApplication
    {
        void Run(params string[] args);
    }

    public interface IApplication<out T> : ICompositionRoot<T>, IApplication
    {
    }

    public interface IConfigurable
    {
        void Configure();
    }
}