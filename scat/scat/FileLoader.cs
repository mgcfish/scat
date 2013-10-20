using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace scat
{
    public class FileLoader
    {
        public string Filename
        {
            get;
            private set;
        }

        public string Raw
        {
            get;
            set;
        }

        public string[] Lines
        {
            get;
            set;
        }

        public SyntaxAnalyzer SyntaxAnalyzer
        {
            get;
            set;
        }

        public FileLoader(string filename)
        {
            this.Filename = filename;
            this.Raw = string.Empty;
            this.Lines = null;
            this.SyntaxAnalyzer = new SyntaxAnalyzer(this.Filename);
        }

        public void Load()
        {
            this.Raw = File.ReadAllText(this.Filename);
            this.Lines = File.ReadAllLines(this.Filename);
            this.SyntaxAnalyzer.Load();

        }
    }
}
