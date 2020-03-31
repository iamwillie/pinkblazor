using System;
using System.Reflection;

namespace Share
{
    /// <summary>
    /// Represents a Main Program
    /// </summary>
    /// <typeparam name="T">Type of T</typeparam>
    public class MainProgram<T>
    {
        /// <summary>
        /// Program Version
        /// </summary>
        public static string Version
        {
            get
            {
                var versionNumber = "";

                var fileVersionAttribute = typeof(T).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyFileVersionAttribute>();
                if (fileVersionAttribute != null)
                {
                    var version = new Version(fileVersionAttribute.Version);
                    versionNumber = version.ToString();
                }

                return versionNumber;
            }
        }

        /// <summary>
        /// Program Copyright
        /// </summary>
        public static string Copyright
        {
            get
            {
                var copyRightAttribute = typeof(T).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyCopyrightAttribute>();
                var copyRight = copyRightAttribute?.Copyright;

                return copyRight ?? "";
            }
        }

        /// <summary>
        /// Program Company Name
        /// </summary>
        public static string Company
        {
            get
            {
                var companyAttribute = typeof(T).GetTypeInfo().Assembly.GetCustomAttribute<AssemblyCompanyAttribute>();
                var company = companyAttribute?.Company;

                return company ?? "";
            }
        }
    }
}
