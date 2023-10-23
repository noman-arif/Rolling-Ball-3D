using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;                                                          //Enemy Prefab Variable to store that reference
    public Transform[] spawnPos;                                                            //array of spawn Pos of enemy 
    //Function for Spawn Enemy
    public void SpawnEnemy()
    {
        for (int i = 0; i < spawnPos.Length; i++)                                          //loop to get position of enemy transform where it spawn
        {
            Instantiate(enemyPrefab, spawnPos[i].position, enemyPrefab.transform.rotation); //spawn enemy prefab in the transform location of an array index
        }
    }

}
