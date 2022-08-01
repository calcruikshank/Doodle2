using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRender : MonoBehaviour
{
    public RenderTexture renderTexture;

    void OnGUI()
    {
        GUI.depth = 5;
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), renderTexture, ScaleMode.ScaleToFit);
    }
}
