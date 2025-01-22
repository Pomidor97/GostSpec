using Autodesk.Revit.DB;

namespace GostSpec.Interfaces
{
    public interface IFamilyInstanceUtils
    {
        FamilyInstance GetRootFamilyInstance(FamilyInstance fi);
    }
}