using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace scat
{
    public class Variable 
    {
        public string VariableName
        {
            get;
            set;
        }

        public string VariableType
        {
            get;
            set;
        }

        public string VariableCode
        {
            get;
            set;
        }

        public bool IsTainted
        {
            get;
            set;
        }

        public Variable(string name, string code)
        {
            this.VariableType = string.Empty;
            this.VariableName = name;
            this.VariableCode = code;
            if (this.VariableCode.Contains("Request.") || this.VariableCode.Contains("Request[") || this.VariableCode.Contains(".Text"))
            {
                if (!this.VariableCode.Contains("Encode") && !this.VariableCode.Contains("Parse"))
                {
                    this.IsTainted = true;
                }
            }
            else
            {
                this.IsTainted = false;
            }
        }

        public Variable(string name, string code, string variableType)
        {
            this.VariableType = variableType;
            this.VariableName = name;
            this.VariableCode = code;
            if (this.VariableCode.Contains("Request.") || this.VariableCode.Contains("Request[") || this.VariableCode.Contains(".Text"))
            {
                if (!this.VariableCode.Contains("Encode") && !this.VariableCode.Contains("Parse"))
                {
                    this.IsTainted = true;
                }
            }
            else
            {
                this.IsTainted = false;
            }
        }

   

    }

}
