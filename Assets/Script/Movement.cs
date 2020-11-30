using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour{

    public float movementSpeed;
    public SwipeManager swipe;
    private int desiredLane = 1;

    void Start()    {
        
    }

    // Update is called once per frame
    void Update(){
        transform.position += transform.TransformDirection(Vector3.forward) * Time.fixedDeltaTime * 1.5f;

        /*if (swipe.SwipeUp || (Input.GetKey("w") && !Input.GetKey("s"))){
            transform.position += transform.TransformDirection(Vector3.forward) * movementSpeed * 6.5f;

        }else if (swipe.SwipeDown || (Input.GetKey("s") && !Input.GetKey("w"))){
            transform.position -= transform.TransformDirection(Vector3.forward) * movementSpeed * 6.5f;
        }*/

        if (swipe.SwipeLeft || (Input.GetKey("a") && !Input.GetKey("d"))){
            if (desiredLane != 0){
                transform.position += transform.TransformDirection(Vector3.left) * movementSpeed * 2.5f;
                desiredLane--;
            }

        }else if (swipe.SwipeRight || (Input.GetKey("a") && !Input.GetKey("d"))){
            if (desiredLane != 2){
                transform.position -= transform.TransformDirection(Vector3.left) * movementSpeed * 2.5f;
                desiredLane++;
            }
        }
    }
}
