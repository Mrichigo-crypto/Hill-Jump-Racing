using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScreenResolution : MonoBehaviour
{
    
    private bool pressedOnce = false;
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape) && !pressedOnce)
        {
            Screen.fullScreen =false;
            Debug.Log("Fullscreen: " + Screen.fullScreen);
            pressedOnce = true;
        }

        else if(Input.GetKeyDown(KeyCode.Escape) && pressedOnce)
        {
            Screen.fullScreen = true;
            Debug.Log("Fullscreen: " + Screen.fullScreen);
            pressedOnce = false;
        }
    }
}
