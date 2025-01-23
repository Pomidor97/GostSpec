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

        public AutoSchedulingCommand()
        {
            _scheduleGenerator = new DefaultScheduleGenerator();
        }

        public AutoSchedulingCommand(IScheduleGenerator scheduleGenerator)
        {
            _scheduleGenerator = scheduleGenerator;
        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            var elementsWithSystems = new FilteredElementCollector(doc)
                .WhereElementIsNotElementType()
                .Where(e => e.LookupParameter("С_Система") != null)
                .ToList();

            var systemNames = elementsWithSystems
                .Select(e => e.LookupParameter("С_Система").AsString())
                .Where(name => !string.IsNullOrEmpty(name))
                .Distinct();

            using (Transaction tx = new Transaction(doc, "Auto Scheduling"))
            {
                tx.Start();

                foreach (var systemName in systemNames)
                {
                    var schedule = _scheduleGenerator.CreateScheduleForSystem(doc, systemName);
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