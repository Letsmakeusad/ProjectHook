using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveNextScene : MonoBehaviour
{
    public int nextSceneLoad; //MARKER
    public int selectedScene;

    public void Start()
    {
        nextSceneLoad = SceneManager.GetActiveScene().buildIndex + 1;
    }

    public void MoveToScene()
    {
        SceneManager.LoadScene(nextSceneLoad);
    }

    public void MoveToHome()
    {
        SceneManager.LoadScene(0);
    }

    public void MoveToSelectedScene(int num)
    {
        SceneManager.LoadScene(num);
    }


}
