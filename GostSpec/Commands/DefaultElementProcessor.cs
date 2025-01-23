using System.Collections.Generic;
using Autodesk.Revit.DB;
using GostSpec.Interfaces;

namespace GostSpec.Commands
{
    public class DefaultElementProcessor : IElementProcessor
    {
        public void ProcessElements(Document doc, IEnumerable<Element> elements)
        {
        }
    }
}