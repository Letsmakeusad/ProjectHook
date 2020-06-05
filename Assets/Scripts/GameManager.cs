using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject PlayerModel;
    public GameObject Player;
    public GameObject Menu;
    public Transform startingPoint;
    public CinemachineVirtualCamera vCam;
    public int nextLevelLoad;
    [SerializeField]
    private float coinCollected;
    
   

    // Start is called before the first frame update
    void Start()
    {
        vCam = FindObjectOfType<CinemachineVirtualCamera>();
        startingPoint = GameObject.FindGameObjectWithTag("startingPoint").GetComponent<Transform>();
        Player = Instantiate(Player, startingPoint.transform.position, Quaternion.identity, startingPoint);
        
        vCam.m_Follow = Player.transform;
        vCam.m_LookAt = Player.transform;

        nextLevelLoad = SceneManager.GetActiveScene().buildIndex + 1;
        coinCollected = 0;

    }
 
    public void StartLevel()
    {

        Vector2 v = Player.GetComponent<Rigidbody2D>().velocity;
        v.x = 0.0f;
        v.y = 0.0f;
        Player.GetComponent<Rigidbody2D>().velocity = v;
        Player.transform.position = startingPoint.position;
        Player.transform.rotation = Quaternion.identity;
        Player.GetComponent<Player>().isDead = false;
        Input.ResetInputAxes();
        
    }

    public void CompleteLevel()
    {
        // GET STATS ABOUT LEVEL
        // PAUSE PLAYER
        Debug.Log(nextLevelLoad);
        Player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0) * 0;
        //POP MENU
        Menu.SetActive(true);
        var level = FindObjectOfType<LevelGenerator>().level;
        var completeLevel = FindObjectOfType<LevelCompleteScript>();
        float starsToShow = coinCollected / level.maxStars;
         
        if(starsToShow == 0)
        {
            completeLevel.ShowStars(0);
        }    
        if(starsToShow <= 0.33f && starsToShow > 0)
        {
            completeLevel.ShowStars(1);
            if (level.starsCompleted < 1)
            {
                SetBestLevel(level, 1);
            }
        }
        else if(starsToShow >=0.33f && starsToShow <= 0.66)
        {
            completeLevel.ShowStars(2);
            if (level.starsCompleted < 2)
            {
                SetBestLevel(level, 2);
            }
        }
        else if (starsToShow > 0.66)
        {
            completeLevel.ShowStars(3);    
            SetBestLevel(level, 3);
           
        }

       

        if(nextLevelLoad > PlayerPrefs.GetInt("levelAt"))
        {
            PlayerPrefs.SetInt("levelAt", nextLevelLoad);       
        }
        
    }


    public void CollectCoin()
    {
        coinCollected++;
    }


    public void SetBestLevel(LevelStats level, int stars)
    {
        
        level.starsCompleted = stars;
    }


}
