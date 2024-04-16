#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using RevitAddins.SharedParametersFile;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

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
                var uiapp = commandData.Application;
                var uidoc = uiapp.ActiveUIDocument;
                var app = uiapp.Application;
                var doc = uidoc.Document;
                string sharedParameterFileName = "mvp-fullstack-sample.txt";

                // Call method to set and open the external shared parameter file
                // set the path of shared parameter file to current Revit
                app.SharedParametersFilename = sharedParameterFileName;
                // open the file
               DefinitionFile file=  app.OpenSharedParameterFile();

                StringBuilder fileInformation = new StringBuilder(500);

                // get the file name 
                fileInformation.AppendLine("File Name: " + file.Filename);

                // iterate the Definition groups of this file
                foreach (DefinitionGroup myGroup in file.Groups)
                {
                    // get the group name
                    fileInformation.AppendLine("Group Name: " + myGroup.Name);

                    // iterate the difinitions
                    foreach (Definition definition in myGroup.Definitions)
                    {
                        // Parameter as a definition properties
                        fileInformation.AppendLine("Definition Name: " + definition.Name 
                            + "\n" + "Definition Parameter Group id " + definition.GetGroupTypeId()
                            + "\n" + "Definition Parameter DataType " + definition.GetDataType()
                            + "\n" + "Definition Parameter Type " + definition.GetType().Name
                            + "\n" + "Definition Parameter depricated Prameter Group " + definition
                            + "\n" + "Definition Parameter Hash Code " + definition.GetHashCode()
                            );
                        // Parameter as a definition as External Definition.
                        var param = definition as ExternalDefinition;
                        fileInformation.AppendLine("\n"+"ExtDefinition Name: " + param.Name
                           + "\n" + "ExtDefinition Parameter  GUID " + param.GUID
                           + "\n" + "ExtDefinition Parameter DataType " + param.GetDataType()
                           + "\n" + "ExtDefinition Parameter Type " + param.GetType().Name
                           + "\n" + "ExtDefinition Parameter Group id " + param.GetGroupTypeId()
                           + "\n" + "ExtDefinition Parameter Owner Group " + param.OwnerGroup
                           + "\n" + "ExtDefinition Parameter Hide when no value " + param.HideWhenNoValue
                           + "\n" + "ExtDefinition Parameter Description" + param.Description
                           + "\n" + "ExtDefinition Parameter Visible" + param.Visible
                           + "\n" + "ExtDefinition Parameter user modifiable" + param.UserModifiable
                           + "\n" + "ExtDefinition Parameter hash Code" + param.GetHashCode()
                           );
                    }
                }
                TaskDialog.Show("Revit", fileInformation.ToString());

                // Display the contents of the shared parameter file
                TaskDialog.Show("Shared Parameters", file.Filename);

                return Result.Succeeded;
            }
            catch (Exception e)
            {
                TaskDialog.Show("Error", e.ToString());
                return Result.Failed;
            }
            //try
            //{
            //    //creates a sample of StreamReader with using:
            //    string txtPath = @"D:\GitHubRepos\RevitAddins\RevitAddins\Assets\mvp-fullstack-sample.txt";
            //    using (StreamReader reader = new StreamReader(txtPath))
            //    {
            //        //txt to string
            //        string txtFileText = reader.ReadToEnd();

            //        var sharedParameters = new SharedParametersFile.SharedParameterFile(txtFileText);
            //        //string parameters = "";
            //        //for (int i = 0; i> sharedParameters.Parameters.Count; i++) { 
            //        //    parameters += sharedParameters.Parameters[i].Name;
            //        //    parameters += "\n";
            //        //}
            //        //TaskDialog.Show("Only Parameters Parameters", parameters);

            //        TaskDialog.Show("Shared Parameters",sharedParameters.ToString());

            //        TaskDialog.Show("Txt file", txtFileText);
            //    }
            //    return Result.Succeeded;
            //}
            //catch (Exception e)
            //{
            //    TaskDialog.Show("Error", e.ToString());
            //    return Result.Failed;
            //}



        }

    }
}
