using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObjects : MonoBehaviour
{
    //Trigger Function to Destroy Box Fall from Platform
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Die"))                                         //if Trigger with Die 
        {
            Destroy(gameObject);                                                        //DestroyObject
        }
    }
}
