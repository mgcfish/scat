using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scat
{
    public class BasicLdapInjectionRule : IRule
    {
        public List<FileLoader> Loaders
        {
            get;
            set;
        }

        private ITemplate template;

        public BasicLdapInjectionRule(List<FileLoader> loaders, ITemplate template)
        {
            this.Loaders = loaders;
            this.Vulnerabilities = new List<BaseVulnerability>();
            this.template = template;
        }

        private List<BaseVulnerability> Vulnerabilities;

        public IEnumerable<BaseVulnerability> GetVulnerabilities()
        {
            return this.Vulnerabilities;
        }

        public string GetRuleName()
        {
            return this.template.GetRuleName();
        }

        public class BasicLdapInjectionAnalyzer
        {
            public List<BaseVulnerability> vulns;
            public FileLoader fileLoader;
            private ITemplate template;

            public BasicLdapInjectionAnalyzer(FileLoader l, ITemplate template)
            {
                this.fileLoader = l;
                this.vulns = new List<BaseVulnerability>();
                this.template = template;
            }

            public void Analyze()
            {
                string lwrFilename = this.fileLoader.Filename.ToLower();
                string raw = this.fileLoader.Raw;

                if (lwrFilename.EndsWith(".cs"))
                {
                    if (raw.Contains("System.DirectoryServices") && raw.Contains("DirectorySearcher") && raw.Contains(".Filter"))
                    {
                        if (Util.ContainsScaryInput(raw))
                        {
                            string[] lines = this.fileLoader.Lines;

                            foreach (string line in lines)
                            {
                                if (line.Contains(".Filter") && line.Contains("=") && !line.Contains("==") && !line.Contains("!="))
                                {
                                    if (Util.ContainsScaryInput(line))
                                    {
                                        string lh = line.Substring(0, line.IndexOf("="));
                                        if (lh.Contains(".Filter"))
                                        {
                                            this.vulns.Add(new GenericVulnerability(this.fileLoader.Filename, "Potential Ldap Injection Vulnerability", fileLoader.Filename, line, Severity.Medium, VulnerabilityType.LdapInjection));
                                        }
                                    }
                                }
                            }

                            List<Variable> taintedVariables = Util.EnumerateTaintedVariables(this.fileLoader.Raw);

                            foreach (var taintedVariable in taintedVariables)
                            {
                                foreach(var line in lines)
                                {
                                    if (line.Contains(".Filter") && line.Contains(taintedVariable.VariableName))
                                    {
                                        string message = "<table>";
                                        message += string.Format("<tr>   <td>Tainted Variable</td>   <td>{0}</td>     </tr>", taintedVariable.VariableCode);
                                        message += string.Format("<tr>   <td>Assignment to Filter</td>   <td>{0}</td>     </tr>", line);
                                        message += "</table>";

                                        this.vulns.Add(new GenericVulnerability(this.fileLoader.Filename, "Potential Ldap Injection Vulnerability", fileLoader.Filename, message, Severity.Medium, VulnerabilityType.LdapInjection));
                                    }
                                }
                            }


                        }
                    }
                }
            }
        }

        public void Evaluate()
        {
            List<BasicLdapInjectionAnalyzer> analyzers = new List<BasicLdapInjectionAnalyzer>();

            foreach (var l in this.Loaders)
            {
                analyzers.Add(new BasicLdapInjectionAnalyzer(l, this.template));
            }

            Parallel.ForEach(analyzers, a =>
            {
                a.Analyze();
            });

            //
            // aggregate the vulnerabilities.
            //
            foreach (var a in analyzers)
            {
                if (a.vulns.Count > 0)
                {
                    foreach (var v in a.vulns)
                    {
                        this.Vulnerabilities.Add(v);
                    }
                }
            }
        }
    }
}
