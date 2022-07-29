using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBoardObjectInfo
{
    public List<Vector3> listToCalculateBetween = new List<Vector3>();
    public uint objectSessionID;
    public GameObject gameObjectTiedToIt;
    public List<Vector3> sceneObjectPositions;
    public Mesh meshInBoardObject;
    public NewBoardObjectInfo(GameObject GOSent, uint SessionIDSent, Mesh meshCreated)
    {
        objectSessionID = SessionIDSent;
        gameObjectTiedToIt = GOSent;
        meshInBoardObject = meshCreated;
    }
}
