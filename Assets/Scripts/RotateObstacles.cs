using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateObstacles : MonoBehaviour
{
    public float rotationSpeed=5f;                                              //rotation Speed of an object
    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * rotationSpeed*Time.deltaTime);           //rotate object along Y axis 
    }
}
