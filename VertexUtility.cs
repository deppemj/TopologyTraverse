using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopologyTraverseByAPI
{
    public class VertexUtility
    {

        public interop.CimBaseAPI.ICimEntityList GetConnectedEdges(interop.CimBaseAPI.ICimEntity iVertex)
        {
            interop.CimBaseAPI.IEntityQuery aVertexQuery = (interop.CimBaseAPI.IEntityQuery)iVertex;
            interop.CimBaseAPI.IEntityFilter aEdgeEntityFilter = aVertexQuery.CreateFilter(interop.CimBaseAPI.EFilterEnumType.cmFilterEntityType);
            interop.CimBaseAPI.FilterType aIEdgeFilter = (interop.CimBaseAPI.FilterType)aEdgeEntityFilter;
            aIEdgeFilter.Add(interop.CimBaseAPI.EntityEnumType.cmEdge);
            aVertexQuery.SetFilter((interop.CimBaseAPI.IEntityFilter)aIEdgeFilter);
            interop.CimBaseAPI.ICimEntityList aEdgeList = aVertexQuery.Select();
            return aEdgeList;
        }

        public double[] GetVertexLocation(interop.CimBaseAPI.ICimEntity iVertex)
        {
            interop.CimBaseAPI.IGeometry3D aGeom = iVertex.Geometry;
            
            if (aGeom.Type== interop.CimBaseAPI.GeomType.cmGeomPoint)
            {
                interop.CimServicesAPI.IGeom3DPoint aPoint = (interop.CimServicesAPI.IGeom3DPoint)aGeom;
                double[] aPointData  = { aPoint.X, aPoint.Y, aPoint.Z };
                return aPointData;
            }
            return null;
        }

    }
}
