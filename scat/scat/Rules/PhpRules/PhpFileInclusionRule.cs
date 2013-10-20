using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scat
{
    public class PhpFileInclusionRule : IRule
    {
        public List<FileLoader> Loaders
        {
            get;
            set;
        }


        private ITemplate template;

        public PhpFileInclusionRule(List<FileLoader> loaders, ITemplate template)
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
                string[] inclusionDirectives = { "include", "require" };

                if (this.fileLoader.Filename.EndsWith(".php"))
                {
                    foreach (var line in this.fileLoader.Lines)
                    {
                        //
                        // If the line contains an include and a $_GET then flag it as being stupid.
                        //
                        foreach (var inclusionDirective in inclusionDirectives)
                        {
                            if (PhpUtil.ContainsUserInput(line) && line.Contains(inclusionDirective))
                            {
                                this.vulns.Add(this.template.GetVulnerability(this.fileLoader.Filename, "Local File Inclusion", line + "<-->" + inclusionDirective));
                            }
                        }
                    }

                    List<Tuple<string, string>> taintedLove = PhpUtil.EnumerateTaintedVariables(this.fileLoader.Lines);

                    foreach (var t in taintedLove)
                    {
                        foreach (var l in this.fileLoader.Lines)
                        {
                            foreach (string inclusionDirective in inclusionDirectives)
                            {
                                if (l.Contains(inclusionDirective) && l.Contains(t.Item1))
                                {
                                    this.vulns.Add(this.template.GetVulnerability(this.fileLoader.Filename, "Local File Inclusion", l + " <-> " + t.Item2));

                                }
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
