using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    Transform playerTransform;

    private void Start()
    {
        var player = GameObject.FindWithTag("Player");
        if(player != null)
        {
            playerTransform = GameObject.FindWithTag("Player").transform;
        }
    }

    private void Update()
    {
        if(playerTransform != null)
        {
            //store current position of the camera
            Vector3 tmp = transform.position;

            //set the camera's position x to the player's position x
            tmp.x = playerTransform.position.x + 2;
            //tmp.y = playerTransform.position.y;

            //set back the position of the camera to the tmp variable
            transform.position = tmp;
        }
    }
}
