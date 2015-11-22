using System.Collections;
using System.Runtime.InteropServices;

namespace Cush.Collections
{
    /// <summary>
    /// Represents a non-generic, limited-length collection of objects that can be individually accessed by index.
    /// </summary>
    /// <filterpriority>1</filterpriority>
    [ComVisible(true)]
    
    public interface IBoundedList : IList, ICollection, IEnumerable
    {
        /// <summary>
        ///     Gets the number of elements actually contained in the <see cref="T:BoundedList" />.
        /// </summary>
        /// <returns>
        ///     The number of elements actually contained in the <see cref="T:BoundedList" />.
        /// </returns>
        
        new int Count {  get; }

        /// <summary>
        ///     Gets or sets the total number of elements the internal data structure can hold before resizing.
        /// </summary>
        /// <returns>
        ///     The number of elements that the <see cref="T:BoundedList" /> can contain before resizing is required.
        /// </returns>
        /// <exception cref="T:System.ArgumentOutOfRangeException">
        ///     <see cref="P:BoundedList.Capacity" /> is set to a value that is less than <see cref="P:BoundedList.Count" />.
        /// </exception>
        /// <exception cref="T:System.OutOfMemoryException">There is not enough memory available on the system.</exception>
        
        int Capacity {  get;  set; }
        
        /// <summary>
        ///     Copies the entire <see cref="T:BoundedList" /> to a compatible one-dimensional array, starting at the specified
        ///     index of the target array.
        /// </summary>
        /// <param name="array">
        ///     The one-dimensional <see cref="T:System.Array" /> that is the destination of the elements copied
        ///     from <see cref="T:BoundedList" />. The <see cref="T:System.Array" /> must have zero-based indexing.
        /// </param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array" /> at which copying begins.</param>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="array" /> is null.</exception>
        /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="arrayIndex" /> is less than 0.</exception>
        /// <exception cref="T:System.ArgumentException">
        ///     The number of elements in the source <see cref="T:BoundedList" /> is
        ///     greater than the available space from <paramref name="arrayIndex" /> to the end of the destination
        ///     <paramref name="array" />.
        /// </exception>
        
        void CopyTo(object[] array, int arrayIndex);
    }
}
