using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using GostSpec.Interfaces;
using System.Collections.Generic;

namespace GostSpec.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CopyParameterValuesCommand : IExternalCommand
    {
        private readonly IElementProcessor _elementProcessor;

        // Вариант внедрения через DI (или получаем через ServiceLocator):
        public CopyParameterValuesCommand(IElementProcessor elementProcessor)
        {
            _elementProcessor = elementProcessor;
        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Document doc = uiapp.ActiveUIDocument.Document;

            List<BuiltInCategory> categories = new List<BuiltInCategory>()
            {
                BuiltInCategory.OST_PipeFitting,
                BuiltInCategory.OST_DuctFitting
            };

            var allElements = new List<Element>();
            foreach (var cat in categories)
            {
                var collector = new FilteredElementCollector(doc).OfCategory(cat).WhereElementIsNotElementType();
                allElements.AddRange(collector);
            }

            _elementProcessor.ProcessElements(doc, allElements);

            return Result.Succeeded;
        }
    }
}