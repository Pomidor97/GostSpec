﻿using System.Collections.Generic;
using Autodesk.Revit.DB;
using GostSpec.Interfaces;
using GostSpec.Models;
using GostSpec.Utils;

namespace GostSpec.Services
{
    public class ElementProcessor : IElementProcessor
    {
        private readonly IParameterHandler _parameterHandler;
        private readonly IList<ParameterMapping> _parameterMappings;

        public ElementProcessor(IParameterHandler parameterHandler, IList<ParameterMapping> parameterMappings)
        {
            _parameterHandler = parameterHandler;
            _parameterMappings = parameterMappings;
        }

        public void ProcessElements(Document doc, IEnumerable<Element> elements)
        {
            using (Transaction tx = new Transaction(doc, "Process Elements"))
            {
                tx.Start();

                foreach (var element in elements)
                {
                    foreach (var mapping in _parameterMappings)
                    {
                        try
                        {
                            CopyParameters(element, mapping);
                        }
                        catch (System.Exception ex)
                        {
                            TaskDialogUtils.ShowWarning($"Ошибка при обработке элемента {element.Id}: {ex.Message}");
                        }
                    }
                }

                tx.Commit();
            }
        }

        private void CopyParameters(Element element, ParameterMapping mapping)
        {
            string sourceValue = _parameterHandler.GetParameterValue(element, mapping.SourceParam);
            _parameterHandler.SetParameterValue(element, mapping.TargetParam, sourceValue);
        }
    }
}