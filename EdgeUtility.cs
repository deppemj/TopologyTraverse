using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopologyTraverseByAPI
{
    public class EdgeUtility
    {


        public interop.CimBaseAPI.ICimEntityList GetCoedges(interop.CimBaseAPI.ICimEntity iEdge)
        {
            interop.CimBaseAPI.IEntityQuery aIEdgeQuery = (interop.CimBaseAPI.IEntityQuery)iEdge;
            interop.CimBaseAPI.IEntityFilter aIEdgeEntityFilter = aIEdgeQuery.CreateFilter(interop.CimBaseAPI.EFilterEnumType.cmFilterEntityType);
            interop.CimBaseAPI.FilterType aIEdgeFilter = (interop.CimBaseAPI.FilterType)aIEdgeEntityFilter;
            aIEdgeFilter.Add(interop.CimBaseAPI.EntityEnumType.cmCoedge);
            aIEdgeQuery.SetFilter((interop.CimBaseAPI.IEntityFilter)aIEdgeFilter);
            interop.CimBaseAPI.ICimEntityList aCoedgeList = aIEdgeQuery.Select();
            return aCoedgeList;
        }
        public interop.CimBaseAPI.ICimEntityList GetVertices(interop.CimBaseAPI.ICimEntity iEdge)
        {
            interop.CimBaseAPI.IEntityQuery aIEdgeQuery = (interop.CimBaseAPI.IEntityQuery)iEdge;
            interop.CimBaseAPI.IEntityFilter aIEdgeEntityFilter = aIEdgeQuery.CreateFilter(interop.CimBaseAPI.EFilterEnumType.cmFilterEntityType);
            interop.CimBaseAPI.FilterType aVertexFiletr = (interop.CimBaseAPI.FilterType)aIEdgeEntityFilter;
            aVertexFiletr.Add(interop.CimBaseAPI.EntityEnumType.cmVertex);
            aIEdgeQuery.SetFilter((interop.CimBaseAPI.IEntityFilter)aVertexFiletr);
            interop.CimBaseAPI.ICimEntityList avertexlist = aIEdgeQuery.Select();
            return avertexlist;
        }

        public interop.CimServicesAPI.IGeom3DCurve GetCurve(interop.CimBaseAPI.ICimEntity iEdge)
        {
            interop.CimBaseAPI.IGeometry3D aGeom = iEdge.Geometry;

            if (iEdge.Type == interop.CimBaseAPI.EntityEnumType.cmEdge)
            {

                if (aGeom.Type == interop.CimBaseAPI.GeomType.cmGeomCurve)
                {
                    interop.CimServicesAPI.IGeom3DCurve GeomCurve = (interop.CimServicesAPI.IGeom3DCurve)aGeom;
                    if (GeomCurve.CurveType == interop.CimServicesAPI.GeomCurveType.cmGeomCurveStraight)
                    {
                        interop.CimServicesAPI.IGeom3DStraight aStraight = (interop.CimServicesAPI.IGeom3DStraight)GeomCurve;
                        double[] RetRootPoint = (double[])aStraight.RootPoint;
                        double[] RetVector = (double[])aStraight.Direction;
                        //return ("Straight Curve X=" + RetRootPoint[0] + ", Y=" + RetRootPoint[1] + " ,Z=" + RetRootPoint[2]);
                    }
                    else if (GeomCurve.CurveType == interop.CimServicesAPI.GeomCurveType.cmGeomCurveIntCurv)
                    {
                        interop.CimServicesAPI.IGeom3DIntCurve aIntCurve = (interop.CimServicesAPI.IGeom3DIntCurve)GeomCurve;
                        double[] controlPoint = (double[])aIntCurve.ControlPoints;
                        //return (controlPoint.Length / 3).ToString();
                    }
                    else if (GeomCurve.CurveType == interop.CimServicesAPI.GeomCurveType.cmGeomCurveEllipse)
                    {
                        interop.CimServicesAPI.IGeom3DEllipse aEllipse = (interop.CimServicesAPI.IGeom3DEllipse)GeomCurve;

                        //return ("Ellipse Curve, Radius=" + aEllipse.RadiusMajor.ToString());
                    }
                    return GeomCurve;
                }
            }
            return null;
        }

        public interop.CimBaseAPI.ICimEntityList GetConnectedFaces(interop.CimBaseAPI.ICimEntity iEdge)
        {
            //Get Edge Related faces
            interop.CimBaseAPI.IEntityQuery IEdgeQuery = (interop.CimBaseAPI.IEntityQuery)iEdge;
            interop.CimBaseAPI.IEntityFilter aEntFilter = IEdgeQuery.CreateFilter(interop.CimBaseAPI.EFilterEnumType.cmFilterEntityType);
            interop.CimBaseAPI.FilterType aFaceFilter = (interop.CimBaseAPI.FilterType)aEntFilter;
            aFaceFilter.Add(interop.CimBaseAPI.EntityEnumType.cmFace);
            IEdgeQuery.SetFilter((interop.CimBaseAPI.IEntityFilter)aFaceFilter);
            interop.CimBaseAPI.ICimEntityList aEdgeRelatedFaces = IEdgeQuery.Select();
            return aEdgeRelatedFaces;
        }


    }
}
