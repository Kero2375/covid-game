using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum Dir { None, Left, Right };
public class Movement : Swipable {
    //Parametri pubblici
    public float speed = 10;
    public int jumpForceFactor = 300;
    public GameManagerSchiva gameManager;

    readonly Lane lane = new Lane(1);
    Dir moving = Dir.None;
    private bool jumping = false;

    override protected void Update() {
        base.Update();
        Run();
    }

    //Corsa del giocatore
    private void Run() {
        transform.position += new Vector3(0, 0, 10 * gameManager.GetSpeedFactor()) * Time.deltaTime;
    }

    //Controllo giocatore
    override protected void OnLeftSwipe() {
        if (!jumping && moving == Dir.None && lane.ToLeft()) {
            transform.position = new Vector3(
                lane.GetLane(),
                transform.position.y,
                transform.position.z);
        }
    }
    override protected void OnRightSwipe() {
        if (!jumping && moving == Dir.None && lane.ToRight()) {
            transform.position = new Vector3(
                lane.GetLane(),
                transform.position.y,
                transform.position.z);
        }
    }
    override protected void OnUpSwipe() {
        if (!jumping)
            gameManager.playJumpDone();
            Jump();
    }

    private void Jump() {
        jumping = true;
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0, jumpForceFactor, 0));
        gameObject.GetComponent<Animator>().SetBool("jumping", jumping);
    }

    //Collisione con la strada
    void OnCollisionEnter(Collision collision) {
        if (collision.collider.name.Contains("Road")) {
            //Interrompe il salto
            jumping = false;
            gameObject.GetComponent<Animator>().SetBool("jumping", jumping);
        }
    }
}
