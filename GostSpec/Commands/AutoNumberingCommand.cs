using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;

namespace GostSpec.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class AutoNumberingCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            if (doc.ActiveView.ViewType == ViewType.Schedule)
            {
                var schedule = doc.ActiveView as ViewSchedule;
            }
            else
            {
                TaskDialog.Show("Предупреждение", "Нужно открыть спецификацию для автонумерации");
            }

            return Result.Succeeded;
        }
    }
}