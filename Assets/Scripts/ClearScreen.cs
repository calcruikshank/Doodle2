using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearScreen : MonoBehaviour
{
    public static ClearScreen singleton;


    private void Awake()
    {
        singleton = this;
    }
    

}
