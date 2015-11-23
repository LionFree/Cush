namespace meh.Logging
{
    interface ILoggerNope
    {
        ///// <overloads>
        ///// Writes the diagnostic message at the <c>Trace</c> level using the specified format provider and format parameters.
        ///// </overloads>
        ///// <summary>
        ///// Writes the diagnostic message at the <c>Trace</c> level.
        ///// </summary>
        ///// <typeparam name="T">Type of the value.</typeparam>
        ///// <param name="value">The value to be written.</param>
        //void Trace<T>(T value);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Trace</c> level.
        ///// </summary>
        ///// <typeparam name="T">Type of the value.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="value">The value to be written.</param>
        //void Trace<T>(IFormatProvider formatProvider, T value);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Trace</c> level using the specified parameters and formatting them with the supplied format provider.
        ///// </summary>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing format items.</param>
        ///// <param name="args">Arguments to format.</param>
        //[StringFormatMethod("message")]
        //void Trace(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

        ///// <summary>
        ///// Writes the diagnostic message and exception at the <c>Trace</c> level.
        ///// </summary>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> to be written.</param>
        ///// <param name="exception">An exception to be logged.</param>
        ///// <param name="args">Arguments to format.</param>
        //void Trace(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message,
        //    params object[] args);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Trace</c> level using the specified parameter and formatting it with the supplied format provider.
        ///// </summary>
        ///// <typeparam name="TArgument">The type of the argument.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument">The argument to format.</param>
        //[StringFormatMethod("message")]
        //void Trace<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Trace</c> level using the specified parameter.
        ///// </summary>
        ///// <typeparam name="TArgument">The type of the argument.</typeparam>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument">The argument to format.</param>
        //[StringFormatMethod("message")]
        //void Trace<TArgument>([Localizable(false)] string message, TArgument argument);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Trace</c> level using the specified arguments formatting it with the supplied format provider.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        //[StringFormatMethod("message")]
        //void Trace<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message,
        //    TArgument1 argument1, TArgument2 argument2);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Trace</c> level using the specified parameters.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        //[StringFormatMethod("message")]
        //void Trace<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1,
        //    TArgument2 argument2);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Trace</c> level using the specified arguments formatting it with the supplied format provider.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <typeparam name="TArgument3">The type of the third argument.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        ///// <param name="argument3">The third argument to format.</param>
        //[StringFormatMethod("message")]
        //void Trace<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider,
        //    [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Trace</c> level using the specified parameters.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <typeparam name="TArgument3">The type of the third argument.</typeparam>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        ///// <param name="argument3">The third argument to format.</param>
        //[StringFormatMethod("message")]
        //void Trace<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1,
        //    TArgument2 argument2, TArgument3 argument3);

        // NLog-specific Items
        // ================================================================================

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Debug</c> level using the specified parameters and formatting them with the supplied format provider.
        ///// </summary>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing format items.</param>
        ///// <param name="args">Arguments to format.</param>
        //[StringFormatMethod("message")]
        //[Both]
        //void Debug(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

        ///// <summary>
        ///// Writes the diagnostic message and exception at the <c>Debug</c> level.
        ///// </summary>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> to be written.</param>
        ///// <param name="exception">An exception to be logged.</param>
        ///// <param name="args">Arguments to format.</param>
        //[NLog]
        //void Debug(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message,
        //    params object[] args);

        ///// <overloads>
        ///// Writes the diagnostic message at the <c>Debug</c> level using the specified format provider and format parameters.
        ///// </overloads>
        ///// <summary>
        ///// Writes the diagnostic message at the <c>Debug</c> level.
        ///// </summary>
        ///// <typeparam name="T">Type of the value.</typeparam>
        ///// <param name="value">The value to be written.</param>
        //void Debug<T>(T value);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Debug</c> level.
        ///// </summary>
        ///// <typeparam name="T">Type of the value.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="value">The value to be written.</param>
        //void Debug<T>(IFormatProvider formatProvider, T value);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Debug</c> level using the specified parameter and formatting it with the supplied format provider.
        ///// </summary>
        ///// <typeparam name="TArgument">The type of the argument.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument">The argument to format.</param>
        //[StringFormatMethod("message")]
        //void Debug<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Debug</c> level using the specified parameter.
        ///// </summary>
        ///// <typeparam name="TArgument">The type of the argument.</typeparam>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument">The argument to format.</param>
        //[StringFormatMethod("message")]
        //void Debug<TArgument>([Localizable(false)] string message, TArgument argument);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Debug</c> level using the specified arguments formatting it with the supplied format provider.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        //[StringFormatMethod("message")]
        //void Debug<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message,
        //    TArgument1 argument1, TArgument2 argument2);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Debug</c> level using the specified parameters.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        //[StringFormatMethod("message")]
        //void Debug<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1,
        //    TArgument2 argument2);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Debug</c> level using the specified arguments formatting it with the supplied format provider.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <typeparam name="TArgument3">The type of the third argument.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        ///// <param name="argument3">The third argument to format.</param>
        //[StringFormatMethod("message")]
        //void Debug<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider,
        //    [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Debug</c> level using the specified parameters.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <typeparam name="TArgument3">The type of the third argument.</typeparam>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        ///// <param name="argument3">The third argument to format.</param>
        //[StringFormatMethod("message")]
        //void Debug<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1,
        //    TArgument2 argument2, TArgument3 argument3);





        ///// <overloads>
        ///// Writes the diagnostic message at the <c>Info</c> level using the specified format provider and format parameters.
        ///// </overloads>
        ///// <summary>
        ///// Writes the diagnostic message at the <c>Info</c> level.
        ///// </summary>
        ///// <typeparam name="T">Type of the value.</typeparam>
        ///// <param name="value">The value to be written.</param>
        //void Info<T>(T value);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Info</c> level.
        ///// </summary>
        ///// <typeparam name="T">Type of the value.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="value">The value to be written.</param>
        //void Info<T>(IFormatProvider formatProvider, T value);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Info</c> level using the specified parameters and formatting them with the supplied format provider.
        ///// </summary>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing format items.</param>
        ///// <param name="args">Arguments to format.</param>
        //[StringFormatMethod("message")]
        //void Info(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

        ///// <summary>
        ///// Writes the diagnostic message and exception at the <c>Info</c> level.
        ///// </summary>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> to be written.</param>
        ///// <param name="exception">An exception to be logged.</param>
        ///// <param name="args">Arguments to format.</param>
        //void Info(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message,
        //    params object[] args);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Info</c> level using the specified parameter and formatting it with the supplied format provider.
        ///// </summary>
        ///// <typeparam name="TArgument">The type of the argument.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument">The argument to format.</param>
        //[StringFormatMethod("message")]
        //void Info<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Info</c> level using the specified parameter.
        ///// </summary>
        ///// <typeparam name="TArgument">The type of the argument.</typeparam>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument">The argument to format.</param>
        //[StringFormatMethod("message")]
        //void Info<TArgument>([Localizable(false)] string message, TArgument argument);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Info</c> level using the specified arguments formatting it with the supplied format provider.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        //[StringFormatMethod("message")]
        //void Info<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message,
        //    TArgument1 argument1, TArgument2 argument2);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Info</c> level using the specified parameters.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        //[StringFormatMethod("message")]
        //void Info<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1,
        //    TArgument2 argument2);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Info</c> level using the specified arguments formatting it with the supplied format provider.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <typeparam name="TArgument3">The type of the third argument.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        ///// <param name="argument3">The third argument to format.</param>
        //[StringFormatMethod("message")]
        //void Info<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider,
        //    [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Info</c> level using the specified parameters.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <typeparam name="TArgument3">The type of the third argument.</typeparam>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        ///// <param name="argument3">The third argument to format.</param>
        //[StringFormatMethod("message")]
        //void Info<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1,
        //    TArgument2 argument2, TArgument3 argument3);

        ///// <overloads>
        ///// Writes the diagnostic message at the <c>Warn</c> level using the specified format provider and format parameters.
        ///// </overloads>
        ///// <summary>
        ///// Writes the diagnostic message at the <c>Warn</c> level.
        ///// </summary>
        ///// <typeparam name="T">Type of the value.</typeparam>
        ///// <param name="value">The value to be written.</param>
        //void Warn<T>(T value);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Warn</c> level.
        ///// </summary>
        ///// <typeparam name="T">Type of the value.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="value">The value to be written.</param>
        //void Warn<T>(IFormatProvider formatProvider, T value);


        ///// <summary>
        ///// Writes the diagnostic message at the <c>Warn</c> level using the specified parameters and formatting them with the supplied format provider.
        ///// </summary>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing format items.</param>
        ///// <param name="args">Arguments to format.</param>
        //[StringFormatMethod("message")]
        //void Warn(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

        ///// <summary>
        ///// Writes the diagnostic message and exception at the <c>Warn</c> level.
        ///// </summary>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> to be written.</param>
        ///// <param name="exception">An exception to be logged.</param>
        ///// <param name="args">Arguments to format.</param>
        //void Warn(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message,
        //    params object[] args);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Warn</c> level using the specified parameter and formatting it with the supplied format provider.
        ///// </summary>
        ///// <typeparam name="TArgument">The type of the argument.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument">The argument to format.</param>
        //[StringFormatMethod("message")]
        //void Warn<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Warn</c> level using the specified parameter.
        ///// </summary>
        ///// <typeparam name="TArgument">The type of the argument.</typeparam>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument">The argument to format.</param>
        //[StringFormatMethod("message")]
        //void Warn<TArgument>([Localizable(false)] string message, TArgument argument);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Warn</c> level using the specified arguments formatting it with the supplied format provider.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        //[StringFormatMethod("message")]
        //void Warn<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message,
        //    TArgument1 argument1, TArgument2 argument2);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Warn</c> level using the specified parameters.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        //[StringFormatMethod("message")]
        //void Warn<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1,
        //    TArgument2 argument2);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Warn</c> level using the specified arguments formatting it with the supplied format provider.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <typeparam name="TArgument3">The type of the third argument.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        ///// <param name="argument3">The third argument to format.</param>
        //[StringFormatMethod("message")]
        //void Warn<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider,
        //    [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Warn</c> level using the specified parameters.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <typeparam name="TArgument3">The type of the third argument.</typeparam>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        ///// <param name="argument3">The third argument to format.</param>
        //[StringFormatMethod("message")]
        //void Warn<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1,
        //    TArgument2 argument2, TArgument3 argument3);


        ///// <overloads>
        ///// Writes the diagnostic message at the <c>Error</c> level using the specified format provider and format parameters.
        ///// </overloads>
        ///// <summary>
        ///// Writes the diagnostic message at the <c>Error</c> level.
        ///// </summary>
        ///// <typeparam name="T">Type of the value.</typeparam>
        ///// <param name="value">The value to be written.</param>
        //void Error<T>(T value);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Error</c> level.
        ///// </summary>
        ///// <typeparam name="T">Type of the value.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="value">The value to be written.</param>
        //void Error<T>(IFormatProvider formatProvider, T value);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Error</c> level using the specified parameters and formatting them with the supplied format provider.
        ///// </summary>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing format items.</param>
        ///// <param name="args">Arguments to format.</param>
        //[StringFormatMethod("message")]
        //void Error(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

        ///// <summary>
        ///// Writes the diagnostic message and exception at the <c>Error</c> level.
        ///// </summary>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> to be written.</param>
        ///// <param name="exception">An exception to be logged.</param>
        ///// <param name="args">Arguments to format.</param>
        //void Error(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message,
        //    params object[] args);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Error</c> level using the specified parameter and formatting it with the supplied format provider.
        ///// </summary>
        ///// <typeparam name="TArgument">The type of the argument.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument">The argument to format.</param>
        //[StringFormatMethod("message")]
        //void Error<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Error</c> level using the specified parameter.
        ///// </summary>
        ///// <typeparam name="TArgument">The type of the argument.</typeparam>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument">The argument to format.</param>
        //[StringFormatMethod("message")]
        //void Error<TArgument>([Localizable(false)] string message, TArgument argument);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Error</c> level using the specified arguments formatting it with the supplied format provider.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        //[StringFormatMethod("message")]
        //void Error<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message,
        //    TArgument1 argument1, TArgument2 argument2);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Error</c> level using the specified parameters.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        //[StringFormatMethod("message")]
        //void Error<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1,
        //    TArgument2 argument2);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Error</c> level using the specified arguments formatting it with the supplied format provider.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <typeparam name="TArgument3">The type of the third argument.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        ///// <param name="argument3">The third argument to format.</param>
        //[StringFormatMethod("message")]
        //void Error<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider,
        //    [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Error</c> level using the specified parameters.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <typeparam name="TArgument3">The type of the third argument.</typeparam>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        ///// <param name="argument3">The third argument to format.</param>
        //[StringFormatMethod("message")]
        //void Error<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1,
        //    TArgument2 argument2, TArgument3 argument3);



        ///// <overloads>
        ///// Writes the diagnostic message at the <c>Fatal</c> level using the specified format provider and format parameters.
        ///// </overloads>
        ///// <summary>
        ///// Writes the diagnostic message at the <c>Fatal</c> level.
        ///// </summary>
        ///// <typeparam name="T">Type of the value.</typeparam>
        ///// <param name="value">The value to be written.</param>
        //void Fatal<T>(T value);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Fatal</c> level.
        ///// </summary>
        ///// <typeparam name="T">Type of the value.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="value">The value to be written.</param>
        //void Fatal<T>(IFormatProvider formatProvider, T value);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Fatal</c> level using the specified parameters and formatting them with the supplied format provider.
        ///// </summary>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing format items.</param>
        ///// <param name="args">Arguments to format.</param>
        //[StringFormatMethod("message")]
        //void Fatal(IFormatProvider formatProvider, [Localizable(false)] string message, params object[] args);

        ///// <summary>
        ///// Writes the diagnostic message and exception at the <c>Fatal</c> level.
        ///// </summary>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> to be written.</param>
        ///// <param name="exception">An exception to be logged.</param>
        ///// <param name="args">Arguments to format.</param>
        //void Fatal(Exception exception, IFormatProvider formatProvider, [Localizable(false)] string message,
        //    params object[] args);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Fatal</c> level using the specified parameter and formatting it with the supplied format provider.
        ///// </summary>
        ///// <typeparam name="TArgument">The type of the argument.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument">The argument to format.</param>
        //[StringFormatMethod("message")]
        //void Fatal<TArgument>(IFormatProvider formatProvider, [Localizable(false)] string message, TArgument argument);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Fatal</c> level using the specified parameter.
        ///// </summary>
        ///// <typeparam name="TArgument">The type of the argument.</typeparam>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument">The argument to format.</param>
        //[StringFormatMethod("message")]
        //void Fatal<TArgument>([Localizable(false)] string message, TArgument argument);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Fatal</c> level using the specified arguments formatting it with the supplied format provider.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        //[StringFormatMethod("message")]
        //void Fatal<TArgument1, TArgument2>(IFormatProvider formatProvider, [Localizable(false)] string message,
        //    TArgument1 argument1, TArgument2 argument2);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Fatal</c> level using the specified parameters.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        //[StringFormatMethod("message")]
        //void Fatal<TArgument1, TArgument2>([Localizable(false)] string message, TArgument1 argument1,
        //    TArgument2 argument2);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Fatal</c> level using the specified arguments formatting it with the supplied format provider.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <typeparam name="TArgument3">The type of the third argument.</typeparam>
        ///// <param name="formatProvider">An IFormatProvider that supplies culture-specific formatting information.</param>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        ///// <param name="argument3">The third argument to format.</param>
        //[StringFormatMethod("message")]
        //void Fatal<TArgument1, TArgument2, TArgument3>(IFormatProvider formatProvider,
        //    [Localizable(false)] string message, TArgument1 argument1, TArgument2 argument2, TArgument3 argument3);

        ///// <summary>
        ///// Writes the diagnostic message at the <c>Fatal</c> level using the specified parameters.
        ///// </summary>
        ///// <typeparam name="TArgument1">The type of the first argument.</typeparam>
        ///// <typeparam name="TArgument2">The type of the second argument.</typeparam>
        ///// <typeparam name="TArgument3">The type of the third argument.</typeparam>
        ///// <param name="message">A <see langword="string" /> containing one format item.</param>
        ///// <param name="argument1">The first argument to format.</param>
        ///// <param name="argument2">The second argument to format.</param>
        ///// <param name="argument3">The third argument to format.</param>
        //[StringFormatMethod("message")]
        //void Fatal<TArgument1, TArgument2, TArgument3>([Localizable(false)] string message, TArgument1 argument1,
        //    TArgument2 argument2, TArgument3 argument3);
    }
}
