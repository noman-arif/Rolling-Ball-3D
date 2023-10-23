using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float speed = 200;                                                                       //speed for camera rotation
    public GameObject player;                                                                       //player reference

    // Update is called once per frame
    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");                                        //Keyboard input for horizontal Movement
        transform.Rotate(Vector3.up.normalized, horizontalInput * speed * Time.deltaTime);          //rotate Camera 

    }
    private void LateUpdate()
    {
        transform.position = player.transform.position;                                             // Move focal point with player
    }
}
