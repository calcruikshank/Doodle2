using Gameboard;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Transform playerPrefab;
    Transform instantiatedPlayer;

    [SerializeField] List<Transform> playerScores;
    [SerializeField] Transform playerHud;

    List<Transform> playersInstantiated = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer(new Vector3(1400, 150, 0));
        for (int i = 0; i < 8; i++)
        {
            SpawnPlayerScore(playerScores[i]);
        }
    }

    private void SpawnPlayerScore(Transform trnsfrm)
    {
        Transform instantiatedScore = Instantiate(playerHud, FindObjectOfType<Canvas>().transform);
        instantiatedScore.transform.position = trnsfrm.position;
        instantiatedScore.transform.rotation = trnsfrm.rotation;
        playersInstantiated.Add(instantiatedScore);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SpawnPlayer(Vector3 positionToSpawn)
    {
        instantiatedPlayer = Instantiate(playerPrefab, Camera.main.ScreenToWorldPoint(positionToSpawn), Quaternion.identity);
    }

    public void StartPlayerTurn(int playerNumber)
    {
        UIManager.singleton.ChangeWord();
        foreach (Transform pl in playersInstantiated)
        {
            pl.gameObject.SetActive(true);
        }
        playersInstantiated[playerNumber].gameObject.SetActive(false);
    }

}
