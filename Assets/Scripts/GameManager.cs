using System.Collections;
using System.Collections.Generic;
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
        mainCanvas = FindObjectOfType<Canvas>();
        SetColorBlack();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ClearScreenWhite()
    {
        clearScreenMesh.GetComponent<MeshRenderer>().sortingOrder = lastSortingOrder;
        clearScreenMesh.gameObject.SetActive(true);
        StartCoroutine(WaitForFrame());
        lastSortingOrder++;
    }
    IEnumerator WaitForFrame()
    {

        //returning 0 will make it wait 1 frame
        yield return new WaitForEndOfFrame();


        //code goes here

        clearScreenMesh.gameObject.SetActive(false);

    }
    IEnumerator WaitForFrameStart()
    {
        clearScreenMesh.gameObject.SetActive(true);

        //returning 0 will make it wait 1 frame
        yield return new WaitForSeconds(1f);


        //code goes here

        clearScreenMesh.gameObject.SetActive(false);

    }

    public void SetColorRed()
    {
        lineColor = new Color(.93f, .11f, .14f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        Instantiate(redUI, mainCanvas.transform).GetComponent<RectTransform>().position = new Vector3(1400, 150, 0);
    }
    public void SetColorOrange()
    {
        lineColor = new Color(.97f, .58f, .11f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        Instantiate(orangeUI, mainCanvas.transform).GetComponent<RectTransform>().position = new Vector3(1400, 150, 0);
    }
    public void SetColorYellow()
    {
        lineColor = new Color(1f, .95f, 0f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        Instantiate(yellowUI, mainCanvas.transform).GetComponent<RectTransform>().position = new Vector3(1400, 150, 0);
    }
    public void SetColorGreen()
    {
        lineColor = new Color(0f, .65f, .32f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        Instantiate(greenUI, mainCanvas.transform).GetComponent<RectTransform>().position = new Vector3(1400, 150, 0);
    }
    public void SetColorLightBlue()
    {
        lineColor = new Color(0f, .68f, .94f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        Instantiate(lightBlueUI, mainCanvas.transform).GetComponent<RectTransform>().position = new Vector3(1400, 150, 0);
    }
    public void SetColorDarBlue()
    {
        lineColor = new Color(.18f, .19f, .57f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        Instantiate(darkBlueUI, mainCanvas.transform).GetComponent<RectTransform>().position = new Vector3(1400, 150, 0);
    }
    public void SetColorPurple()
    {
        lineColor = new Color(.57f, .15f, .56f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        Instantiate(purpleUI, mainCanvas.transform).GetComponent<RectTransform>().position = new Vector3(1400, 150, 0);
    }
    public void SetColorPink()
    {
        lineColor = new Color(.94f, .31f, .53f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        Instantiate(pinkUI, mainCanvas.transform).GetComponent<RectTransform>().position = new Vector3(1400, 150, 0);
    }
    public void SetColorBrown()
    {
        lineColor = new Color(.46f, .30f, .16f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        Instantiate(brownUI, mainCanvas.transform).GetComponent<RectTransform>().position = new Vector3(1400, 150, 0);
    }
    public void SetColorBlack()
    {
        lineColor = Color.black; 
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        Instantiate(blackUI, mainCanvas.transform).GetComponent<RectTransform>().position = new Vector3(1400, 150, 0);
    }
    public void SetColorGray()
    {
        lineColor = new Color(.51f, .51f, .52f);
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        Instantiate(grayUI, mainCanvas.transform).GetComponent<RectTransform>().position = new Vector3(1400, 150, 0);
    }
    public void SetColorWhite()
    {
        lineColor = Color.white;
        if (mainCanvas == null)
        {
            mainCanvas = FindObjectOfType<Canvas>();
        }
        Instantiate(blackUI, mainCanvas.transform).GetComponent<RectTransform>().position = new Vector3(1400, 150, 0);
    }
}
