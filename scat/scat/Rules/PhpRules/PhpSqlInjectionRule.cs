using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scat
{
    public class PhpSqlInjectionRule : IRule
    {
        public List<FileLoader> Loaders
        {
            get;
            set;
        }


        private ITemplate template;

        public PhpSqlInjectionRule(List<FileLoader> loaders, ITemplate template)
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

        public class PhpFileInclusionAnalyzer
        {
            public List<BaseVulnerability> vulns;
            public FileLoader fileLoader;
            private ITemplate template;

            public PhpFileInclusionAnalyzer(FileLoader l, ITemplate template)
            {
                this.fileLoader = l;
                this.vulns = new List<BaseVulnerability>();
                this.template = template;
            }

            public void Analyze()
            {
                if (this.fileLoader.Filename.EndsWith(".php"))
                {
                    //
                    //  test the basecase first.
                    //

                    foreach (var line in this.fileLoader.Lines)
                    {
                        if (line.Contains("mysql_query(") && PhpUtil.ContainsUserInput(line))
                        {
                            this.vulns.Add(this.template.GetVulnerability(this.fileLoader.Filename, "Php Sql Injection", line));
                        }
                    }

                    //
                    // ok, now see if we get all the tainted variables and see if those are used in a mysql line.
                    //

                    List<Tuple<string,string>> taintedVariables = PhpUtil.EnumerateTaintedVariables(this.fileLoader.Lines);
                    foreach (var taintedLove in taintedVariables)
                    {
                        foreach (var line in this.fileLoader.Lines)
                        {
                            if (line.Contains("mysql_query(") && line.Contains(taintedLove.Item1))
                            {
                                this.vulns.Add(this.template.GetVulnerability(this.fileLoader.Filename, "Php Sql Injection", line + "<-->" + taintedLove.Item2));
                            }
                        }
                    }
          
                }
                       
            }

        }

        public void Evaluate()
        {
            List<PhpFileInclusionAnalyzer> analyzers = new List<PhpFileInclusionAnalyzer>();

            foreach (var l in this.Loaders)
            {
                analyzers.Add( new PhpFileInclusionAnalyzer(l, this.template));
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
