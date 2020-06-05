using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBounce : MonoBehaviour
{
    public Animator platformAnim;
    
    private void Start()
    {
        platformAnim = GetComponent<Animator>();

    }
 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {

            platformAnim.SetTrigger("New Trigger");
        }
    }

}
