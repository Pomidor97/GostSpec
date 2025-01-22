using Autodesk.Revit.DB;
using GostSpec.Interfaces;

namespace GostSpec.Services
{
    public class ScheduleGenerator : IScheduleGenerator
    {
        public ViewSchedule CreateScheduleForSystem(Document doc, string systemName)
        {
            FilteredElementCollector collector = new FilteredElementCollector(doc)
                .OfClass(typeof(ViewSchedule));

            ViewSchedule templateSchedule = null;
            foreach (ViewSchedule vs in collector)
            {
                if (vs.Name == "# Спецификация для оформления")
                {
                    templateSchedule = vs;
                    break;
                }
            }

            if (templateSchedule == null)
                return null;

            ViewSchedule newSchedule = doc.GetElement(templateSchedule.Duplicate(ViewDuplicateOption.Duplicate)) as ViewSchedule;
            if (newSchedule != null)
            {
                newSchedule.Name = "О_Спецификация_" + systemName;
            }

            return newSchedule;
        }

        public void ConfigureScheduleFilters(ViewSchedule schedule, string systemName)
        {
            var definition = schedule.Definition;
            var filters = definition.GetFilters();
            if (filters.Count > 1)
            {
                var oldFilter = filters[1];
                var newFilter = new ScheduleFilter(
                    oldFilter.FieldId,
                    oldFilter.FilterType,
                    systemName
                );
                definition.RemoveFilter(1);
                definition.AddFilter(newFilter);
            }
        }
    }
}