using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    public float moveSpeed = 2f;                                                                            //platform moving Speed;
    public GameObject[] objDestination;                                                                     //point where platform go
    private int indexVal = 0;                                                                                 //point index to determine the location
    void Update()
    {
        MovePlatforms();                                                                                    //call movePlatforms function per frame
    }
    private void MovePlatforms()
    {
        if (Vector3.Distance(transform.position, objDestination[indexVal].transform.position) < 0.1f)       //this condition calculate distance between platform and target point then check if distance is less than 0.1f if true
        {
            indexVal++;                                                                                     //then it increment index value mean now platform will have to move toward updated index
            if (indexVal >= objDestination.Length)                                                          //if index val is greater or equal to last array element the index will set to 0
            {
                indexVal = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, objDestination[indexVal].transform.position, moveSpeed * Time.deltaTime); //move platform toward the destination which is set according to index value
                                                                                                                                               //movetoward function is used in that case.
    }
}
