using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scat
{
    public class SqlConnectionStringRule : IRule
    {
        public List<FileLoader> Loaders
        {
            get;
            set;
        }

        private ITemplate template;


        public SqlConnectionStringRule(List<FileLoader> loaders, ITemplate template)
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

        public class SqlConnectionStringAnalyzer
        {
            public List<BaseVulnerability> vulns;
            public FileLoader fileLoader;
            private ITemplate template;

            public SqlConnectionStringAnalyzer(FileLoader l, ITemplate template)
            {
                this.fileLoader = l;
                this.vulns = new List<BaseVulnerability>();
                this.template = template;
            }

            public void Analyze()
            {
                string evil = "SqlConnection(";
                if (this.fileLoader.Raw.Contains("System.Data.SqlClient") && this.fileLoader.Raw.Contains(evil) )
                {
                    foreach (var line in this.fileLoader.Lines)
                    {
                        if (line.Contains(evil))
                        {
                            string lwr = line.ToLower();
                            if (lwr.Contains("pwd") || lwr.Contains("password"))
                            {
                                this.vulns.Add(new GenericVulnerability(this.fileLoader.Filename, "Hard-Coded SqlConnection string", ".", line, Severity.Low, VulnerabilityType.HardCodedPassword));
                            }
                        }
                    }


                }

            }

        }

        public void Evaluate()
        {
            List<SqlConnectionStringAnalyzer> analyzers = new List<SqlConnectionStringAnalyzer>();

            foreach (var l in this.Loaders)
            {
                analyzers.Add(new SqlConnectionStringAnalyzer(l, this.template));
            }

            Parallel.ForEach(analyzers, a =>
            {
                a.Analyze();
            });

            //
            // aggregate the vulnerabilities.
            //
            foreach(var a in analyzers)
            {
                if(a.vulns.Count > 0)
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
