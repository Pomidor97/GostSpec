using Autodesk.Revit.DB;
using GostSpec.Interfaces;

namespace GostSpec.Services
{
    public class ParameterHandler : IParameterHandler
    {
        public string GetParameterValue(Element element, string paramName)
        {
            Parameter param = element.LookupParameter(paramName);
            if (param != null)
            {
                return param.AsString() ?? string.Empty;
            }
            return string.Empty;
        }

        public void SetParameterValue(Element element, string paramName, string value)
        {
            Parameter param = element.LookupParameter(paramName);
            if (param != null && !param.IsReadOnly)
            {
                param.Set(value);
            }
        }
    }
}