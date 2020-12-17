using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Dir { None, Left, Right };
public class Movement : Swipable {
    //Parametri pubblici
    public float initSpeed = 10;
    public int jumpForceFactor = 300;

    readonly Lane lane = new Lane(1);
    Dir moving = Dir.None;
    private bool jumping = false;
    private float speed;

    void Start() {
        speed = initSpeed;
    }

    override protected void Update() {
        base.Update();
        Run();
    }

    //Corsa del giocatore
    private void Run() {
        transform.position += new Vector3(0, 0, speed) * Time.deltaTime;
    }

    //Controllo giocatore
    override protected void OnLeftSwipe() {
        if (moving == Dir.None && lane.ToLeft()) {
            //gameObject.transform.Rotate(new Vector3(0, -45, 0));
            transform.position = new Vector3(
                lane.GetLane(),
                transform.position.y,
                transform.position.z);
            //moving = Dir.Left;
            //dir = -1;
        }
    }
    override protected void OnRightSwipe() {
        if (moving == Dir.None && lane.ToRight()) {
            //gameObject.transform.Rotate(new Vector3(0, 45, 0));
            transform.position = new Vector3(
                lane.GetLane(),
                transform.position.y,
                transform.position.z);
            //moving = Dir.Right;
            //dir = 1;
        }
    }
    override protected void OnUpSwipe() {
        if (!jumping)
            Jump();
    }

    private void Jump() {
        jumping = true;
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForceFactor, 0));
        gameObject.GetComponent<Animator>().SetBool("jumping", jumping);
    }

    //Controllo del movimento
    /*IEnumerator StopPlayerOnLane() {
        yield return new WaitForSecondsRealtime(.3F);
        gameObject.transform.rotation = new Quaternion();
        moving = Dir.None;
        dir = 0;

        float posX = gameObject.transform.position.x;
        //Se il giocatore si muove verso dx/sx e raggiunge la lane corretta
        if (moving == Dir.Right && posX >= lane.GetLane()) {
            //Resetto la rotazione e l'indicatore della direzione
            gameObject.transform.rotation = new Quaternion();
            moving = Dir.None;
            dir = 0;
        } else if (moving == Dir.Left && posX <= lane.GetLane()) {
            gameObject.transform.rotation = new Quaternion();
            moving = Dir.None;
            dir = 0;
        }
    }*/

    //Collisione con la strada
    void OnCollisionEnter(Collision collision) {
        if (collision.collider.name.Contains("Road")) {
            //Interrompe il salto
            jumping = false;
            gameObject.GetComponent<Animator>().SetBool("jumping", jumping);
        }
    }

    public void IncreaseSpeed() {
        speed += 0.5F;
    }
}
