using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using GostSpec.Interfaces;
using System.Linq;
using System.Collections.Generic;

namespace GostSpec.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class AutoSchedulingCommand : IExternalCommand
    {
        private readonly IScheduleGenerator _scheduleGenerator;

        public AutoSchedulingCommand(IScheduleGenerator scheduleGenerator)
        {
            
        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elementSet)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            var elements = new FilteredElementCollector(doc)
                .WhereElementIsNotElementType()
                .Where(e => e.LookupParameter("С_Система") != null)
                .ToList();

            var systemNames = new HashSet<string>();
            foreach (var el in elements)
            {
                string sys = el.LookupParameter("С_Система").AsString();
                if (!string.IsNullOrEmpty(sys))
                    systemNames.Add(sys.Trim());
            }

            using (Transaction tx = new Transaction(doc, "Auto Schedules"))
            {
                tx.Start();

                foreach (var systemName in systemNames)
                {
                    ViewSchedule schedule = _scheduleGenerator.CreateScheduleForSystem(doc, systemName);
                    if (schedule != null)
                    {
                        _scheduleGenerator.ConfigureScheduleFilters(schedule, systemName);
                    }
                }

                tx.Commit();
            }

            return Result.Succeeded;
        }
    }
}