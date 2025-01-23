using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace GostSpec.Utils
{
    public static class ScheduleUtils
    {
        public static void ConfigureSchedule(ViewSchedule schedule, Dictionary<string, object> config)
        {
            if (schedule == null || config == null) return;
            
            if (config.TryGetValue("Name", out object name) && name is string newName)
            {
                schedule.Name = newName;
            }
            
            if (config.TryGetValue("Filters", out object filters) && filters is List<ScheduleFilter> scheduleFilters)
            {
                var definition = schedule.Definition;
                definition.ClearFilters();

                foreach (var filter in scheduleFilters)
                {
                    definition.AddFilter(filter);
                }
            }
            
        }
    }
}