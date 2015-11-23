using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Cush.Composition.Helpers
{
    public static class ExceptionHelper
    {
        public static void ShowException(ReflectionTypeLoadException ex)
        {
            var message = "ReflectionTypeLoadException:";
            var types = ex.Types;
            message += Environment.NewLine + "  -------------- ";
            message += Environment.NewLine + "  Types : " +
                       (types == null ? "<null>" : types.Count().ToString(CultureInfo.CurrentUICulture));

            if (types != null)
            {
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
            }

            message += Environment.NewLine + "  -------------- ";

            var exe = ex.LoaderExceptions;
            foreach (var exception in exe)
            {
                message += Environment.NewLine + exception;
                message += Environment.NewLine + "  -------------- ";

                message += string.Format(Environment.NewLine + "EXCEPTION TYPE: ({0})", exception.GetType());
                var foundException = exception as FileNotFoundException;
                if (foundException != null)
                {
                    ShowException(foundException);
                }
            }

            Trace.WriteLine(message);
        }

        public static void ShowException(Exception ex)
        {
            Trace.WriteLine("Composition failed: " + ex);
        }

        public static void ShowException(FileNotFoundException ex)
        {
            Trace.WriteLine("File not found: " + ex);
        }

        internal static void DisplayCatalogs(IEnumerable<ComposablePartCatalog> catalogs)
        {
            foreach (var item in catalogs)
            {
                var list = item.Parts.ToList();
                Trace.WriteLine("  " + item + " : " + list.Count + " parts:");

                for (var i = 0; i < list.Count; i++)
                {
                    var part = list[i];
                    var definition = part.ExportDefinitions.FirstOrDefault();
                    if (definition == null) continue;
                    object type;
                    definition.Metadata.TryGetValue("ExportTypeIdentity", out type);
                    if (type == null) type = "<undefined type>";
                    Trace.WriteLine("    " + (i + 1) + " : " + type + " : " + part);
                }
            }
        }
    }
}