using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManagerSchiva : MonoBehaviour {
    public SwipeManager swipeManager;
    public Transform player;
    public GameObject hand;
    public GameObject popup;
    public GameObject point;

    private float speed = 10;
    public float lineX = 3.2F;

    bool swippedR = false;
    bool swippedL = false;
    bool swippedUp = false;
    bool coinTaken = false;


    void Update() {
        //Movimento
        player.Translate(new Vector3(0, 0, speed * Time.deltaTime));

        //Faccio vedere il popup con scritto "tutorial"
        if(player.position.z <= 30F) {
            popup.transform.GetChild(0).GetComponent<Text>().text = "Tutorial";
            popup.SetActive(true);
        }

        //Tolgo il popup
        if(player.position.z >= 30F) {
            popup.SetActive(false);
        }


        //Primo swipe verso destra
        if (player.position.z >= 45F && player.position.z <= 50F) { //Raggiunta la coordinata Z 40
            if (!swippedR) { //se non è stato effettuato lo swipe
                Time.timeScale = 0; //stop al tempo
                hand.SetActive(true); //attiva la grafica della mano
                popup.SetActive(true);
                popup.transform.GetChild(0).GetComponent<Text>().text = "Esegui uno swipe verso destra per schivare l'assembramento";

                //Animazione mano
                hand.transform.Rotate(new Vector3(0, 0, -30 * Time.unscaledDeltaTime)); //inizia la rotazione della mano
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
                popup.SetActive(false);
                hand.transform.rotation = new Quaternion();
                        
                player.position = new Vector3(
                lineX,
                player.position.y,
                player.position.z);

            }      
        }

        //Primo swipe verso sinistra (analogo)
        if (player.position.z >= 85F && player.position.z <= 90F) {
            if (!swippedL) {
                Time.timeScale = 0;
                hand.SetActive(true);
                popup.SetActive(true);
                popup.transform.GetChild(0).GetComponent<Text>().text = "Esegui uno swipe verso sinistra per schivare l'assembramento";
                hand.transform.Rotate(new Vector3(0, 0, 30 * Time.unscaledDeltaTime));
                if (hand.transform.rotation.z >= .3F) {
                    hand.transform.rotation = new Quaternion();
                }
            }
            if (swipeManager.GetDirection() == SwipeManager.Direction.Left) {
                Time.timeScale = 1;
                swippedL = true;
                hand.SetActive(false);
                popup.SetActive(false);
                hand.transform.rotation = new Quaternion();
                
                player.position = new Vector3(
                0F,
                player.position.y,
                player.position.z);

            }
        }

        //Swipe verso l'alto (salto)
        if (player.position.z >= 112F && player.position.z <= 122F) {
            if (!swippedUp) {
                Time.timeScale = 0;
                hand.SetActive(true);           
                popup.SetActive(true);
                popup.transform.GetChild(0).GetComponent<Text>().text = "Esegui uno swipe verso l'alto per saltare il tombino";
                hand.transform.Translate(new Vector3(0, 80 * Time.unscaledDeltaTime, 0));
                if (hand.GetComponent<RectTransform>().anchoredPosition.y >= 250F) {
                    hand.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                }
            }
            if (swipeManager.GetDirection() == SwipeManager.Direction.Up) {
                Time.timeScale = 1;
                swippedUp = true;
                hand.SetActive(false);
                popup.SetActive(false);
                hand.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
                player.GetComponent<Rigidbody>().AddForce(new Vector3(0, 300, 0));
                player.GetComponent<Animator>().SetBool("jumping", true);
            }
        }

        if(player.transform.position.z >= 130 && player.transform.position.z <= 150) {
            popup.SetActive(true);
            popup.transform.GetChild(0).GetComponent<Text>().text = "Ogni volta che ti scontri con un ostacolo, perdi una vita, non arrivare mai a zero";
        }

        if (player.transform.position.z >= 150 && player.transform.position.z <= 170) {
            popup.SetActive(false);
        }

        if (player.transform.position.z >= 180) {
            point.GetComponent<Text>().text = "1";
        }

        if (player.transform.position.z >= 170 && player.transform.position.z <= 200) {
            popup.transform.GetChild(0).GetComponent<Text>().text = "Prendi più monete possibili, ti permettono di ottenere più punti";
            popup.SetActive(true);
            coinTaken = true;          
        }

        //Carico la scena del gioco vero e proprio
        if (swippedL && swippedR && swippedUp && coinTaken && player.transform.position.z >= 210) {
            popup.transform.GetChild(0).GetComponent<Text>().text = "Ottimo lavoro, hai completato il tutorial!";
            popup.SetActive(true);
            StartCoroutine(waitLoadScene());
        }
    }

    private IEnumerator waitLoadScene() {
        //Aspetto 4 secondi e carico la nuova scena
        yield return new WaitForSeconds(2);
        SaveData.SetTutorial(SaveData.GAMES.EvitaAssembramenti);
        SceneManager.LoadScene("SchivaAssembramenti", LoadSceneMode.Single);
    }

    public void skipTutorial() {
        SaveData.SetTutorial(SaveData.GAMES.EvitaAssembramenti);
        SceneManager.LoadScene("SchivaAssembramenti", LoadSceneMode.Single);
    }
 
}
