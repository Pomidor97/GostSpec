using Autodesk.Revit.DB;
using GostSpec.Interfaces;

namespace GostSpec.Services
{
    public class SystemHandler : ISystemHandler
    {
        private const string S_SystemParam = "С_Система";

        public string GetSystemName(Element element)
        {
            Parameter param = element.LookupParameter(S_SystemParam);
            if (param != null)
                return param.AsString() ?? string.Empty;
            return string.Empty;
        }

        public void SetSystemName(Element element, string systemName)
        {
            Parameter param = element.LookupParameter(S_SystemParam);
            if (param != null && !param.IsReadOnly)
            {
                param.Set(systemName);
            }
        }
    }
}