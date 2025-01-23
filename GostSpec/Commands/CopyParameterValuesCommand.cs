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
        
        public CopyParameterValuesCommand()
        {
            _elementProcessor = new DefaultElementProcessor();
        }

        public CopyParameterValuesCommand(IElementProcessor elementProcessor)
        {
            _elementProcessor = elementProcessor;
        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            List<BuiltInCategory> categories = new List<BuiltInCategory>
            {
                BuiltInCategory.OST_DuctCurves,               // Воздуховоды
                BuiltInCategory.OST_DuctFitting,              // Фитинги для воздуховодов
                BuiltInCategory.OST_DuctAccessory,            // Аксессуары для воздуховодов
                BuiltInCategory.OST_DuctSystem,               // Системы воздуховодов
                BuiltInCategory.OST_DuctInsulations,          // Изоляция воздуховодов    
                BuiltInCategory.OST_MechanicalEquipment,      // Механическое оборудование
                
                BuiltInCategory.OST_PipeCurves,            // Трубы
                BuiltInCategory.OST_PipeFitting,           // Фитинги для труб
                BuiltInCategory.OST_PipeAccessory,         // Аксессуары для труб
                BuiltInCategory.OST_PipingSystem,          // Системы трубопроводов
                BuiltInCategory.OST_PipeInsulations,       // Изоляция труб
                BuiltInCategory.OST_Sprinklers,            // Спринклеры
                BuiltInCategory.OST_PlumbingFixtures,      // Сантехнические приборы

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