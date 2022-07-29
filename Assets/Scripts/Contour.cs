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
        public Gameboard gameboard;
        public static Contour singleton;
        private int lastSortingOrder;
        [SerializeField] private Material drawMeshMaterial;
        private float lineThickness = 1f;
        private Color lineColor;
        private Vector3 lastPosition;


        public Dictionary<uint, NewBoardObjectInfo> boardObjectDict = new Dictionary<uint, NewBoardObjectInfo>();
        float colorTimerThresh = 5f;
        float colorTimer= 6f;
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
        private void Update()
        {
            colorTimer += Time.deltaTime;
            if (colorTimer > colorTimerThresh)
            {
                colorTimer = 0f;
                lineColor = new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
                drawMeshMaterial.color = lineColor;
                lastSortingOrder++;
            }
        }
        private void BoardObjectsUpdated(object sender, List<TrackedBoardObject> updatedList)
        {

            foreach (TrackedBoardObject newBoardObject in updatedList)
            {
                if (!boardObjectDict.ContainsKey(newBoardObject.sessionId))
                {

                    lastGameObject = new GameObject("DrawMeshSingle", typeof(MeshFilter), typeof(MeshRenderer));
                    lastSortingOrder++;
                    lastGameObject.GetComponent<MeshRenderer>().sortingOrder = lastSortingOrder;

                    Vector3[] verticesToApply = newBoardObject.contourWorldVectors3D;

                    Vector2[] uvsToApply = new Vector2[verticesToApply.Length];

                    Vector2[] vertsToTriangulate = new Vector2[verticesToApply.Length];
                    for (int i = 0; i < verticesToApply.Length; i++)
                    {
                        verticesToApply[i] = new Vector3(verticesToApply[i].x, verticesToApply[i].y, 1);
                        vertsToTriangulate[i] = (Vector2)verticesToApply[i];
                    }

                    Triangulator triangulator = new Triangulator(vertsToTriangulate);


                    int[] triangleIndeces = triangulator.Triangulate();

                    Mesh mesh = new Mesh();

                    mesh.vertices = verticesToApply;
                    mesh.uv = uvsToApply;
                    mesh.triangles = triangleIndeces;

                    lastGameObject.GetComponent<MeshFilter>().mesh = mesh;
                    lastGameObject.GetComponent<MeshRenderer>().material = drawMeshMaterial;
                    boardObjectDict.Add(newBoardObject.sessionId, new NewBoardObjectInfo(lastGameObject, newBoardObject.sessionId, mesh));


                }

                
                if (boardObjectDict.ContainsKey(newBoardObject.sessionId))
                {
                    float minDistance = .1f;
                    if (Vector2.Distance(lastPosition, newBoardObject.sceneWorldPosition) > minDistance)
                    {
                        // Far enough from last point
                        Vector2 forwardVector = (newBoardObject.sceneWorldPosition - lastPosition).normalized;

                        /*lastPosition = newBoardObject.sceneWorldPosition;
                        lastGameObject = new GameObject("DrawMeshSingle", typeof(MeshFilter), typeof(MeshRenderer));
                        lastSortingOrder++;
                        lastGameObject.GetComponent<MeshRenderer>().sortingOrder = lastSortingOrder;*/

                        Vector3[] verticesToApply = newBoardObject.contourWorldVectors3D;
                        Vector2[] uvsToApply = new Vector2[verticesToApply.Length];
                        Vector2[] vertsToTriangulate = new Vector2[verticesToApply.Length];
                        for (int i = 0; i < verticesToApply.Length; i++)
                        {
                            verticesToApply[i] = new Vector3(verticesToApply[i].x, verticesToApply[i].y, 1);
                            vertsToTriangulate[i] = (Vector2)verticesToApply[i];
                        }

                        Triangulator triangulator = new Triangulator(vertsToTriangulate);


                        int[] triangleIndeces = triangulator.Triangulate();


                        Mesh correspondingMesh = boardObjectDict[newBoardObject.sessionId].meshInBoardObject;
                        correspondingMesh.vertices = verticesToApply;
                        correspondingMesh.uv = uvsToApply;
                        correspondingMesh.triangles = triangleIndeces;

                        GameObject correspondingObject = boardObjectDict[newBoardObject.sessionId].gameObjectTiedToIt;
                        correspondingObject.GetComponent<MeshFilter>().mesh = correspondingMesh;
                        correspondingObject.GetComponent<MeshRenderer>().material = new Material(drawMeshMaterial);
                        correspondingObject.GetComponent<MeshRenderer>().material.color = lineColor;

                        correspondingObject.GetComponent<MeshRenderer>().sortingOrder = lastSortingOrder;
                    }
                }
            }
        }

        private void BoardObjectSessionsDeleted(object sender, List<uint> e)
        {
            foreach (uint id in e)
            {
                if (boardObjectDict.ContainsKey(id))
                {
                    GameObject correspondingObject = boardObjectDict[id].gameObjectTiedToIt;
                    //Destroy(correspondingObject);
                    boardObjectDict.Remove(id);
                    Destroy(correspondingObject);
                    /*Vector3[] verticesToApply = allVectorsToAdd.ToArray();
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
                    allVectorsToAdd.Clear();*/

                }
            }

        }

        private void NewBoardObjectsCreated(object sender, List<TrackedBoardObject> newBoardObjectList)
        {
        }
    }
}
