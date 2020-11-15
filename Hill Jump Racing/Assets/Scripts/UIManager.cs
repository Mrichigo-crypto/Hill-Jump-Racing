using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI distanceText, highScoreText, distanceCrossedText;
    private int scoreSoFar;
    void Start() 
    {
        highScoreText.text = "High Score: " + PlayerPrefs.GetInt("High Score:", 0).ToString();
        
    }
    void OnEnable() 
    {
        CarDrive.OnDriving += UpdateDistance;
        GameOverEvent.OnGameOver += IsGameOver;
    }

    void OnDisable() 
    {
        CarDrive.OnDriving -= UpdateDistance;
        GameOverEvent.OnGameOver -= IsGameOver;
    }

    void UpdateDistance(int value)
    {
        scoreSoFar = value;
        distanceText.SetText("Distance: " + value +" m");
    }

    void IsGameOver()
    {
         distanceCrossedText.SetText("Distance Crossed: " + scoreSoFar +" m");
         if(scoreSoFar > PlayerPrefs.GetInt("High Score:", 0))
          {
            PlayerPrefs.SetInt("High Score:" , scoreSoFar);
            highScoreText.SetText("Highest Distance : " + scoreSoFar +" m");
          }
    }
}
