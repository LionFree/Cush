namespace Cush.Windows.SingleInstance
{
    /// <summary>
    ///     Represents the method which would be used to retrieve an ISingleInstanceApplication object when instantiating a
    ///     SingleInstanceTracker object.
    ///     If the method returns null, the SingleInstanceTracker's constructor will throw an exception.
    /// </summary>
    /// <returns>An ISingleInstanceApplication object which would receive messages.</returns>
    public delegate ISingleInstanceApplication SingleInstanceDelegate();
}