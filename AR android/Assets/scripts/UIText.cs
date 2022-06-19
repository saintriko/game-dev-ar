using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    private int count;
    public void SetCount(int countToChange)
    {
        count = countToChange;
    }
     void Start()
     {

     }

    void Update()
    {
        
    }

    void OnGUI()
    {
        GUIStyle myStyle = new GUIStyle();
        myStyle.normal.textColor = new Color(255,255,255);
        myStyle.fontSize = 80;
        myStyle.normal.background = Texture2D.grayTexture;
        GUI.Box(new Rect(10,10,340,100), "Score: " + count, myStyle);
    }
}
