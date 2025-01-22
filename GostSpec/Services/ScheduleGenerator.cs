using System.Linq;
using Autodesk.Revit.DB;
using GostSpec.Interfaces;
using GostSpec.Utils;

namespace GostSpec.Services
{
    public class ScheduleGenerator : IScheduleGenerator
    {
        public ViewSchedule CreateScheduleForSystem(Document doc, string systemName)
        {
            var collector = new FilteredElementCollector(doc).OfClass(typeof(ViewSchedule));
            var templateSchedule = collector
                .Cast<ViewSchedule>()
                .FirstOrDefault(vs => vs.Name == "# Спецификация для оформления");

            if (templateSchedule == null)
            {
                TaskDialogUtils.ShowWarning("Шаблон спецификации не найден.");
                return null;
            }

            var newSchedule = doc.GetElement(templateSchedule.Duplicate(ViewDuplicateOption.Duplicate)) as ViewSchedule;
            if (newSchedule != null)
            {
                newSchedule.Name = $"О_Спецификация_{systemName}";
            }

            return newSchedule;
        }

        public void ConfigureScheduleFilters(ViewSchedule schedule, string systemName)
        {
            if (schedule == null) return;

            var definition = schedule.Definition;
            var filters = definition.GetFilters();

            if (filters.Count > 1)
            {
                var oldFilter = filters[1];
                var newFilter = new ScheduleFilter(oldFilter.FieldId, oldFilter.FilterType, systemName);
                definition.RemoveFilter(1);
                definition.AddFilter(newFilter);
            }
        }
    }
}