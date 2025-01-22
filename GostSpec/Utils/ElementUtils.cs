using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace GostSpec.Utils
{
    public static class ElementUtils
    {
        public static IEnumerable<Element> GetElementsByCategory(Document doc, BuiltInCategory category)
        {
            return new FilteredElementCollector(doc)
                .OfCategory(category)
                .WhereElementIsNotElementType();
        }

        public static IEnumerable<Element> GetElementsByCatIdValue(Document doc, int catIdValue)
        {
            BuiltInCategory bic = (BuiltInCategory)catIdValue;
            return new FilteredElementCollector(doc)
                .OfCategory(bic)
                .WhereElementIsNotElementType();
        }
    }
}