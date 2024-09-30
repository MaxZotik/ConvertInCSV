using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConvertInCSV.Class
{
    internal class ObjectCSVmode : ObjectCSV
    {
        public string ModeWork {  get; set; }

        public ObjectCSVmode(string time, string value, string modeWork) : base(time, value)
        {
            ModeWork = modeWork;
        }

        public override string ToString()
        {
            return $@"{Time};{Value};{ModeWork}";
        }
    }
}
