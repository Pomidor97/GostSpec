using Autodesk.Revit.DB;
using GostSpec.Interfaces;

namespace GostSpec.Services
{
    public class FamilyInstanceUtils : IFamilyInstanceUtils
    {
        public FamilyInstance GetRootFamilyInstance(FamilyInstance fi)
        {
            var superComponent = fi.SuperComponent as FamilyInstance;
            if (superComponent != null)
            {
                return GetRootFamilyInstance(superComponent);
            }
            else
            {
                return fi;
            }
        }
    }
}