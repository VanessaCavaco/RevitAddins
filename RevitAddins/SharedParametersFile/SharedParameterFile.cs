using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddins.SharedParametersFile
{
    internal class SharedParameterFile
    {
        public List<Meta> Metas { get; set; }
        public List<Group> Groups { get; set; }
        public List<Parameter> Parameters { get; set; }

        public override string ToString()
        {
            return $"Meta: {string.Join(", ", Metas.Select(m => $"{m.Version}: {m.MiniVersion}"))}\n" +
                   $"Groups: {string.Join(", ", Groups.Select(g => $"{g.Id}: {g.Name}"))}\n" +
                   $"Parameters: {string.Join(", ", Parameters.Select(p => $"{p.Guid}: {p.Name}"))}";
        }
    
        public SharedParameterFile(string data)
        {
            Metas = new List<Meta>();
            Groups = new List<Group>();
            Parameters = new List<Parameter>();

            ParseSharedParameterFile(data);
        }
    private void ParseSharedParameterFile(string data)
    {
        var lines = data.Split('\n');
            TaskDialog.Show("data", data);

            foreach (var line in lines)
        {
            TaskDialog.Show("Lines of txt", line);
            var parts = line.Trim().Split((char[])null, StringSplitOptions.RemoveEmptyEntries);
            var section = parts[0];

            switch (section)
            {
                case "#":
                    // Ignore comments
                    break;
                case "*META":
                    // Ignore comments
                    break;
                case "*GROUP":
                    // Ignore comments
                    break;
                case "*PARAM":
                // Ignore comments
                case "META":
                    Metas.Add(new Meta()
                    {
                        Version = parts[1],
                        MiniVersion = parts[2],
                    });
                    break;
                case "GROUP":
                    Groups.Add(new Group 
                    { 
                        Id = parts[1], 
                        Name = parts[2] 
                    });
                    break;
                case "PARAM":
                    Parameters.Add(new Parameter
                    {
                        Guid = parts[1],
                        Name = parts[2],
                        DataType = parts[3],
                        DataCategory = parts[4],
                        Group = parts[5],
                        Visible = parts[6],
                        Description = parts[7],
                        UserModifiable = parts[8],
                        HideWhenNoValue = parts[9]
                    });
                    break;
                default:
                    //TaskDialog.Show("Unknown section", "Unknown section, skip");
                    // Unknown section, skip
                    break;
            }
        }

    }
    }

}
