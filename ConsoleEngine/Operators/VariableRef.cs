using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleEngine.Operators
{
    internal class VariableRef : OperatorValue
    {

        public Variables VariableObj;

        public override int Value { 
            get 
            {
                return VariableObj.Value;
            } 
            set 
            {
                VariableObj.Value = value;
            } 
        }
    }
}
