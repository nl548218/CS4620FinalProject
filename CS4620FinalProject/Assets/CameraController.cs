using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   public float sensitivity = 100f; 
    
    //public Transform head; 
    public Transform playa; 

    float xRotation = 0f; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float mouse_x = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouse_y = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        //If  += is used the rotation will be flipped 
        xRotation -= mouse_y;

        //We want to clamp x axis when looking up and do
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //rotates the camera along the y axis
        playa.Rotate(Vector3.up * mouse_x);

        //transform.position = head.position;
        //transform.rotation = head.rotation;
        
    }
}
