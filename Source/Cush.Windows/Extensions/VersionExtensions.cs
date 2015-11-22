using System;

namespace Cush.Windows
{
    public static class VersionExtensions
    {
        /// <summary>
        /// Convert an unmodified Windows assembly version to the datetime it represents.
        /// Note: This is not as reliable as using the 
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(this Version version)
        {
            var results = new DateTime(2000, 1, 1);
            results = results.AddDays(version.Build);
            results = results.AddSeconds(version.Revision*2);
            results = results.ToLocalTime();
            return results;
        }
    }
}