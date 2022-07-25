using Gameboard.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
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
        private Color lineColor = Color.green;
        public Dictionary<uint, Mesh> testObjectDict = new Dictionary<uint, Mesh>();
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

        private void BoardObjectsUpdated(object sender, List<TrackedBoardObject> newBoardObjectList)
        {
        }

        private void BoardObjectSessionsDeleted(object sender, List<uint> e)
        {
        }

        private void NewBoardObjectsCreated(object sender, List<TrackedBoardObject> newBoardObjectList)
        {
            foreach (TrackedBoardObject newBoardObject in newBoardObjectList)
            {
                CreateMeshObject();
                Mesh mesh = Utilities2.CreateEmptyMesh(); testObjectDict.Add(newBoardObject.sessionId, mesh);
                mesh.MarkDynamic();
                lastGameObject.GetComponent<MeshFilter>().mesh = mesh; 
                Material material = new Material(drawMeshMaterial);
                material.color = lineColor;
                foreach (Vector3 cwv in newBoardObject.contourWorldVectors3D)
                {
                    Utilities2.AddLinePoint(mesh, cwv, 1f);
                }
            }
        }
        private void CreateMeshObject()
        {
            lastGameObject = new GameObject("DrawMeshSingle", typeof(MeshFilter), typeof(MeshRenderer));
            lastSortingOrder++;
            lastGameObject.GetComponent<MeshRenderer>().sortingOrder = lastSortingOrder;
        }
    }
}
