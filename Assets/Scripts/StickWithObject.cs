using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StickWithObject : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))                  //if player collide with the moving platform or rotating platform 
        {
            collision.gameObject.transform.SetParent(transform);        //then player become the child of the gameobject or platform
        }
    }
    private void OnCollisionExit(Collision collision)                   
    {
        if (collision.gameObject.CompareTag("Player"))                  //as long as player is on that platform 
        {
            collision.gameObject.transform.SetParent(null);             //when player move from the platform then it remove from the child of the gameobject
        }
    }
}
