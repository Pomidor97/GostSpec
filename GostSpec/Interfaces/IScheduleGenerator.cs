using Autodesk.Revit.DB;

namespace GostSpec.Interfaces
{
    public interface IScheduleGenerator
    {
        ViewSchedule CreateScheduleForSystem(Document doc, string systemName);
        void ConfigureScheduleFilters(ViewSchedule schedule, string systemName);
    }
}