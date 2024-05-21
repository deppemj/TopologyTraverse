using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TopologyTraverseByAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Open part document continue...");
            Console.WriteLine("Traversal Started !!");
            interop.CimAppAccess.AppAccess CimAppAcc = new interop.CimAppAccess.AppAccess();
            interop.CimatronE.IApplication aCimApplication = (interop.CimatronE.IApplication)CimAppAcc.GetActiveApplication();
            interop.CimatronE.ICimDocument aDoc = aCimApplication.GetActiveDoc();
            interop.CimMdlrAPI.IModelContainer aContainer = (interop.CimMdlrAPI.IModelContainer)aDoc;
            interop.CimMdlrAPI.IMdlrModel aMdlrModel = (interop.CimMdlrAPI.IMdlrModel)aContainer.Model;
            interop.CimBaseAPI.IEntityQuery aMdlrQuery = (interop.CimBaseAPI.IEntityQuery)aMdlrModel;
            interop.CimBaseAPI.IEntityFilter aEntityFilter = aMdlrQuery.CreateFilter(interop.CimBaseAPI.EFilterEnumType.cmFilterEntityType);
            //Get All bodies in MdlrModel
            interop.CimBaseAPI.FilterType aFilter = (interop.CimBaseAPI.FilterType)aEntityFilter;
            aFilter.Add(interop.CimBaseAPI.EntityEnumType.cmBody);
            aMdlrQuery.SetFilter((interop.CimBaseAPI.IEntityFilter)aFilter);
            interop.CimBaseAPI.ICimEntityList aBodyList = aMdlrQuery.Select();

            if (aBodyList.Count > 0)
            {
                Console.WriteLine("Bodies Count = " + aBodyList.Count);
                foreach (interop.CimBaseAPI.ICimEntity aBody in aBodyList)
                {
                    int aBodyId = aBody.ID;

                    //------------------------------------------------BodyUtility------------------------------------------------
                    //Use body utility functions
                    BodyUtility aBodyUtility = new BodyUtility();
                    //Get All Faces from body 
                    interop.CimBaseAPI.ICimEntityList aFaceListRelatedToBody = aBodyUtility.GetAllFaces(aBody);
                    int aIsWireBody = aBodyUtility.IsWirebody(aBody);
                    //------------------------------------------------------------------------------------------------

                    if (aIsWireBody == 1) //Sketch is also treated as body
                        continue;


                    //Get All Faces in aBody
                    interop.CimBaseAPI.IEntityQuery aBodyQuery = (interop.CimBaseAPI.IEntityQuery)aBody;
                    interop.CimBaseAPI.IEntityFilter aEntFilter = aBodyQuery.CreateFilter(interop.CimBaseAPI.EFilterEnumType.cmFilterEntityType);
                    interop.CimBaseAPI.FilterType aFaceFilter = (interop.CimBaseAPI.FilterType)aEntFilter;
                    aFaceFilter.Add(interop.CimBaseAPI.EntityEnumType.cmFace);
                    aBodyQuery.SetFilter((interop.CimBaseAPI.IEntityFilter)aFaceFilter);
                    interop.CimBaseAPI.ICimEntityList aFaces = aBodyQuery.Select();
                    if (aFaces.Count > 0)
                    {
                        Console.WriteLine("Face Count = " + aFaces.Count);
                        foreach (interop.CimBaseAPI.ICimEntity aFace in aFaces)
                        {
                            int aFaceId = aFace.ID;
                            //--------------------------------------------FaceUtility----------------------------------------------------
                            //Use face utility functions
                            FaceUtility aFaceUtility = new FaceUtility();
                            //Get All Edges in face
                            interop.CimBaseAPI.ICimEntityList aEdgeListRelatedToFace = aFaceUtility.GetAllEdges(aFace);
                            //Get All Loops in face
                            interop.CimBaseAPI.ICimEntityList aLoopListRelatedToFace = aFaceUtility.GetAllLoops(aFace);
                            //Get parent body of face
                            interop.CimBaseAPI.ICimEntity aBodyRelatedToFace = aFaceUtility.GetParentBody(aFace);
                            //Get surface data of face, IGeom3DSurface has all functions which provide surface information
                            interop.CimServicesAPI.IGeom3DSurface aSurface = aFaceUtility.GetSurfaceData(aFace);
                            //------------------------------------------------------------------------------------------------

                            if (aEdgeListRelatedToFace.Count > 0)
                            {
                                foreach (interop.CimBaseAPI.ICimEntity aEdge in aEdgeListRelatedToFace)
                                {
                                    int aEdgeId = aEdge.ID;
                                    //-------------------------------------------EdgeUtility-----------------------------------------------------
                                    //Use edge utility functions
                                    EdgeUtility aEdgeUtility = new EdgeUtility();
                                    interop.CimBaseAPI.ICimEntityList aCoedgeListRelatedToEdge = aEdgeUtility.GetCoedges(aEdge);
                                    interop.CimBaseAPI.ICimEntityList aFaceListRelatedToEdge = aEdgeUtility.GetConnectedFaces(aEdge);
                                    interop.CimBaseAPI.ICimEntityList aVerticesListRelatedToEdge = aEdgeUtility.GetVertices(aEdge);
                                    //Get curve data of edge, IGeom3DCurve has all functions which provide curve information
                                    interop.CimServicesAPI.IGeom3DCurve aCurve = aEdgeUtility.GetCurve(aEdge);
                                    //------------------------------------------------------------------------------------------------

                                    if (aVerticesListRelatedToEdge.Count > 0)
                                    {
                                        foreach (interop.CimBaseAPI.ICimEntity aVertex in aVerticesListRelatedToEdge)
                                        {
                                            //--------------------------------------------VertexUtility----------------------------------------------------
                                            //Use vertex utility functions
                                            VertexUtility aVertexUtility = new VertexUtility();
                                            interop.CimBaseAPI.ICimEntityList aEdgeListRelatedToVertex = aVertexUtility.GetConnectedEdges(aVertex);
                                            double[] aVertexLocation = aVertexUtility.GetVertexLocation(aVertex);
                                            //------------------------------------------------------------------------------------------------
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
    }
}
