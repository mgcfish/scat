using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

namespace scat
{
    public class StaticCodeAnalyzer
    {
        Action<StaticCodeAnalyzer> completed;

        public StaticCodeAnalyzer(Action<StaticCodeAnalyzer> completed)
        {
            this.completed = completed;
            this.Vulnerabilities = new List<BaseVulnerability>();
            this.FileLoaders = new List<FileLoader>();
        }

        public List<BaseVulnerability> Vulnerabilities
        {
            get;
            private set;
        }

        private IEnumerable<string> EnumerateFiles()
        {
 
            IEnumerable<string> csFiles = Directory.EnumerateFiles(Configuration.SourceFolder, "*.cs", SearchOption.AllDirectories);
            IEnumerable<string> aspxFiles = Directory.EnumerateFiles(Configuration.SourceFolder, "*.aspx", SearchOption.AllDirectories);
            IEnumerable<string> phpFiles = Directory.EnumerateFiles(Configuration.SourceFolder, "*.php", SearchOption.AllDirectories);
            IEnumerable<string> configFiles = Directory.EnumerateFiles(Configuration.SourceFolder, "*.config", SearchOption.AllDirectories);
            IEnumerable<string> sqlFiles = Directory.EnumerateFiles(Configuration.SourceFolder, "*.sql_", SearchOption.AllDirectories);
            IEnumerable<string> allFiles = csFiles.Union(aspxFiles).Union(sqlFiles).Union(configFiles).Union(phpFiles);

            return allFiles;
        }

        public List<FileLoader> FileLoaders
        {
            get;
            set;
        }

        public void OnThread()
        {
            //
            // Load each of the files.
            //
            IEnumerable<string> sourceFiles = this.EnumerateFiles();
            foreach (string sourceFile in sourceFiles)
            {
                this.FileLoaders.Add(new FileLoader(sourceFile));
            }

            Parallel.ForEach(this.FileLoaders, loader =>
            {
                loader.Load();
            });


            //
            // Build the graph so adjacent nodes makes sense.
            //

            GraphBuilder.BuildGraph(this.FileLoaders);

            //
            // save the file loaders for later, just in case.
            //

            Configuration.Loaders = this.FileLoaders;

            //
            // run the rules in parallel. 
            //

            List<IRule> rules = RulesBuilder.GetRules(this.FileLoaders);

            // rules.Add(new AdvancedCommandInjectionRule(this.FileLoaders, this.debug));
            Parallel.ForEach(rules, rule =>
            {
                rule.Evaluate();
            });

            //
            // aggregate the vulnerabilities  
            //
            foreach (var r in rules)
            {
                IEnumerable<BaseVulnerability> vulns = r.GetVulnerabilities();

                foreach (var v in vulns)
                {
                    Util.AddVulnerability(this.Vulnerabilities, v);
                    //this.Vulnerabilities.Add(v);
                }
            }

            this.completed(this);
        }
    }
}
