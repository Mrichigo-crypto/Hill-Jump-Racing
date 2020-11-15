using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroyer : MonoBehaviour
{
    
    void Update()
    {
        Destroy(this.gameObject, 5f);
    }
}
