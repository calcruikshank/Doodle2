using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBoardObjectInfo
{
    public List<List<Vector3>> pointsInTheContour = new List<List<Vector3>>();
    //don't need object session id since its in a dict with it TODO
    public uint objectSessionID;
    public GameObject gameObjectTiedToIt;
    public GameObject GOBetweenPoints;
    public List<Vector3> sceneObjectPositions = new List<Vector3>();
    public Mesh meshInBoardObject;
    public Mesh meshBetweenPoints;
    public NewBoardObjectInfo(GameObject GOSent, uint SessionIDSent, Mesh meshCreated, GameObject GOBetweenPointsSent, Mesh meshSentBetweenPoints)
    {
        objectSessionID = SessionIDSent;
        gameObjectTiedToIt = GOSent;
        meshInBoardObject = meshCreated;
        GOBetweenPoints = GOBetweenPointsSent;
        meshBetweenPoints = meshSentBetweenPoints; 
    }

    public void AddToSceneObjectPositions(Vector3 positionSent, List<Vector3> pointsInTheContourSent)
    {
        pointsInTheContour.Add(pointsInTheContourSent);
        sceneObjectPositions.Add(new Vector3(positionSent.x, positionSent.y, 1));
    }
}
