using Autodesk.Revit.DB;
using GostSpec.Interfaces;

namespace GostSpec.Commands
{
    public class DefaultScheduleGenerator : IScheduleGenerator
    {
        public ViewSchedule CreateScheduleForSystem(Document doc, string systemName)
        {
            return null;
        }

        public void ConfigureScheduleFilters(ViewSchedule schedule, string systemName)
        {
        }
    }
}