using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace scat
{
    public static class Configuration
    {
        public static int MaxRecursionDepth = 5;
        public static string SourceFolder = string.Empty;
        public static Action<string> debug;
        public static object globalLock = new object();
        public static string CustomRulesFilename = string.Empty;
        public static List<FileLoader> Loaders = new List<FileLoader>();



        public static bool bBasicPhpCommandInjection = true;
        public static bool bBasicPhpFileInclusion = true;
        public static bool bBasicPhpSqlInjection = true;

        public static bool bBasicSqlInjection = true;

        public static bool bAdvancedSqlInjection = true;
        public static bool bMildSqlInjection = true;
        public static bool bBasicLdapInjection = true;
        public static bool bBasicCsrf = true;
        public static bool bIisConfig = true;
        public static bool bSqlConnectionString = true;
        public static bool bInlineAspxRule = true;
        public static bool bBasicReflectedXss = true;
        public static bool bAdvancedReflectedXss = true;
        public static bool bBasicOpenRedirect = true;
        public static bool bAdvancedOpenRedirect = true;
        public static bool bBasicArbitraryFileOperations = true;
        public static bool bAdvancedArbitraryFileOperations = true;
        public static bool bBasicCommandInjection = true;
        public static bool bAdvancedCommandInjection = true;
        public static bool bBasicArbitraryFileAccess = true;        
        public static bool bAdvancedArbitraryFileAccess = true;
        public static bool bBasicXPathInjection = true;
        public static bool bAdvancedXPathInjection = true;
        public static bool bWeakPermissions = true;
        public static bool bCookieSecurity = true;
        public static bool bHardCodedPassword = true;
        public static bool bWeakCryptoRule = true;
        public static bool bResourceLeak = true;


    }
}
