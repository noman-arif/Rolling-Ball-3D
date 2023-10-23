using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{
    private Rigidbody enemyRb;                                                                          //variable for RigidBody
    public float enemySpeed = 5;                                                                          //EnemySpeed
    private PlayerController player;                                                                    //Player Controller Script variable
    public bool isSpawn = false;                                                                          //isSpawm bool
    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();                                                            //get RigidBody Component
        player = GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>();      //Find PlayerController to access it function
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;            //determine distance and normalized it smooth it
        enemyRb.AddForce(lookDirection * enemySpeed);                                                   //applay force 
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Die"))                                                         //if Trigger with Die
        {
            Destroy(gameObject);                                                                        //Destroy Enemy
        }

    }
}
