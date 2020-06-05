using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    public GameManager gameManager;
    public GameObject TryAgainMenu;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log(collision.gameObject.name);
            
            TryAgainMenu.SetActive(true);
        }
    }
    
    public void TryAgain()
    {
        TryAgainMenu.SetActive(false);
        gameManager.StartLevel();
    }
}
