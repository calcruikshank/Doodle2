using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Camera DrawCamera;
    [SerializeField] Camera UICamera;
    public int lastSortingOrder;

    public Color lineColor;
    public static GameManager singleton;
    // Start is called before the first frame update
    void Start()
    {
        if (singleton != null)
        {
            Destroy(this);
        }
        singleton = this;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ClearDrawScreen()
    {
        DrawCamera.clearFlags = CameraClearFlags.Depth;
        StartCoroutine("UploadPNG");
    }
    IEnumerator UploadPNG()
    {
        // We should only read the screen buffer after rendering is complete
        yield return new WaitForEndOfFrame();

        Debug.Log("Clearing Draw Screen");
        DrawCamera.clearFlags = CameraClearFlags.Nothing;
    }

    public void SetColorRed()
    {
        lineColor = new Color(.93f, .11f, .14f);
    }
    public void SetColorOrange()
    {
        lineColor = new Color(.97f, .58f, .11f);
    }
    public void SetColorYellow()
    {
        lineColor = new Color(1f, .95f, 0f);
    }
    public void SetColorGreen()
    {
        lineColor = new Color(0f, .65f, .32f);
    }
    public void SetColorLightBlue()
    {
        lineColor = new Color(0f, .68f, .94f);
    }
    public void SetColorDarBlue()
    {
        lineColor = new Color(.18f, .19f, .57f);
    }
    public void SetColorPurple()
    {
        lineColor = new Color(.57f, .15f, .56f);
    }
    public void SetColorPink()
    {
        lineColor = new Color(.94f, .31f, .53f);
    }
    public void SetColorBrown()
    {
        lineColor = new Color(.46f, .30f, .16f);
    }
    public void SetColorBlack()
    {
        lineColor = Color.black;
    }
    public void SetColorGray()
    {
        lineColor = new Color(.51f, .51f, .52f);
    }
    public void SetColorWhite()
    {
        lineColor = Color.white;
    }
}
