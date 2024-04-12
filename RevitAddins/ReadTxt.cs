#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

#endregion

namespace RevitAddins
{
    [Transaction(TransactionMode.Manual)]
    public class ReadTxt : IExternalCommand
    {
       public Result Execute(
         ExternalCommandData commandData,
         ref string message,
         ElementSet elements)
       {
            try
            {
                //creates a sample of StreamReader with using:
                string txtPath = @"D:\GitHubRepos\RevitAddins\RevitAddins\Assets\mvp-fullstack-sample.txt";
                using (StreamReader reader = new StreamReader(txtPath))
                {
                    
                    //txt to string
                    string txtFileText = reader.ReadToEnd();

                    var sharedParameters = ParseSharedParameterFile(txtFileText);
                    string parameters = "";
                    for (int i = 0; i> sharedParameters.Parameters.Count; i++) { 
                        parameters += sharedParameters.Parameters[i].Name;
                        parameters += "\n";
                    }
                    TaskDialog.Show("Only Parameters Parameters", parameters);

                    TaskDialog.Show("Shared Parameters",sharedParameters.ToString());

                    TaskDialog.Show("Txt file", txtFileText);
                }
                return Result.Succeeded;
            }
            catch (Exception e)
            {
                TaskDialog.Show("Error", e.ToString());
                return Result.Failed;
            }
        }
        static SharedParameterData ParseSharedParameterFile(string data)
        {
            var sharedParameters = new SharedParameterData
            {
                Meta = new Dictionary<string, string>(),
                Groups = new List<Group>(),
                Parameters = new List<Parameter>()
            };

            var lines = data.Split('\n');

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
                        sharedParameters.Meta[parts[1]] = parts[2];
                        break;
                    case "*GROUP":
                        sharedParameters.Groups.Add(new Group { Id = parts[1], Name = parts[2] });
                        break;
                    case "*PARAM":
                        sharedParameters.Parameters.Add(new Parameter
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

            return sharedParameters;
        }
    }
}
