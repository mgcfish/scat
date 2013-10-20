using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace scat
{
    public class Node // this is a method.
    {
        public string ClassName
        {
            get;
            set;
        }

        public string NodeName
        {
            get;
            set;
        }

        public string NodeCode
        {
            get;
            set;
        }

        public List<Variable> VariablesInScope
        {
            get;
            set;
        }

        public List<Node> AdjacentNodes
        {
            get;
            set;
        }

        public List<Parameter> Parameters
        {
            get;
            set;
        }

        public List<Invocation> Invocations
        {
            get;
            set;
        }

        public string Filename
        {
            get;
            set;
        }

        public Node(string filename, string classname, string name, string code)
        {
            this.Filename = filename;
            this.ClassName = classname;
            this.NodeName = name;
            this.NodeCode = code;
            this.VariablesInScope = new List<Variable>();
            this.AdjacentNodes = new List<Node>();
            this.Parameters = new List<Parameter>();
            this.Invocations = new List<Invocation>();

        }

        public bool ParameterListContainsAString()
        {
            bool retval = false;

            foreach (var p in this.Parameters)
            {
                if (p.ParameterCode.ToLower().Contains("string"))
                {
                    retval = true;
                    break;
                }
            }

            return retval;
        }
    }
}
