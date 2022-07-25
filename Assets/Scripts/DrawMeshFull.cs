using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawMeshFull : MonoBehaviour
{

    public static DrawMeshFull Instance { get; private set; }



    [SerializeField] private Material drawMeshMaterial;

    private GameObject lastGameObject;
    private int lastSortingOrder;
    private Mesh mesh;
    private Vector3 lastMouseWorldPosition;
    private float lineThickness = 1f;
    private Color lineColor = Color.green;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        // Only run logic if not over UI
        Vector3 mouseWorldPosition = Utilities.GetMouseWorldPosition();
        if (Input.GetMouseButtonDown(0))
        {
            // Mouse Down
            CreateMeshObject();
            mesh = Utilities.CreateMesh(mouseWorldPosition, mouseWorldPosition, mouseWorldPosition, mouseWorldPosition);
            mesh.MarkDynamic();
            lastGameObject.GetComponent<MeshFilter>().mesh = mesh;
            Material material = new Material(drawMeshMaterial);
            material.color = lineColor;
            lastGameObject.GetComponent<MeshRenderer>().material = material;
            Utilities.AddLinePoint(mesh, mouseWorldPosition, lineThickness);
        }

        if (Input.GetMouseButton(0))
        {
            // Mouse Held Down
            float minDistance = .1f;
            if (Vector2.Distance(lastMouseWorldPosition, mouseWorldPosition) > minDistance)
            {
                // Far enough from last point
                Vector2 forwardVector = (mouseWorldPosition - lastMouseWorldPosition).normalized;

                lastMouseWorldPosition = mouseWorldPosition;

                Utilities.AddLinePoint(mesh, mouseWorldPosition, lineThickness);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            // Mouse Up
            Utilities.AddLinePoint(mesh, mouseWorldPosition, lineThickness);
        }
    }

    private void CreateMeshObject()
    {
        lastGameObject = new GameObject("DrawMeshSingle", typeof(MeshFilter), typeof(MeshRenderer));
        lastSortingOrder++;
        lastGameObject.GetComponent<MeshRenderer>().sortingOrder = lastSortingOrder;
    }

    public void SetThickness(float lineThickness)
    {
        this.lineThickness = lineThickness;
    }

    public void SetColor(Color lineColor)
    {
        this.lineColor = lineColor;
    }

}
