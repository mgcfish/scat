using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace scat
{
    public static class RulesBuilder
    {

        private static List<IRule> GetCustomRules(List<FileLoader> loaders)
        {
            List<IRule> retval = new List<IRule>();

            if(!string.IsNullOrEmpty(Configuration.CustomRulesFilename))
            {
                if(File.Exists(Configuration.CustomRulesFilename))
                {
                    string[] lines = File.ReadAllLines(Configuration.CustomRulesFilename);

                    foreach (string line in lines)
                    {
                        if (!string.IsNullOrEmpty(line))
                        {
                            string trimmedLine = line.Trim();
                            if (!trimmedLine.StartsWith("#"))
                            {
                                string[] tokens = trimmedLine.Split(",".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                                if (tokens.Length == 2)
                                {
                                    string ruleName = tokens[0].Trim();
                                    string scaryMethod = tokens[1].Trim();
                                    //
                                    // now things get wierd.
                                    //

                                    retval.Add(new AdvancedRule(loaders, new CustomTemplate(ruleName, scaryMethod)));
                                    retval.Add(new BasicRule(loaders, new CustomTemplate(ruleName, scaryMethod)));
                                }
                            }
                        }
                    }
                }
            }

            return retval;
        }
        public static List<IRule> GetRules(List<FileLoader> loaders)
        {
            List<IRule> retval = new List<IRule>();

            List<IRule> customRules = GetCustomRules(loaders);
            foreach (var cr in customRules)
            {
                retval.Add(cr);
            }

            if (Configuration.bBasicPhpCommandInjection)
            {
                retval.Add(new PhpCommandInjectionRule(loaders, new PhpCommandInjectionTemplate()));
            }

            if (Configuration.bBasicPhpFileInclusion)
            {
                retval.Add(new PhpFileInclusionRule(loaders, new PhpFileInclusionTemplate()));
            }

            if (Configuration.bBasicPhpSqlInjection)
            {
                retval.Add(new PhpSqlInjectionRule(loaders, new PhpSqlInjectionTemplate()));
            }

            if (Configuration.bBasicSqlInjection)
            {
                retval.Add(new BasicRule(loaders, new SqlInjectionTemplate()));
            }

            if (Configuration.bAdvancedSqlInjection)
            {
                retval.Add(new AdvancedRule(loaders, new SqlInjectionTemplate()));
            }

            if (Configuration.bMildSqlInjection)
            {
                retval.Add(new MildSqlInjectionRule(loaders, null));
            }

            if (Configuration.bBasicLdapInjection)
            {
                retval.Add(new BasicLdapInjectionRule(loaders, null));
            }

            if (Configuration.bBasicCsrf)
            {
                retval.Add(new BasicCsrfRule(loaders, null));
            }

            if (Configuration.bIisConfig)
            {
                retval.Add(new IISConfigRule(loaders, null));
            }

            if (Configuration.bSqlConnectionString)
            {
                retval.Add(new SqlConnectionStringRule(loaders, null));
            }

            if (Configuration.bInlineAspxRule)
            {
                retval.Add(new InlineAspxRule(loaders, null));
            }

            if (Configuration.bBasicReflectedXss)
            {
                retval.Add(new BasicRule(loaders, new ReflectedXSSTemplate()));
            }

            if (Configuration.bAdvancedReflectedXss)
            {
                retval.Add(new AdvancedRule(loaders, new ReflectedXSSTemplate()));
            }

            if (Configuration.bBasicOpenRedirect)
            {
                retval.Add(new BasicRule(loaders, new OpenRedirectTemplate()));
            }

            if (Configuration.bAdvancedOpenRedirect)
            {
                retval.Add(new AdvancedRule(loaders, new OpenRedirectTemplate()));
            }

            if (Configuration.bBasicArbitraryFileAccess)
            {
                retval.Add(new BasicRule(loaders, new ArbitraryFileOperationsTemplate()));
            }

            if (Configuration.bAdvancedArbitraryFileOperations)
            {
                retval.Add(new AdvancedRule(loaders, new ArbitraryFileOperationsTemplate()));
            }

            if (Configuration.bBasicCommandInjection)
            {
                retval.Add(new BasicRule(loaders, new CommandInjectionTemplate()));
            }

            if (Configuration.bAdvancedCommandInjection)
            {
                retval.Add(new AdvancedRule(loaders, new CommandInjectionTemplate()));
            }

            if (Configuration.bBasicArbitraryFileAccess)
            {
                retval.Add(new BasicRule(loaders, new ArbitraryFileAccessTemplate()));
            }

            if (Configuration.bAdvancedArbitraryFileAccess)
            {
                retval.Add(new AdvancedRule(loaders, new ArbitraryFileAccessTemplate()));
            }

            if (Configuration.bBasicXPathInjection)
            {
                retval.Add(new BasicRule(loaders, new XPathInjectionTemplate()));
            }

            if (Configuration.bAdvancedXPathInjection)
            {
                retval.Add(new AdvancedRule(loaders, new XPathInjectionTemplate()));
            }

            if (Configuration.bWeakPermissions)
            {
                retval.Add(new TextRule(loaders, new WeakPermissionsTemplate(), WeakPermissionsTemplate.suspiciousWords));
            }

            if (Configuration.bCookieSecurity)
            {
                retval.Add(new CookieSecurityRule(loaders, new CookieSecurityTemplate()));
            }

            if (Configuration.bHardCodedPassword)
            {
                retval.Add(new HardCodedPasswordRule(loaders, new HardCodedPasswordTemplate()));
            }

            if (Configuration.bWeakCryptoRule)
            {
                retval.Add(new WeakCryptoRule(loaders, null));
            }

            if (Configuration.bResourceLeak)
            {
                retval.Add(new ResourceLeakRule(loaders, null));
            }
            

            return retval;
        }
    }
}
