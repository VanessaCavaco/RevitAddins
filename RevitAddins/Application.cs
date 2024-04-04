#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Windows.Media.Imaging;

#endregion

namespace RevitAddins
{
    internal class Application : IExternalApplication
    {
        string assemblyName = System.Reflection.Assembly.GetExecutingAssembly().Location;
        public Result OnStartup(UIControlledApplication a)
        {
            RibbonPanel panel = a.CreateRibbonPanel("Primera Applicacion");
            PushButtonData buttonDataSaludo = new PushButtonData("bt1","Saludo inicio", assemblyName, "RevitAddins.Saludo");
            PushButton botonSaludo = panel.AddItem(buttonDataSaludo) as PushButton ;
            botonSaludo.ToolTip = "Saludo de inicio";
            botonSaludo.LongDescription = "Saludo de bien venida a este Plugin.";
            botonSaludo.LargeImage = new BitmapImage(new Uri(@"D:\GitHubRepos\RevitAddins\RevitAddins\Assets\Params_dark32.png"));
            
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }
    }
}
