using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManagerBackOff : MonoBehaviour {

    public GameObject person;
    public GameObject hand;
    public GameObject[] sixPerson;


    public GameObject lastPerson;
    public GameObject popup;

    private Text textPopup;

    private bool hasSwipped = false;
    private bool secondSwip = false;

    private bool spawnedSevenPeople = false;
    float newYPos;

    private void Start() {
        person.SetActive(false);
        textPopup = popup.transform.GetChild(0).GetComponent<Text>();
        newYPos = popup.transform.position.y - 150F;
        StartCoroutine(TutorialText());

    }

    IEnumerator TutorialText() {
        textPopup.text = "Tutorial";
        popup.SetActive(true);
        yield return new WaitForSeconds(2);
        textPopup.text = "Lo scopo di questo gioco è quello di tenere fuori le persone dalla stanza, in modo da evitare assembramenti";
        yield return new WaitForSeconds(5);
        popup.SetActive(false);
        yield return new WaitForSeconds(2);
        person.SetActive(true);
    }

    IEnumerator SevenPerson() {
        textPopup.text = "Attenzione, la stanza può contenere al massimo 6 persone, inizierai a perdere vite se ci saranno più di 6 persone dentro la stanza";
        yield return new WaitForSecondsRealtime(5);
        spawnedSevenPeople = true;
    }

    IEnumerator WaitSomeSecond() {
        yield return new WaitForSecondsRealtime(2);
    }

    void Update() {
        if (person) {
            if (person.transform.position.z < 12F) {
                Time.timeScale = 0;
                person.GetComponent<BoxCollider>().enabled = true;

                textPopup.text = "Sposta il personaggio toccandolo ed eseguendo uno swipe verso l'esterno della stanza";
                popup.SetActive(true);

                popup.transform.position = new Vector3(popup.transform.position.x, newYPos, popup.transform.position.z);

                hand.SetActive(true);
                hand.transform.Translate(new Vector3(20, 80, 0) * Time.unscaledDeltaTime);
                if (hand.GetComponent<RectTransform>().anchoredPosition.y >= 250F) {
                    hand.GetComponent<RectTransform>().anchoredPosition = new Vector3(120F, 0, 0);
                }
            }
        } else {
            hasSwipped = true;
        }
        //I use last person because the variable person will be destroyed after the first part of the turorial
        if (lastPerson) {
            if (hasSwipped) {
                if (!secondSwip) {
                    StartCoroutine(SevenPerson());
                    if (spawnedSevenPeople) {
                        popup.SetActive(false);
                        Time.timeScale = 1;
                        lastPerson.SetActive(true);
                        
                        foreach (GameObject person in sixPerson) {
                            person.SetActive(true);
                        }
                        spawnedSevenPeople = false;
                    }

                    if (lastPerson.transform.position.z < 12F) {
                        lastPerson.GetComponent<BoxCollider>().enabled = true;
                        foreach (GameObject person in sixPerson) {
                            person.GetComponent<BoxCollider>().enabled = true;
                        }

                        hand.SetActive(true);
                        hand.transform.Translate(new Vector3(20, 80, 0) * Time.unscaledDeltaTime);
                        if (hand.GetComponent<RectTransform>().anchoredPosition.y >= 200F) {
                            hand.GetComponent<RectTransform>().anchoredPosition = new Vector3(100F, -30F, 0);
                        }
                                         
                        Time.timeScale = 0;
                    }
                }
            }
        } else {
            secondSwip = true;
        }
    
        if(hasSwipped && secondSwip) {
            StartCoroutine(WaitSomeSecond());
            textPopup.text = "Complimenti, hai completato il tutorial!";
            popup.SetActive(true);
            StartCoroutine(waitLoadScene());
        }
    }

    public void skipTutorial() {
        SceneManager.LoadScene("BackOff", LoadSceneMode.Single);
    }

    private IEnumerator waitLoadScene() {
        //Aspetto 4 secondi e carico la nuova scena
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("BackOff", LoadSceneMode.Single);
    }
}
