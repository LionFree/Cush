namespace meh.Infrastructure
{
    /// <summary>
    /// Abstraction that composes the object graph of an application.
    /// </summary>
    /// <typeparam name="T">The application type.</typeparam>
    public abstract class AbstractCompositionRoot<T> : ICompositionRoot<T>
    {
        /// <summary>
        /// Composes the object graph of an application.
        /// </summary>
        /// <typeparam name="T">The application type.</typeparam>
        public abstract T ComposeObjectGraph();
    }
    
    /// <summary>
    /// A helper class to compose the object graph of an application.
    /// The application type must have a public, parameterless constructor.
    /// </summary>
    public static class CompositionRoot
    {
        /// <summary>
        /// Composes the object graph of an application.
        /// The application type must have a public, parameterless constructor.
        /// </summary>
        /// <typeparam name="T">The application type.  Must have a public, parameterless constructor.</typeparam>
        public static T ComposeObjectGraph<T>() where T : ICompositionRoot<T>, new()
        {
            return (new T()).ComposeObjectGraph();
        }
    }
    
    /// <summary>
    /// A helper class to compose the object graph of an application.
    /// The application type must have a public, parameterless constructor.
    /// </summary>
    /// <typeparam name="T">The application type.  Must have a public, parameterless constructor.</typeparam>
    public static class CompositionRoot<T> where T : ICompositionRoot<T>, new()
    {
        /// <summary>
        /// Composes the object graph of an application.
        /// The application type must have a public, parameterless constructor.
        /// </summary>
        /// <typeparam name="T">The application type.  Must have a public, parameterless constructor.</typeparam>
        public static T ComposeObjectGraph() 
        {
            return (new T()).ComposeObjectGraph();
        }
    }
}