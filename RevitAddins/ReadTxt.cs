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
                    string txtFileText = reader.ReadToEnd();
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
    }
}
