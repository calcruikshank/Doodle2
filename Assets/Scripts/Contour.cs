using Gameboard.Utilities;
using Hydra.HydraCommon.Utils.Comparers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameboard
{
    public class Contour : MonoBehaviour
    {
        public Gameboard gameboard;
        public static Contour singleton;
        [SerializeField] private Material drawMeshMaterial;
        private float lineThickness = 1f;
        private Vector3 lastPosition;
        Mesh meshBetweenPoints;
        public Dictionary<uint, NewBoardObjectInfo> boardObjectDict = new Dictionary<uint, NewBoardObjectInfo>();
        float colorTimerThresh = 5f;
        float colorTimer = 6f;
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
                //lineColor = new Color(UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f), UnityEngine.Random.Range(0, 1f));
                GameManager.singleton.lastSortingOrder++;
            }
        }
        private void BoardObjectsUpdated(object sender, List<TrackedBoardObject> updatedList)
        {

            foreach (TrackedBoardObject newBoardObject in updatedList)
            {
                if (!boardObjectDict.ContainsKey(newBoardObject.sessionId))
                {

                    drawMeshMaterial.color = GameManager.singleton.lineColor;
                    GameObject lastGameObject = new GameObject("DrawMeshSingle", typeof(MeshFilter), typeof(MeshRenderer));
                    GameManager.singleton.lastSortingOrder++;
                    lastGameObject.GetComponent<MeshRenderer>().sortingOrder = GameManager.singleton.lastSortingOrder;

                    GameObject meshBetweenPointsObject = new GameObject("DrawMeshSingle", typeof(MeshFilter), typeof(MeshRenderer));
                    meshBetweenPointsObject.GetComponent<MeshRenderer>().sortingOrder = GameManager.singleton.lastSortingOrder;

                    Vector3[] verticesToApply = newBoardObject.contourWorldVectors3D;

                    #region circleRegion
                    List<Vector3> circleVertices = new List<Vector3>();
                    if (verticesToApply.Length <= 8)
                    {
                        //TODO change 1f to be the width of the circle
                        circleVertices = CreateCircleBasedOffCenterPoint(newBoardObject.sceneWorldPosition, .1f);
                        verticesToApply = circleVertices.ToArray();
                    }

                    Debug.Log(verticesToApply.Length);
                    #endregion

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
                    mesh.triangles = triangleIndeces;

                    lastGameObject.GetComponent<MeshFilter>().mesh = mesh;
                    lastGameObject.GetComponent<MeshRenderer>().material = drawMeshMaterial;

                    lastGameObject.transform.position = new Vector3(lastGameObject.transform.position.x, lastGameObject.transform.position.y, -1);

                    Mesh meshBetweenPoints = new Mesh();

                    meshBetweenPoints.vertices = verticesToApply;
                    //meshBetweenPoints.uv = uvsToApply;
                    meshBetweenPoints.triangles = triangleIndeces;


                    meshBetweenPointsObject.GetComponent<MeshFilter>().mesh = meshBetweenPoints;
                    meshBetweenPointsObject.GetComponent<MeshRenderer>().material = drawMeshMaterial;

                    NewBoardObjectInfo newBoardObjectInfo = new NewBoardObjectInfo(lastGameObject, newBoardObject.sessionId, mesh, meshBetweenPointsObject, meshBetweenPoints);
                    boardObjectDict.Add(newBoardObject.sessionId, newBoardObjectInfo);
                    boardObjectDict[newBoardObject.sessionId].AddToSceneObjectPositions(newBoardObject.sceneWorldPosition, verticesToApply.ToList());

                    meshBetweenPointsObject.transform.localScale = new Vector3(1, 1, -1);
                }


                if (boardObjectDict.ContainsKey(newBoardObject.sessionId))
                {
                    float minDistance = .005f;
                    if (Vector2.Distance(lastPosition, newBoardObject.sceneWorldPosition) > minDistance)
                    {
                        drawMeshMaterial.color = GameManager.singleton.lineColor;
                        // Far enough from last point
                        Vector2 forwardVector = (newBoardObject.sceneWorldPosition - lastPosition);

                        lastPosition = newBoardObject.sceneWorldPosition;

                        Vector3[] verticesToApply = newBoardObject.contourWorldVectors3D;

                        List<Vector3> circleVertices = new List<Vector3>();
                        if (verticesToApply.Length <= 8)
                        {
                            //TODO change 1f to be the width of the circle
                            circleVertices = CreateCircleBasedOffCenterPoint(newBoardObject.sceneWorldPosition, .1f);
                            verticesToApply = circleVertices.ToArray();
                        }

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
                        correspondingMesh.triangles = triangleIndeces;

                        GameObject correspondingObject = boardObjectDict[newBoardObject.sessionId].gameObjectTiedToIt;
                        correspondingObject.GetComponent<MeshFilter>().mesh = correspondingMesh;
                        correspondingObject.GetComponent<MeshRenderer>().material = new Material(drawMeshMaterial);
                        correspondingObject.GetComponent<MeshRenderer>().material.color = GameManager.singleton.lineColor;

                        correspondingObject.GetComponent<MeshRenderer>().sortingOrder = GameManager.singleton.lastSortingOrder;


                        boardObjectDict[newBoardObject.sessionId].AddToSceneObjectPositions(newBoardObject.sceneWorldPosition, verticesToApply.ToList());
                        if (boardObjectDict[newBoardObject.sessionId].sceneObjectPositions.Count >= 2)
                        {
                            CreateMeshBetweenScenePoints(boardObjectDict[newBoardObject.sessionId], forwardVector);
                        }
                    }
                }
            }
        }

        private List<Vector3> CreateCircleBasedOffCenterPoint(Vector2 centerPoint, float width)
        {
            List<Vector3> circlePoints = new List<Vector3>();
            float radius = width / 2;
            for (int i = 0; i < 26; i++)
            {
                float angle = i * Mathf.PI * 2f / 26;
                Vector3 newPos = new Vector3((Mathf.Cos(angle) * radius) + centerPoint.x, Mathf.Sin(angle) * radius + centerPoint.y, -1);
               // GameObject go = Instantiate(GameObject.CreatePrimitive(PrimitiveType.Cube), newPos, Quaternion.identity);
                circlePoints.Add(newPos);
            }
            return circlePoints;
        }

        private void BoardObjectSessionsDeleted(object sender, List<uint> e)
        {
            foreach (uint id in e)
            {
                if (boardObjectDict.ContainsKey(id))
                {
                    GameObject correspondingObject = boardObjectDict[id].gameObjectTiedToIt;
                    GameObject correspondingMeshObject = boardObjectDict[id].GOBetweenPoints;
                    //Destroy(correspondingObject);
                    boardObjectDict.Remove(id);
                    //Destroy(correspondingObject);
                    //Destroy(correspondingMeshObject);

                }
            }

        }

        private void NewBoardObjectsCreated(object sender, List<TrackedBoardObject> newBoardObjectList)
        {
        }

        private void CreateMeshBetweenScenePoints(NewBoardObjectInfo newBoardObjectInfo, Vector3 forwardVector)
        {
            drawMeshMaterial.color = GameManager.singleton.lineColor;
            List<Vector2> pointsToTriangulate = new List<Vector2>();
            Vector3 offset = CenterOfVectors(newBoardObjectInfo.sceneObjectPositions.ToArray());
            List<Vector3> AllVerts = new List<Vector3>();
            for (int i = newBoardObjectInfo.sceneObjectPositions.Count - 2; i <= newBoardObjectInfo.sceneObjectPositions.Count - 1; i++)
            {
                for (int j = 0; j < newBoardObjectInfo.pointsInTheContour[i].Count; j++)
                {
                    //Debug.Log(i + " i " + " and then j " + j);
                    //AllVerts.Add(new Vector3(newBoardObjectInfo.pointsInTheContour[i][j].x - newBoardObjectInfo.sceneObjectPositions[i].x + offset.x, newBoardObjectInfo.pointsInTheContour[i][j].y - newBoardObjectInfo.sceneObjectPositions[i].y + offset.y, 1));
                    pointsToTriangulate.Add(new Vector3(newBoardObjectInfo.pointsInTheContour[i][j].x, newBoardObjectInfo.pointsInTheContour[i][j].y));
                    //pointsToTriangulate.Add(new Vector3(newBoardObjectInfo.pointsInTheContour[i][j].x - newBoardObjectInfo.sceneObjectPositions[i].x + offset.x, newBoardObjectInfo.pointsInTheContour[i][j].y - newBoardObjectInfo.sceneObjectPositions[i].y + offset.y, 1));
                    //pointsToTriangulate.Add(new Vector3(newBoardObjectInfo.pointsInTheContour[i][j].x, newBoardObjectInfo.pointsInTheContour[i][j].y, 1));
                }
            }


            pointsToTriangulate = JarvisMarchAlgorithm.GetConvexHull(pointsToTriangulate);

            //pointsToTriangulate.Sort(new ClockWiseComparer(offset));
            //adds first point again
            pointsToTriangulate.Add(pointsToTriangulate[0]);
            for (int y = 0; y < pointsToTriangulate.Count; y++)
            {
                AllVerts.Add(new Vector3(pointsToTriangulate[y].x, pointsToTriangulate[y].y));
            }

            Vector2[] pointsToTriangulateAfterSort = pointsToTriangulate.ToArray<Vector2>();
            //Vector2[] uvsToApply = new Vector2[AllVerts.Count];
            Triangulator triangulator = new Triangulator(pointsToTriangulateAfterSort);
            int[] triangleIndeces = triangulator.Triangulate();



            Mesh correspondingMeshBetweenPoints = newBoardObjectInfo.meshBetweenPoints;


            

            //correspondingMeshBetweenPoints.uv = uvsToApply;
            
            correspondingMeshBetweenPoints.triangles = triangleIndeces;
            correspondingMeshBetweenPoints.vertices = AllVerts.ToArray();


            GameObject correspondingMeshObjectBetweenPoints = newBoardObjectInfo.GOBetweenPoints;
            correspondingMeshObjectBetweenPoints.GetComponent<MeshRenderer>().sortingOrder = GameManager.singleton.lastSortingOrder;
            correspondingMeshObjectBetweenPoints.GetComponent<MeshFilter>().mesh = correspondingMeshBetweenPoints;
            correspondingMeshObjectBetweenPoints.GetComponent<MeshRenderer>().material = drawMeshMaterial;

            //no idea why i have to flip the scale maybe it's drawing upside down but reversing the triangle doesnt help. TODO
            correspondingMeshObjectBetweenPoints.transform.localScale = new Vector3(1, 1, -1);

            newBoardObjectInfo.sceneObjectPositions.RemoveAt(0);
            newBoardObjectInfo.pointsInTheContour.RemoveAt(0);

        }

        public Vector3 CenterOfVectors(Vector3[] vectors)
        {
            Vector3 sum = Vector3.zero;
            if (vectors == null || vectors.Length == 0)
            {
                return sum;
            }

            foreach (Vector3 vec in vectors)
            {
                sum += vec;
            }
            return sum / vectors.Length;
        }
    }
}