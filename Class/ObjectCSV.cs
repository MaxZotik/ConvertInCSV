using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertInCSV.Class
{
    internal class ObjectCSV
    {
        public string Time {  get; set; }

        public string Value { get; set; }

        public ObjectCSV(string time, string value) 
        {
            Time = time;
            Value = value;
        }

        public override string ToString()
        {
            return $@"{Time};{Value}";
        }
    }
}
