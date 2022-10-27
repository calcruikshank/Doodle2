using Gameboard.Examples;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Camera DrawCamera;
    [SerializeField] Camera UICamera;
    public int lastSortingOrder;

    [SerializeField] Transform clearScreenMesh;

    public Color lineColor;
    public static GameManager singleton;

    public Vector3 positionToSpawnUI;
   Canvas mainCanvas;

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


    public Vector3 positionToSpawn;
    Vector3 bottomRightSpawn;
    public Vector3 positionToSpawn2 = new Vector3();


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
        positionToSpawn = bottomRightSpawn;
        mainCanvas = FindObjectOfType<Canvas>();
        SetColorBlack();
        ClearUI();
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
        lastColorPicked = Instantiate(redUI, mainCanvas.transform).GetComponent<RectTransform>();
        lastColorPicked.position = GameManager.singleton.positionToSpawn;
    }
    public void SetColorOrange()
    {
        GameManager.singleton.lineColor = new Color(.97f, .58f, .11f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        lastColorPicked = Instantiate(orangeUI, mainCanvas.transform).GetComponent<RectTransform>();
        lastColorPicked.position = GameManager.singleton.positionToSpawn;
    }
    public void SetColorYellow()
    {
        GameManager.singleton.lineColor = new Color(1f, .95f, 0f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        lastColorPicked = Instantiate(yellowUI, mainCanvas.transform).GetComponent<RectTransform>();
        lastColorPicked.position = GameManager.singleton.positionToSpawn;
    }
    public void SetColorGreen()
    {
        GameManager.singleton.lineColor = new Color(0f, .65f, .32f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        lastColorPicked = Instantiate(greenUI, mainCanvas.transform).GetComponent<RectTransform>();
        lastColorPicked.position = GameManager.singleton.positionToSpawn;
    }
    public void SetColorLightBlue()
    {
        GameManager.singleton.lineColor = new Color(0f, .68f, .94f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        lastColorPicked = Instantiate(lightBlueUI, mainCanvas.transform).GetComponent<RectTransform>();
        lastColorPicked.position = GameManager.singleton.positionToSpawn;
    }
    public void SetColorDarBlue()
    {
        GameManager.singleton.lineColor = new Color(.18f, .19f, .57f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        lastColorPicked = Instantiate(darkBlueUI, mainCanvas.transform).GetComponent<RectTransform>();
        lastColorPicked.position = GameManager.singleton.positionToSpawn;
    }
    public void SetColorPurple()
    {
        GameManager.singleton.lineColor = new Color(.57f, .15f, .56f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        lastColorPicked = Instantiate(purpleUI, mainCanvas.transform).GetComponent<RectTransform>();
        lastColorPicked.position = GameManager.singleton.positionToSpawn;
    }
    public void SetColorPink()
    {
        GameManager.singleton.lineColor = new Color(.94f, .31f, .53f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        lastColorPicked = Instantiate(pinkUI, mainCanvas.transform).GetComponent<RectTransform>();
        lastColorPicked.position = GameManager.singleton.positionToSpawn;
    }
    public void SetColorBrown()
    {
        GameManager.singleton.lineColor = new Color(.46f, .30f, .16f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        lastColorPicked = Instantiate(brownUI, mainCanvas.transform).GetComponent<RectTransform>();
        lastColorPicked.position = GameManager.singleton.positionToSpawn;
    }
    public void SetColorBlack()
    {
        GameManager.singleton.lineColor = Color.black; 
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        lastColorPicked = Instantiate(blackUI, mainCanvas.transform).GetComponent<RectTransform>();
        lastColorPicked.position = GameManager.singleton.positionToSpawn;
    }
    public void SetColorGray()
    {
        GameManager.singleton.lineColor = new Color(.51f, .51f, .52f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        lastColorPicked = Instantiate(grayUI, mainCanvas.transform).GetComponent<RectTransform>() ;
        lastColorPicked.position = GameManager.singleton.positionToSpawn;
    }
    public void SetColorWhite()
    {
        GameManager.singleton.lineColor = Color.white;
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        lastColorPicked = Instantiate(blackUI, mainCanvas.transform).GetComponent<RectTransform>();
        lastColorPicked.position = GameManager.singleton.positionToSpawn;
    }


    [SerializeField] Transform clearUIMesh;
    public void ClearUI()
    {
        Transform clearMeshUI = Instantiate(clearUIMesh, GameManager.singleton.mainCanvas.transform);
        clearMeshUI.gameObject.SetActive(true);
        clearMeshUI.gameObject.SetActive(false);
        SetColorBlack();
    }

    public void EndTurn()
    {
        ClearUI();
    }


    [SerializeField] TextMeshProUGUI promptText;
    [SerializeField] Transform promptTextPrefab;
    public void ShowPlayerPrompt()
    {
        GameObject newPrompt = Instantiate(promptTextPrefab.gameObject, mainCanvas.transform);
        newPrompt.transform.position = positionToSpawn;
        newPrompt.GetComponentInChildren<TextMeshProUGUI>().text = UIManager.singleton.wordList.currentWord.ToString();
        newPrompt.gameObject.SetActive(true);
    }
    public void HidePrompt()
    {
        if (GameManager.singleton.lastColorPicked != null)
        {
            lastColorPicked = Instantiate(GameManager.singleton.lastColorPicked, mainCanvas.transform).GetComponent<RectTransform>();
        }
        if (lastColorPicked == null)
        {
            SetColorBlack();
        }
    }
}
