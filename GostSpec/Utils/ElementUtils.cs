using Autodesk.Revit.DB;
using System.Collections.Generic;

namespace GostSpec.Utils
{
    public static class ElementUtils
    {
        public static IEnumerable<Element> GetElementsByCategory(Document doc, BuiltInCategory category)
        {
            return new FilteredElementCollector(doc).OfCategory(category).WhereElementIsNotElementType();
        }

        public static IEnumerable<Element> GetElementsByCatIdValue(Document doc, int catIdValue)
        {
            if (doc == null) return new List<Element>();

            try
            {
                BuiltInCategory bic = (BuiltInCategory)catIdValue;
                return new FilteredElementCollector(doc).OfCategory(bic).WhereElementIsNotElementType();
            }
            catch
            {
                // Возвращаем пустой список в случае ошибки.
                return new List<Element>();
            }
        }
        
        public static IEnumerable<Element> GetElementsByMultipleCategories(Document doc, IEnumerable<BuiltInCategory> categories)
        {
            var collector = new FilteredElementCollector(doc).WhereElementIsNotElementType();
            foreach (var category in categories)
            {
                collector = collector.OfCategory(category);
            }
            return collector;
        }
    }
}