using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteScript : MonoBehaviour
{
    public GameObject[] stars;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void ShowStars(int completedStars)
    {
        if(completedStars != 0)
        for (int i = 0; i < completedStars; i++)
        {
            stars[i].SetActive(true);
        }
    }
}
