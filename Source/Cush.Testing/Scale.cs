namespace Cush.Testing
{
    /// <summary>
    ///     For data types that hold large values (e.g., double data type holds values up
    ///     to 10^308), it is extremely unlikely that any Double chosen at random will have a
    ///     magnitude smaller than 10 ^ "huge".
    ///     This class gives a few options for generating random Doubles at different scales.
    /// </summary>
    public enum Scale
    {
        /// <summary>
        ///     The trivial non-scale.
        /// </summary>
        Unscaled,

        /// <summary>
        ///     The normal scale, where most values will have large magnitude.
        /// </summary>
        Flat,

        /// <summary>
        ///     A scale where values span the entire range, but spike exponentially around 0.
        ///     This will generate numbers much closer to zero.
        ///     For data types that hold large values, this is much more likely than the Flat
        ///     scale to generate numbers that humans are 'familiar' with.
        /// </summary>
        Exponential
    }
}