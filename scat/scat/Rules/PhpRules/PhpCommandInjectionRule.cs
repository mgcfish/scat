using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace scat
{
    public class PhpCommandInjectionRule : IRule
    {
        public List<FileLoader> Loaders
        {
            get;
            set;
        }


        private ITemplate template;

        public PhpCommandInjectionRule(List<FileLoader> loaders, ITemplate template)
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

        public class PhpCommandInjectionAnalyzer
        {
            public List<BaseVulnerability> vulns;
            public FileLoader fileLoader;
            private ITemplate template;

            public PhpCommandInjectionAnalyzer(FileLoader l, ITemplate template)
            {
                this.fileLoader = l;
                this.vulns = new List<BaseVulnerability>();
                this.template = template;
            }

            public void Analyze()
            {
                string[] phpCommandInjectionFunctions = { 
                                                        "shell_exec(", 
                                                        "`",
                                                        "exec(",
                                                        "system(",
                                                        "passthru(",
                                                        "popen(",
                                                        "proc_open(",
                                                        "syscall(",
                                                        "eval("
                                                        };

                if (this.fileLoader.Filename.EndsWith(".php"))
                {
                    //    this.vulns.Add(this.template.GetVulnerability(this.fileLoader.Filename, "Php Sql Injection", line));

                    foreach (var phpCommandInjectionFunction in phpCommandInjectionFunctions)
                    {
                        foreach (var line in this.fileLoader.Lines)
                        {
                            if (line.Contains(phpCommandInjectionFunction) && PhpUtil.ContainsUserInput(line))
                            {
                                this.vulns.Add(this.template.GetVulnerability(this.fileLoader.Filename, "Php Command Injection", line));
                            }
                        }
                    }

                    //
                    // get all tainted variables
                    //

                    List<Tuple<string, string>> taintedLove = PhpUtil.EnumerateTaintedVariables(this.fileLoader.Lines);

                    foreach (var line in this.fileLoader.Lines)
                    {
                        foreach (var tl in taintedLove)
                        {
                            if (line.Contains(tl.Item1))
                            {
                                foreach (var phpCommandInjectionFunction in phpCommandInjectionFunctions)
                                {
                                    if (line.Contains(phpCommandInjectionFunction))
                                    {
                                        this.vulns.Add(this.template.GetVulnerability(this.fileLoader.Filename, "Php Command Injection", line + " <--> " + tl.Item2));
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
            List<PhpCommandInjectionAnalyzer> analyzers = new List<PhpCommandInjectionAnalyzer>();

            foreach (var l in this.Loaders)
            {
                analyzers.Add(new PhpCommandInjectionAnalyzer(l, this.template));
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
