using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float initSpeed;
    public float lineX;
    public SwipeManager swipeManager;
    
    int lane = 1;

    private float speed;
    private bool moving = false;
    private bool jumping = false;

    void Start() {
        speed = initSpeed;
    }

    void Update() {
        gameObject.transform.Translate(new Vector3(0, 0, speed * Time.deltaTime));
        SwipeManager.Direction dir = swipeManager.GetDirection();

        if(!moving)
            switch (dir) {
                case SwipeManager.Direction.Left:
                    if(lane != 0) {
                        gameObject.transform.Rotate(new Vector3(0, -45, 0));
                        moving = true;
                        lane--;
                    }
                    break;
                case SwipeManager.Direction.Right:
                    if(lane != 2) {
                        gameObject.transform.Rotate(new Vector3(0, 45, 0));
                        moving = true;
                        lane++;
                    }
                    break;
                case SwipeManager.Direction.Up:
                    if(!jumping)
                        Jump();
                    break;
            }

        float posX = gameObject.transform.position.x;
        float rotY = gameObject.transform.rotation.y;
        //Se sto andando alla lane n e ci sono arrivato (pos </> qualcosa)
        if ((lane == 0 && posX < -lineX) ||
            (lane == 1 && (
                (posX < 0 && rotY < 0F) ||  //sto arrivando da destra
                (posX > 0 && rotY > 0F))) || //sto arrivando da sinistra
            (lane == 2 && posX > lineX)) {
            //Ferma il movimento
            gameObject.transform.rotation = new Quaternion(0, 0, 0, 0);
            moving = false;
        }
    }

    public void Jump() {
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, 300, 0));
        jumping = true;
        gameObject.GetComponent<Animator>().SetBool("jumping", jumping);
    }

    void OnCollisionEnter(Collision collision) {
        if(collision.collider.name.Contains("Road")) {
            jumping = false;
            gameObject.GetComponent<Animator>().SetBool("jumping", jumping);
        }
    }

    public void increaseSpeed() {
        speed += 0.2F;
    }

    
}
