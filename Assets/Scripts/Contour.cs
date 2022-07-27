using Gameboard.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameboard
{
    public class Contour : MonoBehaviour
    {
        private GameObject lastGameObject;
        private GameObject gameObjectOnTouch;
        public Gameboard gameboard;
        public static Contour singleton;
        private int lastSortingOrder;
        [SerializeField] private Material drawMeshMaterial;
        private float lineThickness = 1f;
        private Color lineColor = Color.green;
        public Dictionary<uint, Mesh> testObjectDict = new Dictionary<uint, Mesh>();
        private Vector3 lastPosition;

        List<Vector3> allVectorsToAdd = new List<Vector3>();
       [SerializeField] Transform transformWithTrail;

        IEnumerator Start()
        {
            while (gameboard.boardTouchController == null || gameboard.boardTouchController.boardTouchHandler == null)
            {
                yield return new WaitForEndOfFrame();
            }
            gameboard.boardTouchController.boardTouchHandler.NewBoardObjectsCreated += NewBoardObjectsCreated;
            gameboard.boardTouchController.boardTouchHandler.BoardObjectSessionsDeleted += BoardObjectSessionsDeleted;
            gameboard.boardTouchController.boardTouchHandler.BoardObjectsUpdated += BoardObjectsUpdated;

            singleton = this;
        }
        Vector3 offset = new Vector3();
        private void BoardObjectsUpdated(object sender, List<TrackedBoardObject> updatedList)
        {
            float minDistance = .1f;

            foreach (TrackedBoardObject newBoardObject in updatedList)
            {
                if (!testObjectDict.ContainsKey(newBoardObject.sessionId))
                {

                    offset = newBoardObject.sceneWorldPosition;
                    lastGameObject = new GameObject("DrawMeshSingle", typeof(MeshFilter), typeof(MeshRenderer));
                    lastSortingOrder++;
                    lastGameObject.GetComponent<MeshRenderer>().sortingOrder = lastSortingOrder;
                    gameObjectOnTouch = new GameObject("DrawMeshSingle", typeof(MeshFilter), typeof(MeshRenderer));
                    gameObjectOnTouch.GetComponent<MeshRenderer>().sortingOrder = lastSortingOrder;
                    //tr = Instantiate(transformWithTrail, lastGameObject.transform.position, Quaternion.identity).GetComponent<TrailRenderer>();
                    //tr.sortingLayerID = lastSortingOrder;
                    //tr.transform.localScale = new Vector3 ((newBoardObject.contourWorldVectors3D[0] - newBoardObject.contourWorldVectors3D[newBoardObject.contourWorldVectors3D.Length - 1]).magnitude, (newBoardObject.contourWorldVectors3D[0] - newBoardObject.contourWorldVectors3D[newBoardObject.contourWorldVectors3D.Length - 1]).magnitude, (newBoardObject.contourWorldVectors3D[0] - newBoardObject.contourWorldVectors3D[newBoardObject.contourWorldVectors3D.Length - 1]).magnitude);
                    TrailRenderer tr = lastGameObject.AddComponent<TrailRenderer>();
                    
                    Vector3[] verticesToApply = newBoardObject.contourWorldVectors3D;

                    Vector2[] uvsToApply = new Vector2[verticesToApply.Length];

                    Vector2[] vertsToTriangulate = new Vector2[verticesToApply.Length];

                    float greatestDistanceBetweenTwoPoints = 0;
                    for (int i = 0; i < verticesToApply.Length; i++)
                    {
                        verticesToApply[i] = new Vector3(verticesToApply[i].x - offset.x, verticesToApply[i].y - offset.y, 1);
                        vertsToTriangulate[i] = (Vector2)verticesToApply[i];
                        allVectorsToAdd.Add(verticesToApply[i]);
                    }

                    for (int i = 0; i < verticesToApply.Length; i++)
                    {
                        for (int j = 1; j < verticesToApply.Length; j++)
                        {
                            if ((Mathf.Abs((verticesToApply[j] - verticesToApply[i]).magnitude) > greatestDistanceBetweenTwoPoints))
                            {
                                greatestDistanceBetweenTwoPoints = Mathf.Abs((verticesToApply[j] - verticesToApply[i]).magnitude);
                                Debug.Log((verticesToApply[j] + " " + verticesToApply[i]));
                            }
                        }
                    }
                    tr.startWidth = greatestDistanceBetweenTwoPoints;
                    Triangulator triangulator = new Triangulator(vertsToTriangulate);


                    int[] triangleIndeces = triangulator.Triangulate();

                    Mesh mesh = new Mesh();

                    mesh.vertices = verticesToApply;
                    mesh.uv = uvsToApply;
                    mesh.triangles = triangleIndeces;

                    lastGameObject.GetComponent<MeshFilter>().mesh = mesh;
                    lastGameObject.GetComponent<MeshRenderer>().material = drawMeshMaterial;
                    testObjectDict.Add(newBoardObject.sessionId, mesh);
                    gameObjectOnTouch.GetComponent<MeshFilter>().mesh = mesh;
                    gameObjectOnTouch.GetComponent<MeshRenderer>().material = drawMeshMaterial;

                }

                if (testObjectDict.ContainsKey(newBoardObject.sessionId))
                {
                    lastGameObject.transform.position = new Vector3(newBoardObject.sceneWorldPosition.x, newBoardObject.sceneWorldPosition.y, 1);

                    /*Mesh correspondingMesh = testObjectDict[newBoardObject.sessionId];
                    Vector3[] verticesToApply = newBoardObject.contourWorldVectors3D;
                    Vector2[] uvsToApply = new Vector2[verticesToApply.Length];
                    Vector2[] vertsToTriangulate = new Vector2[verticesToApply.Length];
                    for (int i = 0; i < verticesToApply.Length; i++)
                    {
                        verticesToApply[i] = new Vector3(verticesToApply[i].x, verticesToApply[i].y, 1);
                        vertsToTriangulate[i] = (Vector2)verticesToApply[i];
                        allVectorsToAdd.Add(verticesToApply[i]);
                    }

                    Triangulator triangulator = new Triangulator(vertsToTriangulate);

                    int[] triangleIndeces = triangulator.Triangulate();

                    /*correspondingMesh.vertices = verticesToApply;
                    correspondingMesh.uv = uvsToApply;
                    correspondingMesh.triangles = triangleIndeces;*/

                }


                /*if (testObjectDict.ContainsKey(newBoardObject.sessionId))
                {
                    lastGameObject = new GameObject("DrawMeshSingle", typeof(MeshFilter), typeof(MeshRenderer));
                    lastSortingOrder++;
                    lastGameObject.GetComponent<MeshRenderer>().sortingOrder = lastSortingOrder;

                    Vector3[] verticesToApply = newBoardObject.contourWorldVectors3D;
                    for (int i = 0; i < verticesToApply.Length; i++)
                    {
                        if (!allVectorsToAdd.Contains(verticesToApply[i]))
                        {
                            allVectorsToAdd.Add(verticesToApply[i]);
                        }
                    }
                }*/
            }
        }
        //Mesh correspondingMesh;
        private void BoardObjectSessionsDeleted(object sender, List<uint> e)
        {
            /*foreach (uint id in e)
            {
                if (testObjectDict.ContainsKey(id))
                {

                    Vector3[] verticesToApply = allVectorsToAdd.ToArray();
                    Vector2[] uvsToApply = new Vector2[allVectorsToAdd.Count];
                    Vector2[] vertsToTriangulate = new Vector2[verticesToApply.Length];
                    for (int i = 0; i < verticesToApply.Length; i++)
                    {
                        verticesToApply[i] = new Vector3(verticesToApply[i].x, verticesToApply[i].y, 1);


                        vertsToTriangulate[i] = (Vector2)verticesToApply[i];
                    }
                    Triangulator triangulator = new Triangulator(vertsToTriangulate);


                    int[] triangleIndeces = triangulator.Triangulate();
                    if (correspondingMesh != null)
                    {
                        correspondingMesh.vertices = verticesToApply;
                        correspondingMesh.uv = uvsToApply;
                        correspondingMesh.triangles = triangleIndeces;
                    }
                    allVectorsToAdd.Clear();
                }
            }*/

        }

        private void NewBoardObjectsCreated(object sender, List<TrackedBoardObject> newBoardObjectList)
        {
        }
    }
}
