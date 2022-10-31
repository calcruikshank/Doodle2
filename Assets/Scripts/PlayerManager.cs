using Gameboard;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Transform playerPrefab;
    Transform instantiatedPlayer;

    Canvas mainCanvas;

    [SerializeField] List<Transform> playerOnCanvasPositions;

    [SerializeField] Transform playerScorePrefab;
    // Start is called before the first frame update
    void Start()
    {
        mainCanvas = FindObjectOfType<Canvas>();
        for (int i = 0; i < 8; i++)
        {

            SpawnPlayerScore(playerOnCanvasPositions[i]);
        }
    }

    private void SpawnPlayerScore(Transform trsfrm)
    {
        Instantiate(playerScorePrefab, trsfrm.position, trsfrm.rotation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
