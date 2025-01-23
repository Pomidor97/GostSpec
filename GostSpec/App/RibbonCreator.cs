
namespace GostSpec.App
{

using System.IO;
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
                // Игнорируем ошибку, если вкладка уже существует
            }

            var panel = application.GetRibbonPanels(tabName)
                            .FirstOrDefault(p => p.Name == "Оформление") 
                        ?? application.CreateRibbonPanel(tabName, "Оформление");

            string assemblyPath = Assembly.GetExecutingAssembly().Location;

            // Добавляем кнопки с иконками, загружаемыми из ресурсов
            AddPushButton(panel, "copyParameterValues_btn", "Копирование\nзначений", assemblyPath, "GostSpec.Commands.CopyParameterValuesCommand", "GostSpec.Resources.icons8_copy_32.png", "Копирование значений параметров");
            AddPushButton(panel, "autoNumbering_btn", "Автонумерация", assemblyPath, "GostSpec.Commands.AutoNumberingCommand", "GostSpec.Resources.icons8_counter_32.png", "Автонумерация позиций");
            AddPushButton(panel, "autoScheduling_btn", "Авто\nспецификация", assemblyPath, "GostSpec.Commands.AutoSchedulingCommand", "GostSpec.Resources.icons8_schedule_32.png", "Авто-генерация спецификаций по системам");
        }

        private static void AddPushButton(RibbonPanel panel, string name, string text, string assemblyPath, string className, string resourceName, string tooltip)
        {
            // Создаем данные для кнопки
            var buttonData = new PushButtonData(name, text, assemblyPath, className)
            {
                ToolTip = tooltip
            };

            // Загружаем ресурс из DLL
            var assembly = Assembly.GetExecutingAssembly();
            using (Stream stream = assembly.GetManifestResourceStream(resourceName))
            {
                if (stream != null)
                {
                    var image = new BitmapImage();
                    image.BeginInit();
                    image.StreamSource = stream;
                    image.CacheOption = BitmapCacheOption.OnLoad;
                    image.EndInit();
                    buttonData.LargeImage = image;
                }
                else
                {
                    TaskDialog.Show("Ошибка", $"Ресурс {resourceName} не найден в сборке.");
                }
            }

            panel.AddItem(buttonData);
        }
    }
}

}