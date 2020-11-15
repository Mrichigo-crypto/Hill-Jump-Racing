
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGenerator : MonoBehaviour
{
    [SerializeField] private Transform startingLevelTemplate;
    [SerializeField] private List<GameObject> levelTemplates;
    [SerializeField] private Transform car;
    [SerializeField] private float xDistanceFromOneTemplateToAnother = 236f;

    private Vector3 lastEndPoint;
    private float distanceForSpawn = 100f;
     private GameObject generatedLevelPart;
    
    void Start()
    {
        lastEndPoint = startingLevelTemplate.position;
       
    }

    
    void Update()
    {
        
        if(Vector3.Distance(car.position, lastEndPoint) <= distanceForSpawn)
        {
            SpawnLevelPart();
        }
    }

    void SpawnLevelPart()
    {

       GameObject lastLevelPartPosition = SpwanLevelPart(lastEndPoint + new Vector3(xDistanceFromOneTemplateToAnother,0,0)); 
       lastEndPoint = lastLevelPartPosition.transform.position;

     
    
    }
    GameObject SpwanLevelPart(Vector3 spawnPosition)
    {
        generatedLevelPart = Instantiate(levelTemplates[Random.Range(0,levelTemplates.Count)], spawnPosition, Quaternion.identity);
       return generatedLevelPart;
    }
}
