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
using System.Text;

#endregion

namespace RevitAddins
{
    [Transaction(TransactionMode.Manual)]
    public class WriteTxt : IExternalCommand
    {
       public Result Execute(
         ExternalCommandData commandData,
         ref string message,
         ElementSet elements)
       {
            try
            {
                //creates a sample of StreamWriter with using:
                string txtPath = @"D:\GitHubRepos\RevitAddins\RevitAddins\Assets\mvp-fullstack-sample.txt";
                using (StreamWriter writer = new StreamWriter(txtPath, true, Encoding.Unicode))// if false, overwrite all the lines
                {
                    writer.Write("\n nueva linea de parametros");
                    writer.Write("\n otra linea de parametros");
                    writer.Close();
                }
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
