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

        public Dictionary<uint, TrailRenderer> trDictionary = new Dictionary<uint, TrailRenderer>();
        List<Vector3> allVectorsToAdd = new List<Vector3>();
        [SerializeField] Transform transformWithTrail;

        public Dictionary<uint, GameObject> gameObjectDictionary = new Dictionary<uint, GameObject>();
        public Dictionary<uint, TrailRenderer> trailRenderDictionary = new Dictionary<uint, TrailRenderer>();
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
        [SerializeField] Transform emptyT;
        private void BoardObjectsUpdated(object sender, List<TrackedBoardObject> updatedList)
        {

            foreach (TrackedBoardObject newBoardObject in updatedList)
            {
                if (!gameObjectDictionary.ContainsKey(newBoardObject.sessionId))
                {

                    /*offset = newBoardObject.sceneWorldPosition;
                    lastSortingOrder++;
                    lastGameObject.GetComponent<MeshRenderer>().sortingOrder = lastSortingOrder;
                    lastGameObject.transform.position = new Vector3(newBoardObject.sceneWorldPosition.x, newBoardObject.sceneWorldPosition.y, 1);
                    gameObjectOnTouch = new GameObject("DrawMeshSingle", typeof(MeshFilter), typeof(MeshRenderer));
                    gameObjectOnTouch.GetComponent<MeshRenderer>().sortingOrder = lastSortingOrder;*/
                    //tr = Instantiate(transformWithTrail, lastGameObject.transform.position, Quaternion.identity).GetComponent<TrailRenderer>();
                    //tr.sortingLayerID = lastSortingOrder;
                    //tr.transform.localScale = new Vector3 ((newBoardObject.contourWorldVectors3D[0] - newBoardObject.contourWorldVectors3D[newBoardObject.contourWorldVectors3D.Length - 1]).magnitude, (newBoardObject.contourWorldVectors3D[0] - newBoardObject.contourWorldVectors3D[newBoardObject.contourWorldVectors3D.Length - 1]).magnitude, (newBoardObject.contourWorldVectors3D[0] - newBoardObject.contourWorldVectors3D[newBoardObject.contourWorldVectors3D.Length - 1]).magnitude);

                    lastGameObject = Instantiate(emptyT.gameObject, newBoardObject.sceneWorldPosition, Quaternion.identity);
                    TrailRenderer tr = lastGameObject.AddComponent<TrailRenderer>();
                    tr.material = drawMeshMaterial;
                    tr.time = Mathf.Infinity;
                    drawMeshMaterial.color = lineColor;
                    tr.numCapVertices = 90;
                    tr.numCornerVertices = 90;
                    Vector3[] verticesToApply = newBoardObject.contourWorldVectors3D;

                    /*Vector2[] uvsToApply = new Vector2[verticesToApply.Length];

                    Vector2[] vertsToTriangulate = new Vector2[verticesToApply.Length];

                    float greatestDistanceBetweenTwoPoints = 0;
                    for (int i = 0; i < verticesToApply.Length; i++)
                    {
                        verticesToApply[i] = new Vector3(verticesToApply[i].x - offset.x, verticesToApply[i].y - offset.y, 1);
                        vertsToTriangulate[i] = (Vector2)verticesToApply[i];
                        allVectorsToAdd.Add(verticesToApply[i]);
                    }*/

                    float greatestDistanceBetweenTwoPoints = 0;
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
                    //Triangulator triangulator = new Triangulator(vertsToTriangulate);


                    //int[] triangleIndeces = triangulator.Triangulate();

                    //Mesh mesh = new Mesh();
                    //mesh.MarkDynamic();
                    //mesh.vertices = verticesToApply;
                    //mesh.uv = uvsToApply;
                    //mesh.triangles = triangleIndeces;

                    /*lastGameObject.GetComponent<MeshFilter>().mesh = mesh;
                    lastGameObject.GetComponent<MeshRenderer>().material = drawMeshMaterial;
                    testObjectDict.Add(newBoardObject.sessionId, mesh);
                    trailRenderDictionary.Add(newBoardObject.sessionId, tr);
                    gameObjectOnTouch.GetComponent<MeshFilter>().mesh = mesh;
                    gameObjectOnTouch.GetComponent<MeshRenderer>().material = drawMeshMaterial;
                    gameObjectOnTouch.transform.position = new Vector3(newBoardObject.sceneWorldPosition.x, newBoardObject.sceneWorldPosition.y, 1);
                    trDictionary.Add(newBoardObject.sessionId, tr);*/
                    gameObjectDictionary.Add(newBoardObject.sessionId, lastGameObject);
                }

                if (gameObjectDictionary.ContainsKey(newBoardObject.sessionId))
                {
                    float minDistance = .1f;
                    if (Vector2.Distance(lastPosition, newBoardObject.sceneWorldPosition) > minDistance)
                    {
                        //Far enough from last point
                        Vector2 forwardVector = (newBoardObject.sceneWorldPosition - lastPosition).normalized;
                        if (lastPosition != Vector3.zero)
                        {
                        }
                        lastPosition = newBoardObject.sceneWorldPosition;

                        
                    }

                    GameObject correspondingObject = gameObjectDictionary[newBoardObject.sessionId];
                    correspondingObject.transform.position = new Vector3(newBoardObject.sceneWorldPosition.x, newBoardObject.sceneWorldPosition.y, 1);
                    //Vector3.MoveTowards(correspondingObject.transform.position, new Vector3(newBoardObject.sceneWorldPosition.x, newBoardObject.sceneWorldPosition.y, 1), 100 * Time.deltaTime);
                    //new Vector3(newBoardObject.sceneWorldPosition.x, newBoardObject.sceneWorldPosition.y, 1);
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
            
            foreach (uint id in e)
            {
                if (testObjectDict.ContainsKey(id))
                {
                   //trDictionary[id].BakeMesh(testObjectDict[id]);
                }
            }

        }

        private void NewBoardObjectsCreated(object sender, List<TrackedBoardObject> newBoardObjectList)
        {
        }
    }
}
