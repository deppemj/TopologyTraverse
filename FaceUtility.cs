using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopologyTraverseByAPI
{
    public class FaceUtility
    {

        public interop.CimBaseAPI.ICimEntityList GetAllEdges(interop.CimBaseAPI.ICimEntity iFace)
        {
            interop.CimBaseAPI.IEntityQuery aFaceQuery = (interop.CimBaseAPI.IEntityQuery)iFace;
            interop.CimBaseAPI.IEntityFilter aEdgeEntityFilter = aFaceQuery.CreateFilter(interop.CimBaseAPI.EFilterEnumType.cmFilterEntityType);
            interop.CimBaseAPI.FilterType aEdgeFilter = (interop.CimBaseAPI.FilterType)aEdgeEntityFilter;
            aEdgeFilter.Add(interop.CimBaseAPI.EntityEnumType.cmEdge);
            aFaceQuery.SetFilter((interop.CimBaseAPI.IEntityFilter)aEdgeFilter);
            interop.CimBaseAPI.ICimEntityList aEdgeList = aFaceQuery.Select();
            int aEdgeCount = aEdgeList.Count;
            return aEdgeList;
        }

        public interop.CimBaseAPI.ICimEntityList GetAllLoops(interop.CimBaseAPI.ICimEntity iFace)
        {
            interop.CimBaseAPI.IEntityQuery aFaceQuery = (interop.CimBaseAPI.IEntityQuery)iFace;
            interop.CimBaseAPI.IEntityFilter aEntFilter = aFaceQuery.CreateFilter(interop.CimBaseAPI.EFilterEnumType.cmFilterEntityType);
            interop.CimBaseAPI.FilterType aLoopFilter = (interop.CimBaseAPI.FilterType)aEntFilter;
            aLoopFilter.Add(interop.CimBaseAPI.EntityEnumType.cmLoop);
            aFaceQuery.SetFilter((interop.CimBaseAPI.IEntityFilter)aLoopFilter);
            interop.CimBaseAPI.ICimEntityList aLoopList = aFaceQuery.Select();
            return aLoopList;

        }
        public interop.CimServicesAPI.IGeom3DSurface GetSurfaceData(interop.CimBaseAPI.ICimEntity iFace)
        {
            interop.CimBaseAPI.IGeometry3D aGeom = iFace.Geometry;
            interop.CimServicesAPI.IGeom3DSurface aSurf = (interop.CimServicesAPI.IGeom3DSurface)aGeom;
            if (aSurf.SurfType == interop.CimServicesAPI.GeomSurfaceType.cmGeomSurfPlane)
            {
                interop.CimServicesAPI.IGeom3DPlan SurfPlan = (interop.CimServicesAPI.IGeom3DPlan)aSurf;

                //return ("Plane Surface");
            }
            else if (aSurf.SurfType == interop.CimServicesAPI.GeomSurfaceType.cmGeomSurfCone)
            {
                interop.CimServicesAPI.IGeom3DCone SurfCone = (interop.CimServicesAPI.IGeom3DCone)aSurf;

                //return ("Cone Surface");
            }
            else if (aSurf.SurfType == interop.CimServicesAPI.GeomSurfaceType.cmGeomSurfSphere)
            {
                interop.CimServicesAPI.IGeom3DSphere SurfSphere = (interop.CimServicesAPI.IGeom3DSphere)aSurf;
               // return ("Sphere Surface");
            }
            else if (aSurf.SurfType == interop.CimServicesAPI.GeomSurfaceType.cmGeomSurfSpline)
            {
                interop.CimServicesAPI.IGeom3DSpline SurfSpline = (interop.CimServicesAPI.IGeom3DSpline)aSurf;
                //return ("Spline Surface");
            }
            else if (aSurf.SurfType == interop.CimServicesAPI.GeomSurfaceType.cmGeomSurfTorus)
            {
                interop.CimServicesAPI.IGeom3DTorus SurfTorus = (interop.CimServicesAPI.IGeom3DTorus)aSurf;
                //return ("Torus Surface");
            }
            return aSurf;
        }

        public interop.CimBaseAPI.ICimEntity GetParentBody(interop.CimBaseAPI.ICimEntity iFace)
        {
            interop.CimBaseAPI.IEntityQuery aFaceQuery = (interop.CimBaseAPI.IEntityQuery)iFace;
            interop.CimBaseAPI.IEntityFilter aEntFilter = aFaceQuery.CreateFilter(interop.CimBaseAPI.EFilterEnumType.cmFilterEntityType);
            interop.CimBaseAPI.FilterType aBodyFilter = (interop.CimBaseAPI.FilterType)aEntFilter;
            aBodyFilter.Add(interop.CimBaseAPI.EntityEnumType.cmBody);
            aFaceQuery.SetFilter((interop.CimBaseAPI.IEntityFilter)aBodyFilter);
            interop.CimBaseAPI.ICimEntityList aBodyRelatedtoFace = aFaceQuery.Select();
            if (aBodyRelatedtoFace.Count > 0)
            {
                foreach (interop.CimBaseAPI.ICimEntity aBody in aBodyRelatedtoFace)
                {
                    if (aBody.Type == interop.CimBaseAPI.EntityEnumType.cmBody)
                    {
                        int aId = aBody.ID;
                        return aBodyRelatedtoFace[1];
                    }
                }
                
            }

            return null;
        }


    }
}
