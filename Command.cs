#region Namespaces
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.UI.Selection;
#endregion

namespace RevitAddinButton
{
    [RegenerationAttribute(RegenerationOption.Manual)]
    [Transaction(TransactionMode.Manual)]
    
    public class Command : IExternalCommand
    {
        public Result Execute(
            ExternalCommandData commandData,
            ref string message,
            ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            Document doc = uiApp.ActiveUIDocument.Document;

            try
            {
                Reference pickedRef = null;
                Selection sel = uiApp.ActiveUIDocument.Selection;

                // Pick an element

                pickedRef = sel.PickObject(ObjectType.Element,
                    "Выберите элемент");
                Element elem = doc.GetElement(pickedRef);

                //Get the element center
                XYZ origin = GetElementCenter(elem);
                TaskDialog.Show("Отображение координат",
                    $"X: {Math.Round(origin.X, 2)} | Y: {Math.Round(origin.Y, 2)} | Z: {Math.Round(origin.Z, 2)}");
            }
            // If user right-clicks or presess Esc button,
            // handle the exception
            catch (Autodesk.Revit.Exceptions.OperationCanceledException)
            {
                return Result.Cancelled;
            }
            // Catch other errors
            catch (Exception ex)
            {

                message = ex.StackTrace;
                return Result.Failed;
            }

            return Result.Succeeded;
        }


        public XYZ GetElementCenter(Element elem)
        {
            BoundingBoxXYZ bounding = elem.get_BoundingBox(null);
            XYZ center = (bounding.Max + bounding.Min) * 0.5;
            return center;
        }

        //Get all point of element
        //public void TextArrayPointsOfWall(Element wall)
        //{
        //    string s = null;
        //    LocationCurve lc = wall.Location as LocationCurve;
        //    XYZ EndP1 = lc.Curve.GetEndPoint(0);
        //    XYZ EndP2 = lc.Curve.GetEndPoint(1);
        //    double h = wall.LookupParameter("Unconnected Height").AsDouble();
        //    XYZ EndP1above = new XYZ(EndP1.X, EndP1.Y, EndP1.Z + h);
        //    XYZ EndP2above = new XYZ(EndP2.X, EndP2.Y, EndP2.Z + h);
        //    s = EndP1.ToString() + "\n" + EndP2.ToString() + "\n" + EndP1above.ToString() + "\n" + EndP2above.ToString();
        //    TaskDialog.Show("Отображение координат", s);
        //}
    }
}
