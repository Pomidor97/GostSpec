using System.Collections.Generic;
using Autodesk.Revit.DB;

namespace GostSpec.Models
{
    public class ElementData
    {
        public ElementId Id { get; set; }
        public string SystemName { get; set; }
        public Dictionary<string, string> ParameterValues { get; set; }
    }
}