#region Namespaces
using System;
using System.Collections.Generic;
using System.Reflection;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System.Windows.Media.Imaging;
#endregion

namespace RevitAddinButton
{
    class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication a)
        {
            AddRibbonPanel(a);
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication a)
        {
            return Result.Succeeded;
        }


        public static void AddRibbonPanel(UIControlledApplication application)
        {
            // Create a custom ribbon tab
            String tabName = "����������";
            application.CreateRibbonTab(tabName);

            // Add a new ribbon panel
            RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, "�����������");

            // Get dll assembly path
            string thisAssemblyPath = Assembly.GetExecutingAssembly().Location;

            // create push button for CurveTotalLength
            PushButtonData b1Data = new PushButtonData(
                "cmdCalculateCoordinates",
                "�����������" + System.Environment.NewLine + "  ���������  ",
                thisAssemblyPath,
                "RevitAddinButton.Command");

            PushButton pb1 = ribbonPanel.AddItem(b1Data) as PushButton;
            pb1.ToolTip = "�������� ������ ��� ����������� ��� ���������";
            BitmapImage pb1Image = new BitmapImage(new Uri("pack://application:,,,/RevitAddinButton;component/Resources/Frame.png"));
            pb1.LargeImage = pb1Image;
        }
    }
}
