
using System;
using System.Linq;
using System.Reflection;
using System.Windows.Media.Imaging;
using Autodesk.Revit.UI;

namespace GostSpec.App
{
    public static class RibbonCreator
    {
        public static void CreateRibbonPanel(UIControlledApplication application)
        {
            string tabName = "KAZGOR";
            try
            {
                application.CreateRibbonTab(tabName);
            }
            catch (Exception)
            {
            }

            var panel = application.GetRibbonPanels(tabName)
                .FirstOrDefault(p => p.Name == "Оформление");

            if (panel == null)
                panel = application.CreateRibbonPanel(tabName, "Оформление");

            string assemblyPath = Assembly.GetExecutingAssembly().Location;

            PushButtonData copyParamsButton = new PushButtonData(
                "copyParameterValues_btn",
                "Копирование\nзначений",
                assemblyPath,
                "icons8_copy_32.png"
            );

            PushButtonData autoNumButton = new PushButtonData(
                "autoNumbering_btn",
                "Автонумерация",
                assemblyPath,
                "icons8_counter_32.png"
            );

            PushButtonData autoSchedButton = new PushButtonData(
                "autoScheduling_btn",
                "Авто\nспецификация",
                assemblyPath,
                "icons8_schedule_32.png"
            );

            var copyBtn = panel.AddItem(copyParamsButton) as PushButton;
            var numBtn = panel.AddItem(autoNumButton) as PushButton;
            var schedBtn = panel.AddItem(autoSchedButton) as PushButton;

            copyBtn.ToolTip = "Копирование значений параметров";
            numBtn.ToolTip = "Автонумерация позиций";
            schedBtn.ToolTip = "Авто-генерация спецификаций по системам";

        }
    }
}