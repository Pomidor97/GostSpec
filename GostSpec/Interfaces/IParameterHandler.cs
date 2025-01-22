using Autodesk.Revit.DB;

namespace GostSpec.Interfaces
{
    public interface IParameterHandler
    {
        string GetParameterValue(Element element, string paramName);
        void SetParameterValue(Element element, string paramName, string value);
    }
}