using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlatformGenerator : MonoBehaviour
{
    
    public List<GameObject> levelObjects;
    public List<Transform> anchorSpawnPoints;
    public List<Transform> platformSpawnPoints;
    
    public enum Platforms
    {
        Anchor = 0,
        Mushroom = 1,

    }

    // Start is called before the first frame update

    void Start()
    {
        GeneratePlatforms(2, anchorSpawnPoints, Platforms.Anchor);
        GeneratePlatforms(1, platformSpawnPoints, Platforms.Mushroom);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GeneratePlatforms(int objectsCount, List<Transform> spawnPoints, Platforms platformType)
    {
        for (int i = 0; i < objectsCount; i++)
        {
            var randomSpawnPoint = Random.Range(0, spawnPoints.Count);
            var randomObject = Random.Range(0, levelObjects.Count);
            int platform = (int)(object)platformType;
            Instantiate(levelObjects[platform], spawnPoints[randomSpawnPoint].transform.position, Quaternion.identity);
            spawnPoints.RemoveAt(randomSpawnPoint);
        }
    }




}
