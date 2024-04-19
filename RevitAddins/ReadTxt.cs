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
                DefinitionFile file =  app.OpenSharedParameterFile();

                SParamFile sharedParameters = new SParamFile(file);

                TaskDialog.Show("Shared Parameters", sharedParameters.ToString());

                return Result.Succeeded;
            }
            catch (Exception e)
            {
                TaskDialog.Show("Error", e.ToString());
                return Result.Failed;
            }

        }

    }
}
