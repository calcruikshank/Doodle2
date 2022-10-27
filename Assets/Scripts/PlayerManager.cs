using Gameboard;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Transform playerPrefab;
    Transform instantiatedPlayer;


    // Start is called before the first frame update
    void Start()
    {
        SpawnPlayer(new Vector3(1400, 150, 0));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPlayer(Vector3 positionToSpawn)
    {
        instantiatedPlayer = Instantiate(playerPrefab, Camera.main.ScreenToWorldPoint(positionToSpawn), Quaternion.identity);
        instantiatedPlayer.GetComponent<UserPresenceSceneObject>().SpawnInPointHUD(positionToSpawn);
    }
}
