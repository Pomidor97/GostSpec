using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace GostSpec.Interfaces
{
    public interface IElementProcessor
    {
        void ProcessElements(Document doc, IEnumerable<Element> elements);
    }
}