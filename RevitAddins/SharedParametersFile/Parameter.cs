using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddins.SharedParametersFile
{
    internal class Parameter
    {
        public string Guid { get; set; }
        public string Name { get; set; }
        public string DataType { get; set; }
        public string DataCategory { get; set; }
        public string Group { get; set; }
        public string Visible { get; set; }
        public string Description { get; set; }
        public string UserModifiable { get; set; }
        public string HideWhenNoValue { get; set; }

        public Parameter()
        {
        }
    }
}
