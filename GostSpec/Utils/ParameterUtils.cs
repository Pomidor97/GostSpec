using Autodesk.Revit.DB;

namespace GostSpec.Utils
{
    public static class ParameterUtils
    {
        public static Parameter GetParameter(Element element, string paramName)
        {
            if (element == null || string.IsNullOrWhiteSpace(paramName))
                return null;

            return element.LookupParameter(paramName);
        }

        public static double GetDoubleValueInUnits(Parameter parameter, ForgeTypeId unitType)
        {
            if (parameter == null) return 0;
            return UnitUtils.ConvertFromInternalUnits(parameter.AsDouble(), unitType);
        }
    }
}