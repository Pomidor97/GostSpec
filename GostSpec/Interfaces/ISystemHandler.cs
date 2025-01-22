using Autodesk.Revit.DB;

namespace GostSpec.Interfaces
{
    public interface ISystemHandler
    {
        string GetSystemName(Element element);
        void SetSystemName(Element element, string systemName);
    }
}