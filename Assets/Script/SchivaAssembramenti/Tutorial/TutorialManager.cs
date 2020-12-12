using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour {
    public SwipeManager swipeManager;
    public Transform player;
    public GameObject hand;
    private float speed = 10;
    public float lineX = 3.2F;

    bool swippedR = false;
    bool swippedL = false;
    bool swippedUp = false;

    void Update() {
        //Movimento
        player.Translate(new Vector3(0, 0, speed * Time.deltaTime));

        //Primo swipe verso destra
        if (player.position.z >= 40F && player.position.z <= 50F) { //Raggiunta la coordinata Z 40
            if (!swippedR) { //se non è stato effettuato lo swipe
                Time.timeScale = 0; //stop al tempo
                hand.SetActive(true); //attiva la grafica della mano

                //Animazione mano
                hand.transform.Rotate(new Vector3(0, 0, -.5F)); //inizia la rotazione della mano
                if (hand.transform.rotation.z <= -.3F) { //ruota fino a -0.3
                    hand.transform.rotation = new Quaternion(); //resetta la rotazione
                }
            }

            if (swipeManager.GetDirection() == SwipeManager.Direction.Right) { //quando si fa swipe
                //avvia il tempo
                Time.timeScale = 1;
                //resetta e nasconde la mano
                swippedR = true;
                hand.SetActive(false);
                hand.transform.rotation = new Quaternion();
                //ruota il giocatore
                player.Rotate(new Vector3(0, 45, 0));
            }

            //quando arriva alla coordinata della lane, raddrizza il player
            float posX = player.position.x;
            if (posX > lineX) {
                player.rotation = new Quaternion();
            }
        }

        //Primo swipe verso sinistra (analogo)
        if (player.position.z >= 80F && player.position.z <= 90F) {
            if (!swippedL) {
                Time.timeScale = 0;
                hand.SetActive(true);
                hand.transform.Rotate(new Vector3(0, 0, .5F));
                if (hand.transform.rotation.z >= .3F) {
                    hand.transform.rotation = new Quaternion();
                }
            }
            if (swipeManager.GetDirection() == SwipeManager.Direction.Left) {
                Time.timeScale = 1;
                player.Rotate(new Vector3(0, -45, 0));
                swippedL = true;
                hand.SetActive(false);
                hand.transform.rotation = new Quaternion();
            }
            float posX = player.position.x;
            float rotY = player.rotation.y;
            if (posX < 0 && rotY < 0F) {
                player.rotation = new Quaternion();
            }
        }

        //Swipe verso l'alto (salto)
        if (player.position.z >= 112F && player.position.z <= 122F) {
            if (!swippedUp) {
                Time.timeScale = 0;
                hand.SetActive(true);
                hand.transform.Translate(new Vector3(0, 1, 0));
                if (hand.GetComponent<RectTransform>().anchoredPosition.y >= 250F) {
                    hand.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                }
            }
            if (swipeManager.GetDirection() == SwipeManager.Direction.Up) {
                Time.timeScale = 1;
                swippedUp = true;
                hand.SetActive(false);
                hand.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 300, 0));
                player.GetComponent<Animator>().SetBool("jumping", true);
            }
        }


    }
}
