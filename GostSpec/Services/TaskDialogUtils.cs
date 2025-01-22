using Autodesk.Revit.UI;

namespace GostSpec.Utils
{
    public static class TaskDialogUtils
    {
        public static void ShowWarning(string message)
        {
            TaskDialog.Show("Предупреждение", message);
        }

        public static void ShowInfo(string message)
        {
            TaskDialog.Show("Информация", message);
        }
    }
}