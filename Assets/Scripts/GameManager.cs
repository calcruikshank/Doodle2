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
        lineColor = new Color(.93f, .11f, .14f);
    }
    public void SetColorYellow()
    {
        lineColor = new Color(.93f, .11f, .14f);
    }
    public void SetColorGreen()
    {
        lineColor = new Color(.93f, .11f, .14f);
    }
    public void SetColorLightBlue()
    {
        lineColor = new Color(.93f, .11f, .14f);
    }
    public void SetColorDarBlue()
    {
        lineColor = new Color(.93f, .11f, .14f);
    }
    public void SetColorPurple()
    {
        lineColor = new Color(.93f, .11f, .14f);
    }
    public void SetColorPink()
    {
        lineColor = new Color(.93f, .11f, .14f);
    }
    public void SetColorBrown()
    {
        lineColor = new Color(.93f, .11f, .14f);
    }
    public void SetColorBlack()
    {
        lineColor = Color.black;
    }
    public void SetColorGray()
    {
        lineColor = Color.black;
    }
    public void SetColorWhite()
    {
        lineColor = Color.white;
    }
}
