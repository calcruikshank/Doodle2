using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Camera DrawCamera;
    [SerializeField] Camera UICamera;
    public int lastSortingOrder;

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
}
