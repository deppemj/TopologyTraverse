using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopologyTraverseByAPI
{
    public class BodyUtility
    {

        public interop.CimBaseAPI.ICimEntityList GetAllFaces(interop.CimBaseAPI.ICimEntity iBody)
        {
            interop.CimBaseAPI.IEntityQuery aBodyQuery = (interop.CimBaseAPI.IEntityQuery)iBody;
            interop.CimBaseAPI.IEntityFilter aBodyEntFilter = aBodyQuery.CreateFilter(interop.CimBaseAPI.EFilterEnumType.cmFilterEntityType);

            interop.CimBaseAPI.FilterType aBodyFilter1 = (interop.CimBaseAPI.FilterType)aBodyEntFilter;
            aBodyFilter1.Add(interop.CimBaseAPI.EntityEnumType.cmFace);
            aBodyQuery.SetFilter((interop.CimBaseAPI.IEntityFilter)aBodyFilter1);
            interop.CimBaseAPI.ICimEntityList aBodyFaceList = aBodyQuery.Select();
            return aBodyFaceList;
        }

        public int IsWirebody(interop.CimBaseAPI.ICimEntity iBody)
        {
            return iBody.IsOwnerWireBody();
        }

    }
}
