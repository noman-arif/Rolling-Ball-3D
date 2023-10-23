using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public Transform[] fireLocation;                                                                  //Array store Transform for FireWork Position
    public ParticleSystem fireWorks;                                                                  //variabe Get Particles Prefab reference
    //Level Unlocking Function
    public void UnlockLevels()
    {
        if (SceneManager.GetActiveScene().buildIndex >= PlayerPrefs.GetInt("ReachedIndex"))           //if Current Scene Value is Greater than Scene Value Save in PlayerPrefs then      
        {
            if (SceneManager.GetActiveScene().buildIndex == 10)                                       //Check if Current Scene is the last Scene then
            {
                return;                                                                               //Do nothing
            }
            else                                                                                      //if not
            {
                PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("Unlocked", SceneManager.GetActiveScene().buildIndex + 1)); //then increment playerPref by 1 mean when you load again your completed level will keep unlock
                PlayerPrefs.Save();                                                                   //Save it
            }

        }
    }
    // Spawn Firework When Level is Complete
    public void FireWorks()
    {
        for (int i = 0; i < fireLocation.Length; i++)                                                    //condition to get firework position
        {
            Instantiate(fireWorks, fireLocation[i].transform.position, fireWorks.transform.rotation);   //spawn firework particle at the index position
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))                                                  //if player collide with finish line
        {
            UnlockLevels();                                                                             //then both of these function will call
            FireWorks();
        }
    }
}
