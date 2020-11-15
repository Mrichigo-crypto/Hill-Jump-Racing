using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDestructor : MonoBehaviour
{
    [SerializeField] private GameObject car;
    
     void Start() 
    {
            
    }
    
    void Update()
    {
        car = GameObject.Find("Car 1");
        

       if(car.transform.position.x > this.transform.position.x + 260f)
          Destroy(this.gameObject);
    }
}
