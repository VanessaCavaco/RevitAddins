using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddins
{
    internal class SharedParameterData
    {
        public Dictionary<string, string> Meta { get; set; }
        public List<Group> Groups { get; set; }
        public List<Parameter> Parameters { get; set; }

        public override string ToString()
        {
            return $"Meta: {string.Join(", ", Meta.Select(kv => $"{kv.Key}: {kv.Value}"))}\n" +
                   $"Groups: {string.Join(", ", Groups.Select(g => $"{g.Id}: {g.Name}"))}\n" +
                   $"Parameters: {string.Join(", ", Parameters.Select(p => $"{p.Guid}: {p.Name}"))}";
        }
    }

    public class Group
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public class Parameter
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
    }
}
