using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointHud : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI AddText;

    int score = 0;
    public void Add()
    {
        score++;
        AddText.text = score.ToString();
    }
    public void Subtract()
    {
        score--;
        AddText.text = score.ToString();
    }
}
