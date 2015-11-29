using System.Collections;
using System.Collections.Generic;

namespace Cush.Common
{
    /// <summary>
    ///     Represents a collection of objects that can be individually accessed by index.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    /// <filterpriority>1</filterpriority>
    //[TypeDependency("System.SZArrayHelper")]
    
    internal interface IBoundedList<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        /// <summary>
        ///     Gets or sets the total number of elements the internal data structure will hold before trimming the excess.
        /// </summary>
        /// <returns>
        ///     The number of elements that the internal data structure will contain before trimming the excess.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <see cref="P:Capacity" /> is set to a value that is less than <see cref="P:Count" />.
        /// </exception>
        /// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
        int Capacity {  get;  set; }

        /// <summary>
        ///     Copies the elements of the <see cref="T:Cush.Common.IBoundedList" /> to a new array.
        /// </summary>
        /// <returns>
        ///     An array containing copies of the elements of the <see cref="T:Cush.Common.IBoundedList" />.
        /// </returns>
        T[] ToArray();
    }
}