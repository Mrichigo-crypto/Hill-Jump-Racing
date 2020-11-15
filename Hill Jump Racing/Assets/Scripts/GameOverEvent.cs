using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class GameOverEvent : MonoBehaviour
{
    [SerializeField] private GameObject gameOver,car;
    
    private float rotation;
    private bool isFlipped;
     public static event Action OnGameOver;
    void Start()
    {
         gameOver.SetActive(false);
         isFlipped = false;     
    }

    void Update() 
    {
        rotation = car.transform.rotation.z;
        Debug.Log(rotation);
        if( isFlipped && (rotation >= 0.9f && rotation <= 1f))
        {
            gameOver.SetActive(true);
            
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Debug.Log(other.gameObject.tag);
        

        if(other.gameObject.tag == "Grounds")
        {
            isFlipped = true;           
           if(OnGameOver != null)
              OnGameOver();

              
        }
    }
}
