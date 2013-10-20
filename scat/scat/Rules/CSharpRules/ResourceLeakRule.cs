using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scat
{
    public class ResourceLeakRule : IRule
    {
        public List<FileLoader> Loaders
        {
            get;
            set;
        }

        private ITemplate template;

        public ResourceLeakRule(List<FileLoader> loaders, ITemplate template)
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

        public class ResourceLeakAnalyzer
        {
            public List<BaseVulnerability> vulns;
            public FileLoader fileLoader;
            private ITemplate template;

            public ResourceLeakAnalyzer(FileLoader l, ITemplate template)
            {
                this.fileLoader = l;
                this.vulns = new List<BaseVulnerability>();
                this.template = template;
            }

            public void Analyze()
            {
                string raw = this.fileLoader.Raw;

                string[] EvilMethods = { "MemoryStream", "StreamReader", "TextReader", "CryptoStream" };
                string[] Safe = { "using(", ".Dispose", ".Close" };

                // there are two cases
                //   1) there is a declared variable
                //   2) new MemoryStream(...).ReadToEnd();
                //

                foreach (var evil in EvilMethods)
                {
                    if (raw.Contains(evil))
                    {
                        bool mitigated = false;

                        foreach (var safe in Safe)
                        {
                            if (raw.Contains(safe))
                            {
                                mitigated = true;
                            }
                        }

                        if (!mitigated)
                        {
                            this.vulns.Add(new ResourceLeakVulnerability(this.fileLoader.Filename, "A resource leak was detected. When using <b>" + evil + "</b> the resource must be disposed.", string.Empty, string.Empty));
                        }


                    }
                }

            }

        }

        public void Evaluate()
        {
            List<ResourceLeakAnalyzer> analyzers = new List<ResourceLeakAnalyzer>();

            foreach (var l in this.Loaders)
            {
                analyzers.Add(new ResourceLeakAnalyzer(l, this.template));
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
