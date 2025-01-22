using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using GostSpec.Utils;

namespace GostSpec.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class AutoNumberingCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            if (doc.ActiveView is ViewSchedule schedule)
            {
                int index = 1;

                using (Transaction tx = new Transaction(doc, "Auto Numbering"))
                {
                    tx.Start();

                    foreach (var element in new FilteredElementCollector(doc, schedule.Id))
                    {
                        Parameter param = element.LookupParameter("Номер");
                        if (param != null && !param.IsReadOnly)
                        {
                            param.Set(index.ToString());
                            index++;
                        }
                    }

                    tx.Commit();
                }
            }
            else
            {
                TaskDialogUtils.ShowWarning("Нужно открыть спецификацию для автонумерации.");
            }

            return Result.Succeeded;
        }
    }
}