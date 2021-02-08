using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManagerBackOff : MonoBehaviour {

    public GameObject person;
    public GameObject hand;
    public GameObject[] sixPeople;


    public GameObject lastPerson;
    public GameObject popup;

    private Text textPopup;

    private bool hasSwipped = false;
    private bool secondSwipe = false;

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
        person.GetComponent<PeopleGrabber>().locked = true;
    }

    IEnumerator SevenPeople() {
        textPopup.text = "Attenzione, la stanza può contenere al massimo 5 persone, inizierai a perdere vite se ci saranno più di 6 persone dentro la stanza";
        yield return new WaitForSecondsRealtime(5);
        spawnedSevenPeople = true;
    }

    IEnumerator WaitSeconds(int sec) {
        yield return new WaitForSecondsRealtime(sec);
    }

    void Update() {
        if (person) {
            if (person.transform.position.z < 14F) {
                StartCoroutine(WaitSeconds(2));
                person.GetComponent<PeopleGrabber>().locked = false;
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
        //I use last person because the variable person will be destroyed after the first part of tha turorial
        if (lastPerson) {
            if (hasSwipped && !secondSwipe) {
                if (!spawnedSevenPeople) {
                    StartCoroutine(SevenPeople());

                    lastPerson.SetActive(true);
                    foreach (GameObject person in sixPeople) {
                        person.GetComponent<PeopleGrabber>().locked = true;
                        person.SetActive(true);
                    }
                    lastPerson.GetComponent<PeopleGrabber>().locked = true;
                } else {
                    popup.SetActive(false);
                    Time.timeScale = 1;
                }

                if (lastPerson.transform.position.z < 14F) {
                    StartCoroutine(WaitSeconds(2));
                    hand.SetActive(true);
                    lastPerson.GetComponent<PeopleGrabber>().locked = false;

                    hand.transform.Translate(new Vector3(20, 80, 0) * Time.unscaledDeltaTime);
                    if (hand.GetComponent<RectTransform>().anchoredPosition.y >= 200F) {
                        hand.GetComponent<RectTransform>().anchoredPosition = new Vector3(100F, -30F, 0);
                    }

                    Time.timeScale = 0;
                }
            }
        } else {
            secondSwipe = true;
        }

        if (hasSwipped && secondSwipe) {
            StartCoroutine(WaitSeconds(2));
            textPopup.text = "Complimenti, hai completato il tutorial!";
            popup.SetActive(true);
            StartCoroutine(waitLoadScene());
        }
    }

    public void skipTutorial() {
        SaveData.SetTutorial(SaveData.GAMES.FuoriDiQua);
        SceneManager.LoadScene("BackOff", LoadSceneMode.Single);
    }

    private IEnumerator waitLoadScene() {
        //Aspetto 4 secondi e carico la nuova scena
        yield return new WaitForSeconds(2);
        SaveData.SetTutorial(SaveData.GAMES.FuoriDiQua);
        SceneManager.LoadScene("BackOff", LoadSceneMode.Single);
    }
}
