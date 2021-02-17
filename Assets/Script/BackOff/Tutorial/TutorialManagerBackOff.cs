using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManagerBackOff : MonoBehaviour {

    public GameObject person;
    public GameObject hand;
    public GameObject[] fivePeople;


    public GameObject lastPerson;
    public GameObject popup;

    private Text textPopup;

    private bool hasSwipped = false;
    private bool secondSwipe = false;

    private bool spawnedSixPeople = false;
    float newYPos;

    private void Start() {
        person.SetActive(false);
        textPopup = popup.transform.GetChild(0).GetComponent<Text>();
        newYPos = popup.transform.position.y - 150F;
        StartCoroutine(TutorialText());

    }

    IEnumerator TutorialText() {
        textPopup.text = "Tieni fuori le persone dalla tua stanza, in modo da evitare assembramenti";
        popup.SetActive(true);
        yield return new WaitForSeconds(5);
        person.SetActive(true);
    }

    IEnumerator SixPeople() {
        textPopup.text = "Attenzione! La stanza può contenere al massimo 5 persone, dopo inizierai a perdere vite";
        yield return new WaitForSecondsRealtime(5);
        spawnedSixPeople = true;
    }

    IEnumerator WaitSomeSecond() {
        yield return new WaitForSecondsRealtime(2);
    }

    void Update() {
        if (person) {
            if (person.transform.position.z < 14F) {
                StartCoroutine(WaitSomeSecond());
                Time.timeScale = 0;
                person.GetComponent<PeopleGrabber>().enabled = true;
                person.GetComponent<PeopleGrabber>().SetMovable();

                textPopup.text = "Sposta le persone toccandole ed eseguendo uno swipe verso l'esterno della stanza";
                popup.SetActive(true);

                hand.SetActive(true);
                hand.transform.Translate(new Vector3(30, 80, 0) * Time.unscaledDeltaTime);
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
                foreach (GameObject person in fivePeople) {
                    if (!person) {
                        secondSwipe = true;
                    }
                }
                if (!secondSwipe) {
                    StartCoroutine(SixPeople());
                    if (spawnedSixPeople) {
                        popup.SetActive(false);
                        Time.timeScale = 1;
                        lastPerson.SetActive(true);
                        foreach (GameObject person in fivePeople) {
                            person.SetActive(true);
                        }
                        spawnedSixPeople = false;
                    }

                    if (lastPerson.transform.position.z < 14F) {
                        StartCoroutine(WaitSomeSecond());
                        lastPerson.GetComponent<BoxCollider>().enabled = true;
                        lastPerson.GetComponent<PeopleGrabber>().enabled = true;
                        lastPerson.GetComponent<PeopleGrabber>().SetMovable();
                        foreach (GameObject person in fivePeople) {
                            person.GetComponent<BoxCollider>().enabled = true;
                            person.GetComponent<PeopleGrabber>().enabled = true;
                            person.GetComponent<PeopleGrabber>().SetMovable();
                        }

                        hand.SetActive(true);
                        hand.transform.Translate(new Vector3(30, 80, 0) * Time.unscaledDeltaTime);
                        if (hand.GetComponent<RectTransform>().anchoredPosition.y >= 200F) {
                            hand.GetComponent<RectTransform>().anchoredPosition = new Vector3(100F, -30F, 0);
                        }
                    }
                }
            }
        } else {
            secondSwipe = true;
        }

        if (hasSwipped && secondSwipe) {
            hand.SetActive(false);
            StartCoroutine(WaitSomeSecond());
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