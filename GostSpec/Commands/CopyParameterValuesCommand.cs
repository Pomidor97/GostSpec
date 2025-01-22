using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.Attributes;
using GostSpec.Interfaces;
using System.Collections.Generic;
using GostSpec.Utils;

namespace GostSpec.Commands
{
    [Transaction(TransactionMode.Manual)]
    public class CopyParameterValuesCommand : IExternalCommand
    {
        private readonly IElementProcessor _elementProcessor;

        public CopyParameterValuesCommand(IElementProcessor elementProcessor)
        {
            _elementProcessor = elementProcessor;
        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            List<BuiltInCategory> categories = new List<BuiltInCategory>
            {
                BuiltInCategory.OST_PipeFitting,
                BuiltInCategory.OST_DuctFitting,
                BuiltInCategory.OST_PipeInsulations
            };

            var allElements = new List<Element>();
            foreach (var category in categories)
            {
                allElements.AddRange(ElementUtils.GetElementsByCategory(doc, category));
            }

            _elementProcessor.ProcessElements(doc, allElements);
            return Result.Succeeded;
        }
    }
}