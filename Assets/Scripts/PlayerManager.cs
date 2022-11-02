using Gameboard;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] Transform playerPrefab;
    Transform instantiatedPlayer;

    [SerializeField] public List<Transform> playerScores;
    [SerializeField] Transform playerHud;

    public List<Transform> playersInstantiated = new List<Transform>();
    public List<Transform> playersCollidersInstantiated = new List<Transform>();

    public static PlayerManager singleton;


    private void Awake()
    {
        if (singleton != null)
        {
            Destroy(this);
        }
        singleton = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 8; i++)
        {
            SpawnPlayerScore(playerScores[i]);
            SpawnPlayer(playerScores[i]);
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

    float waitTimer;
    bool hasDestroyedAll = true;
    void Update()
    {
        waitTimer += Time.deltaTime;
        if (!hasDestroyedAll)
        {
            if (waitTimer > .1f)
            {
                if (GameObject.Find("ClearUIMesh(Clone)"))
                {
                    Destroy(GameObject.Find("ClearUIMesh(Clone)"));
                }
                else
                {
                    hasDestroyedAll = true;
                }
            }
        }
        
    }

    public void SpawnPlayer(Transform trnsfrm)
    {
        instantiatedPlayer = Instantiate(playerPrefab, Camera.main.ScreenToWorldPoint(trnsfrm.position), Quaternion.identity);
        instantiatedPlayer.rotation = trnsfrm.rotation;
        playersCollidersInstantiated.Add(instantiatedPlayer);
    }

    public void StartPlayerTurn(int playerNumber)
    {
        UIManager.singleton.ChangeWord();
        foreach (Transform pl in playersInstantiated)
        {
            pl.gameObject.SetActive(true);
            pl.SetAsLastSibling();
            
        }
        foreach (Transform plc in playersCollidersInstantiated)
        {
            plc.gameObject.SetActive(false);
            //plc.SetAsLastSibling();

        }
        if (playersInstantiated[playerNumber].gameObject != null)
        {
            playersInstantiated[playerNumber].gameObject.SetActive(false);
        }
        if (playersCollidersInstantiated[playerNumber].gameObject != null)
        {
            playersCollidersInstantiated[playerNumber].gameObject.SetActive(true);
        }
    }

    Transform disableTransformInAFrame;
    public void WaitForFrame(Transform sentTransform)
    {
        waitTimer = 0; hasDestroyedAll = false;
         disableTransformInAFrame = sentTransform;
        sentTransform.gameObject.SetActive(true);


    }

}
