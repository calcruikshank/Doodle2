using Gameboard.Examples;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //this is the most frusturating script. there is a reason why every variable has gamemanager.singleton before it. 
    [SerializeField] Camera DrawCamera;
    [SerializeField] Camera UICamera;
    public int lastSortingOrder;

    [SerializeField] Transform clearScreenMesh;

    public Color lineColor;
    public static GameManager singleton;

    public Vector3 positionToSpawnUI;
    public Canvas mainCanvas;

    [SerializeField] Transform redUI;
    [SerializeField] Transform blackUI;
    [SerializeField] Transform brownUI;
    [SerializeField] Transform orangeUI;
    [SerializeField] Transform yellowUI;
    [SerializeField] Transform greenUI;
    [SerializeField] Transform lightBlueUI;
    [SerializeField] Transform darkBlueUI;
    [SerializeField] Transform purpleUI;
    [SerializeField] Transform pinkUI;
    [SerializeField] Transform grayUI;
    [SerializeField] Transform whiteUI;


    public Vector3 positionToSpawn;
    Vector3 bottomRightSpawn;
    Vector3 topRightSpawn;
    Vector3 topRotation;
    public Vector3 positionToSpawn2 = new Vector3();


   public  Vector3 eulerAnglesToSet;
    Transform lastColorPicked;
    public List<PlayerPresenceDrawer> playersInScene;
    // Start is called before the first frame update
    void Start()
    {
        if (singleton != null)
        {
            Destroy(this);
        }
        singleton = this;
        StartCoroutine(WaitForFrameStart());
        bottomRightSpawn = new Vector3(1400, 150, 0);
        topRightSpawn = new Vector3(1400, 1770, 0);
        topRotation = new Vector3(180, 180, 0);
        positionToSpawn = topRightSpawn;
        eulerAnglesToSet = topRotation;
        mainCanvas = FindObjectOfType<Canvas>();
        SetColorBlack();
        GameManager.singleton.EndTurn();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ClearScreenWhite()
    {
        GameManager.singleton.clearScreenMesh.GetComponent<MeshRenderer>().sortingOrder = lastSortingOrder;
        GameManager.singleton.clearScreenMesh.gameObject.SetActive(true);
        GameManager.singleton.StartCoroutine(WaitForFrame());
        GameManager.singleton.lastSortingOrder++;
    }
    IEnumerator WaitForFrame()
    {

        //returning 0 will make it wait 1 frame
        yield return new WaitForEndOfFrame();


        //code goes here

        GameManager.singleton.clearScreenMesh.gameObject.SetActive(false);
        ShowPlayerPrompt();

    }

    IEnumerator WaitForFrameStart()
    {
        GameManager.singleton.clearScreenMesh.gameObject.SetActive(true);

        //returning 0 will make it wait 1 frame
        yield return new WaitForSeconds(1f);


        //code goes here

        GameManager.singleton.clearScreenMesh.gameObject.SetActive(false);

    }

    public void SetColorRed()
    {
        GameManager.singleton.lineColor = new Color(.93f, .11f, .14f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        if (GameManager.singleton.lastColorPicked != null)
        {
            Destroy(GameManager.singleton.lastColorPicked.gameObject);
        }
        GameManager.singleton.lastColorPicked = Instantiate(redUI, mainCanvas.transform).GetComponent<RectTransform>();
        GameManager.singleton.lastColorPicked.position = GameManager.singleton.positionToSpawn;
        GameManager.singleton.lastColorPicked.localEulerAngles = GameManager.singleton.eulerAnglesToSet;
    }
    public void SetColorOrange()
    {
        GameManager.singleton.lineColor = new Color(.97f, .58f, .11f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        if (GameManager.singleton.lastColorPicked != null)
        {
            Destroy(GameManager.singleton.lastColorPicked.gameObject);
        }
        GameManager.singleton.lastColorPicked = Instantiate(orangeUI, mainCanvas.transform).GetComponent<RectTransform>();
        GameManager.singleton.lastColorPicked.position = GameManager.singleton.positionToSpawn;
        GameManager.singleton.lastColorPicked.localEulerAngles = GameManager.singleton.eulerAnglesToSet;
    }
    public void SetColorYellow()
    {
        GameManager.singleton.lineColor = new Color(1f, .95f, 0f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        if (GameManager.singleton.lastColorPicked != null)
        {
            Destroy(GameManager.singleton.lastColorPicked.gameObject);
        }
        GameManager.singleton.lastColorPicked = Instantiate(yellowUI, mainCanvas.transform).GetComponent<RectTransform>();
        GameManager.singleton.lastColorPicked.position = GameManager.singleton.positionToSpawn;
        GameManager.singleton.lastColorPicked.localEulerAngles = GameManager.singleton.eulerAnglesToSet;
    }
    public void SetColorGreen()
    {
        GameManager.singleton.lineColor = new Color(0f, .65f, .32f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        if (GameManager.singleton.lastColorPicked != null)
        {
            Destroy(GameManager.singleton.lastColorPicked.gameObject);
        }
        GameManager.singleton.lastColorPicked = Instantiate(greenUI, mainCanvas.transform).GetComponent<RectTransform>();
        GameManager.singleton.lastColorPicked.position = GameManager.singleton.positionToSpawn;
        GameManager.singleton.lastColorPicked.localEulerAngles = GameManager.singleton.eulerAnglesToSet;
    }
    public void SetColorLightBlue()
    {
        GameManager.singleton.lineColor = new Color(0f, .68f, .94f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        if (GameManager.singleton.lastColorPicked != null)
        {
            Destroy(GameManager.singleton.lastColorPicked.gameObject);
        }
        GameManager.singleton.lastColorPicked = Instantiate(lightBlueUI, mainCanvas.transform).GetComponent<RectTransform>();
        GameManager.singleton.lastColorPicked.position = GameManager.singleton.positionToSpawn;
        GameManager.singleton.lastColorPicked.localEulerAngles = GameManager.singleton.eulerAnglesToSet;
    }
    public void SetColorDarBlue()
    {
        GameManager.singleton.lineColor = new Color(.18f, .19f, .57f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        if (GameManager.singleton.lastColorPicked != null)
        {
            Destroy(GameManager.singleton.lastColorPicked.gameObject);
        }
        GameManager.singleton.lastColorPicked = Instantiate(darkBlueUI, mainCanvas.transform).GetComponent<RectTransform>();
        GameManager.singleton.lastColorPicked.position = GameManager.singleton.positionToSpawn;
        GameManager.singleton.lastColorPicked.localEulerAngles = GameManager.singleton.eulerAnglesToSet;
    }
    public void SetColorPurple()
    {
        GameManager.singleton.lineColor = new Color(.57f, .15f, .56f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        if (GameManager.singleton.lastColorPicked != null)
        {
            Destroy(GameManager.singleton.lastColorPicked.gameObject);
        }
        GameManager.singleton.lastColorPicked = Instantiate(purpleUI, mainCanvas.transform).GetComponent<RectTransform>();
        GameManager.singleton.lastColorPicked.position = GameManager.singleton.positionToSpawn;
        GameManager.singleton.lastColorPicked.localEulerAngles = GameManager.singleton.eulerAnglesToSet;
    }
    public void SetColorPink()
    {
        GameManager.singleton.lineColor = new Color(.94f, .31f, .53f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        if (GameManager.singleton.lastColorPicked != null)
        {
            Destroy(GameManager.singleton.lastColorPicked.gameObject);
        }
        GameManager.singleton.lastColorPicked = Instantiate(pinkUI, mainCanvas.transform).GetComponent<RectTransform>();
        GameManager.singleton.lastColorPicked.position = GameManager.singleton.positionToSpawn;
        GameManager.singleton.lastColorPicked.localEulerAngles = GameManager.singleton.eulerAnglesToSet;
    }
    public void SetColorBrown()
    {
        GameManager.singleton.lineColor = new Color(.46f, .30f, .16f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        if (GameManager.singleton.lastColorPicked != null)
        {
            Destroy(GameManager.singleton.lastColorPicked.gameObject);
        }
        GameManager.singleton.lastColorPicked = Instantiate(brownUI, mainCanvas.transform).GetComponent<RectTransform>();
        GameManager.singleton.lastColorPicked.position = GameManager.singleton.positionToSpawn;
        GameManager.singleton.lastColorPicked.localEulerAngles = GameManager.singleton.eulerAnglesToSet;
    }
    public void SetColorBlack()
    {
        GameManager.singleton.lineColor = Color.black; 
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        if (GameManager.singleton.lastColorPicked != null)
        {
            Destroy(GameManager.singleton.lastColorPicked.gameObject);
        }
        GameManager.singleton.lastColorPicked = Instantiate(blackUI, mainCanvas.transform).GetComponent<RectTransform>();
        GameManager.singleton.lastColorPicked.position = GameManager.singleton.positionToSpawn;
        GameManager.singleton.lastColorPicked.localEulerAngles = GameManager.singleton.eulerAnglesToSet;
    }
    public void SetColorGray()
    {
        GameManager.singleton.lineColor = new Color(.51f, .51f, .52f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        if (GameManager.singleton.lastColorPicked != null)
        {
            Destroy(GameManager.singleton.lastColorPicked.gameObject);
        }
        GameManager.singleton.lastColorPicked = Instantiate(grayUI, mainCanvas.transform).GetComponent<RectTransform>() ;
        GameManager.singleton.lastColorPicked.position = GameManager.singleton.positionToSpawn;
        GameManager.singleton.lastColorPicked.localEulerAngles = GameManager.singleton.eulerAnglesToSet;
    }
    public void SetColorWhite()
    {
        GameManager.singleton.lineColor = Color.white;
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        if (GameManager.singleton.lastColorPicked != null)
        {
            Destroy(GameManager.singleton.lastColorPicked.gameObject);
        }
        GameManager.singleton.lastColorPicked = Instantiate(whiteUI, mainCanvas.transform).GetComponent<RectTransform>();
        GameManager.singleton.lastColorPicked.position = GameManager.singleton.positionToSpawn;
        GameManager.singleton.lastColorPicked.localEulerAngles = GameManager.singleton.eulerAnglesToSet;
    }


    [SerializeField] Transform clearUIMesh;
    Transform inststiatedClearUIMesh;
    public void ClearUI()
    {
        GameManager.singleton.inststiatedClearUIMesh = Instantiate(GameManager.singleton.clearUIMesh, GameManager.singleton.mainCanvas.transform);
        PlayerManager.singleton.WaitForFrame(GameManager.singleton.inststiatedClearUIMesh);
        SetColorBlack();
    }

    public int playerTurn = 0;
    public void EndTurn()
    {
        SetPlayerTurn(GameManager.singleton.playerTurn);
        ClearUI();
        PlayerManager.singleton.StartPlayerTurn(GameManager.singleton.playerTurn);
        SetColorBlack();
        GameManager.singleton.playerTurn++;
        if (GameManager.singleton.playerTurn == PlayerManager.singleton.playersInstantiated.Count)
        {
            GameManager.singleton.playerTurn = 0;
        }
    }


    [SerializeField] Transform promptTextPrefab;
    public GameObject newPrompt;

    public void ShowPlayerPrompt()
    {
        if (GameManager.singleton.newPrompt != null)
        {
            Destroy(GameManager.singleton.newPrompt);
        }
        GameManager.singleton.newPrompt = Instantiate(promptTextPrefab.gameObject, mainCanvas.transform);
        GameManager.singleton.newPrompt.transform.position = GameManager.singleton.positionToSpawn;
        GameManager.singleton.newPrompt.transform.localEulerAngles = GameManager.singleton.eulerAnglesToSet;
        GameManager.singleton.newPrompt.GetComponentInChildren<TextMeshProUGUI>().text = UIManager.singleton.wordList.currentWord.ToString();
        GameManager.singleton.newPrompt.gameObject.SetActive(true);
    }
    public void HidePrompt()
    {
        Destroy(GameManager.singleton.newPrompt.gameObject);
        SetColorBlack();
        
    }

    public void SetPlayerTurn(int playerNumber)
    {
        GameManager.singleton.positionToSpawn = new Vector3( PlayerManager.singleton.playerScores[playerNumber].position.x, PlayerManager.singleton.playerScores[playerNumber].position.y, PlayerManager.singleton.playerScores[playerNumber].position.z);
        GameManager.singleton.eulerAnglesToSet = PlayerManager.singleton.playerScores[playerNumber].localEulerAngles;
        if (GameManager.singleton.eulerAnglesToSet == Vector3.zero)
        {
            GameManager.singleton.positionToSpawn = new Vector3(GameManager.singleton.positionToSpawn.x, GameManager.singleton.positionToSpawn.y + 100, GameManager.singleton.positionToSpawn.z);
        }
        if (GameManager.singleton.eulerAnglesToSet.z == 180)
        {
            GameManager.singleton.positionToSpawn = new Vector3(GameManager.singleton.positionToSpawn.x, GameManager.singleton.positionToSpawn.y - 100, GameManager.singleton.positionToSpawn.z);
        }
        if (GameManager.singleton.eulerAnglesToSet.z == 90)
        {
            GameManager.singleton.positionToSpawn = new Vector3(GameManager.singleton.positionToSpawn.x - 100, GameManager.singleton.positionToSpawn.y, GameManager.singleton.positionToSpawn.z);
        }
        if (GameManager.singleton.eulerAnglesToSet.z == 270)
        {
            GameManager.singleton.positionToSpawn = new Vector3(GameManager.singleton.positionToSpawn.x + 100, GameManager.singleton.positionToSpawn.y, GameManager.singleton.positionToSpawn.z);
        }
    }
}
