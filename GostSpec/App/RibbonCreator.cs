
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
            catch
            {
                // Игнорируем исключение, если вкладка уже существует.
            }

            var panel = application.GetRibbonPanels(tabName)
                            .FirstOrDefault(p => p.Name == "Оформление") 
                        ?? application.CreateRibbonPanel(tabName, "Оформление");

            string assemblyPath = Assembly.GetExecutingAssembly().Location;

            // Добавление кнопок.
            AddPushButton(panel, "copyParameterValues_btn", "Копирование\nзначений", assemblyPath, "icons8_copy_32.png", "Копирование значений параметров");
            AddPushButton(panel, "autoNumbering_btn", "Автонумерация", assemblyPath, "icons8_counter_32.png", "Автонумерация позиций");
            AddPushButton(panel, "autoScheduling_btn", "Авто\nспецификация", assemblyPath, "icons8_schedule_32.png", "Авто-генерация спецификаций по системам");
        }

        private static void AddPushButton(RibbonPanel panel, string name, string text, string assemblyPath, string className, string tooltip)
        {
            var buttonData = new PushButtonData(name, text, assemblyPath, className)
            {
                ToolTip = tooltip
            };
            panel.AddItem(buttonData);
        }
    }
}