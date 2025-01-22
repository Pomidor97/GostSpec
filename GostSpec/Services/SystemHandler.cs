using Autodesk.Revit.DB;
using GostSpec.Interfaces;

namespace GostSpec.Services
{
    /// <summary>
    /// Класс для управления параметром "С_Система".
    /// </summary>
    public class SystemHandler : ISystemHandler
    {
        private const string S_SystemParam = "С_Система";

        public string GetSystemName(Element element)
        {
            var param = ParameterUtils.GetParameter(element, S_SystemParam);
            return param?.AsString() ?? string.Empty;
        }

        public void SetSystemName(Element element, string systemName)
        {
            var param = ParameterUtils.GetParameter(element, S_SystemParam);
            if (param != null && !param.IsReadOnly)
            {
                param.Set(systemName);
            }
        }
    }
}