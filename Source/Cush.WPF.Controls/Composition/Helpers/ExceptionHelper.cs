using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using Cush.Common.Logging;

// ReSharper disable CheckNamespace

namespace Cush.Composition.Helpers
{
    public abstract class ExceptionHelper
    {
        internal static ExceptionHelper GetInstance(ILogger logger)
        {
            return new Implementation(logger);
        }


        public abstract void ShowException(ReflectionTypeLoadException ex);
        public abstract void ShowException(Exception ex);

        private class Implementation : ExceptionHelper
        {
            private readonly ILogger _logger;

            public Implementation(ILogger logger)
            {
                _logger = logger;
            }

            private string AddTypes(string message, Type[] types)
            {
                if (types == null) return message;

                foreach (var type in types)
                {
                    message += Environment.NewLine + "    ";
                    if (type == null)
                    {
                        message += "<null>";
                        continue;
                    }

                    message += type.ToString();
                }

                return message;
            }

            private string AddExceptions(string message, Exception[] exceptions)
            {
                if (exceptions == null) return message;

                foreach (var exception in exceptions)
                {
                    message += UnwrapException(exception);

                    var foundException = exception as FileNotFoundException;
                    if (foundException != null)
                    {
                        ShowException(foundException);
                    }
                }

                return message;
            }

            private static string UnwrapException(Exception ex)
            {
                var message = string.Format(Environment.NewLine + "EXCEPTION TYPE: ({0})", ex.GetType());
                message += Environment.NewLine + "  -------------- ";
                message += Environment.NewLine + ex.Message;

                if (ex.InnerException != null)
                    message += UnwrapException(ex.InnerException);

                return message;
            }

            public override void ShowException(ReflectionTypeLoadException ex)
            {
                var message = "ReflectionTypeLoadException:";
                var types = ex.Types;
                message += Environment.NewLine + "  -------------- ";
                message += Environment.NewLine + "  Types : " +
                           (types == null ? "<null>" : types.Length.ToString(CultureInfo.CurrentUICulture));

                message = AddTypes(message, types);
                message += Environment.NewLine + "  -------------- ";

                message = AddExceptions(message, ex.LoaderExceptions);

                _logger.Trace(message);
            }

            public override void ShowException(Exception ex)
            {
                _logger.Trace("Composition failed: " + ex);
            }

            private void ShowException(FileNotFoundException ex)
            {
                _logger.Trace("File not found: " + ex);
            }

            //        {

            //        for (var i = 0; i < list.Count; i++)
            //        Logger.Trace("  " + item + " : " + list.Count + " parts:");
            //        var list = item.Parts.ToList();
            //    {
            //    foreach (var item in catalogs)
            //{

            //private void DisplayCatalogs(IEnumerable<ComposablePartCatalog> catalogs)
            //            var part = list[i];
            //            var definition = part.ExportDefinitions.FirstOrDefault();
            //            if (definition == null) continue;
            //            object type;
            //            definition.Metadata.TryGetValue("ExportTypeIdentity", out type);
            //            if (type == null) type = "<undefined type>";
            //            Logger.Trace("    " + (i + 1) + " : " + type + " : " + part);
            //        }
            //    }
            //}
        }
    }
}