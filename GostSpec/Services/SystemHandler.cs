using Autodesk.Revit.DB;
using GostSpec.Interfaces;
using GostSpec.Utils;

using ParameterUtils = GostSpec.Utils.ParameterUtils;

namespace GostSpec.Services
{
    public class SystemHandler : ISystemHandler
    {
        private const string S_SystemParam = "С_Система";

        public string GetSystemName(Element element)
        {
            var param = ParameterUtils.GetParam(element, S_SystemParam);
            return param?.AsString() ?? string.Empty;
        }

        public void SetSystemName(Element element, string systemName)
        {
            var param = ParameterUtils.GetParam(element, S_SystemParam);
            if (param != null && !param.IsReadOnly)
            {
                param.Set(systemName);
            }
        }
    }
}