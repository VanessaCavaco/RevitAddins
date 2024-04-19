using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAddins.SharedParametersFile
{
    internal class SParamFile
    {
        public List<Group> Groups { get; set; }
        public List<Parameter> Parameters { get; set; }

        public override string ToString()
        {
            return $"Groups: {string.Join(", ", Groups.Select(g => $"Group Name:{g.Name}"))}\n" +
                   $"Parameters: {string.Join(", ", Parameters.Select(p => $"Guid:{p.Guid}; Name: {p.Name}; Group: {p.Group}; Visible: {p.Visible}; Data Type: {p.DataType}; Data Category:{p.DataCategory}; Description: {p.Description}, HideWhenNoValue; {p.HideWhenNoValue} UserModifiable: {p.UserModifiable}"))}";
        }
    
        public SParamFile(DefinitionFile data)
        {
            Groups = new List<Group>();
            Parameters = new List<Parameter>();

            ParseSharedParameterFile(data);
        }
        private void ParseSharedParameterFile(DefinitionFile SParamFile)
        {
            foreach (DefinitionGroup myGroup in SParamFile.Groups)
            {
                Groups.Add(new Group
                {
                    Name = myGroup.Name
                });

                // iterate the difinitions
                foreach (Definition definition in myGroup.Definitions)
                {
                    Parameter p = new Parameter();

                    ExternalDefinition param = definition as ExternalDefinition;
                    p.Guid = param.GUID.ToString();
                    p.Name = definition.Name;
                    p.Group = param.OwnerGroup.Name;
                    p.Visible = param.Visible.ToString();
                    p.Description = param.Description.ToString();
                    p.UserModifiable = param.UserModifiable.ToString();
                    p.HideWhenNoValue = param.HideWhenNoValue.ToString();

                    ForgeTypeId paramForgeTypeId = (ForgeTypeId)param.GetDataType();
                    p.DataType = paramForgeTypeId.TypeId;
                    bool isCategory = Category.IsBuiltInCategory(paramForgeTypeId);
                    if (isCategory)
                    {
                        p.DataCategory = Category.GetBuiltInCategory(paramForgeTypeId).ToString();
                    }
                    else 
                    { 
                        p.DataCategory = ""; 
                    }
                    Parameters.Add(p);
                }
            }
        }
    }

}
