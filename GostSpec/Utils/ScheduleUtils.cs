using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace GostSpec.Utils
{
    public static class ScheduleUtils
    {
        /// <summary>
        /// Настраивает спецификацию на основе переданной конфигурации.
        /// </summary>
        public static void ConfigureSchedule(ViewSchedule schedule, Dictionary<string, object> config)
        {
            if (schedule == null || config == null) return;

            // Настройка имени спецификации.
            if (config.TryGetValue("Name", out object name) && name is string newName)
            {
                schedule.Name = newName;
            }

            // Настройка фильтров.
            if (config.TryGetValue("Filters", out object filters) && filters is List<ScheduleFilter> scheduleFilters)
            {
                var definition = schedule.Definition;
                definition.ClearFilters();

                foreach (var filter in scheduleFilters)
                {
                    definition.AddFilter(filter);
                }
            }

            // Дополнительная конфигурация может быть добавлена здесь.
        }
    }
}