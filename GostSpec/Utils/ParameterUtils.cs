using Autodesk.Revit.DB;

namespace GostSpec.Utils
{
    public static class ParameterUtils
    {
        public static Parameter GetParameter(Element element, string paramName)
        {
            if (element == null) return null;
            return element.LookupParameter(paramName);
        }

        public static double GetDoubleValueInMM(Parameter parameter)
        {
            if (parameter == null) return 0;
            double val = parameter.AsDouble();
            return UnitUtils.ConvertFromInternalUnits(val, UnitTypeId.Millimeters);
        }
    }
}