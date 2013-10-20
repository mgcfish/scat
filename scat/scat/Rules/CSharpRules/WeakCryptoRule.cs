using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scat
{
    public class WeakCryptoRule : IRule
    {
        public List<FileLoader> Loaders
        {
            get;
            set;
        }

        private ITemplate template;

        public WeakCryptoRule(List<FileLoader> loaders, ITemplate template)
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

        public class WeakCryptoAnalyzer
        {
            public List<BaseVulnerability> vulns;
            public FileLoader fileLoader;
            private ITemplate template;

            public WeakCryptoAnalyzer(FileLoader l, ITemplate template)
            {
                this.fileLoader = l;
                this.vulns = new List<BaseVulnerability>();
                this.template = template;
            }

            public void Analyze()
            {
                string raw = this.fileLoader.Raw;

                string[] hashNames = { "SHA1", "SHA256", "SHA384", "SHA512", "MD5" };

                if (raw.Contains("System.Security.Cryptography"))
                {
                    foreach (var hashName in hashNames)
                    {
                        if (raw.ToLower().Contains(hashName.ToLower()))
                        {
                            if (raw.Contains("ComputeHash("))
                            {
                                if (!raw.ToLower().Contains("salt"))
                                {
                                    string message = "ComputeHash called with : " + hashName;
                                    this.vulns.Add(new  HashWithoutSaltVulnerability(this.fileLoader.Filename, "Use of a hash without a salt", message, string.Empty));
                                }
                            }
                        }
                    }

                    if (raw.Contains("ComputeHash("))
                    {
                        if (raw.Contains("MD5"))
                        {
                            this.vulns.Add(new HashWithoutSaltVulnerability(this.fileLoader.Filename, "Use of a weak cryptographic algorithm : MD5", string.Empty, string.Empty));
                        }
                    }


                }


            }

        }

        public void Evaluate()
        {
            List<WeakCryptoAnalyzer> analyzers = new List<WeakCryptoAnalyzer>();

            foreach (var l in this.Loaders)
            {
                analyzers.Add(new WeakCryptoAnalyzer(l, this.template));
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
