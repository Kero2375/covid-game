using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TutorialManagerMettiMascherina : MonoBehaviour{


    public GameObject popup;
    public GameObject hand;
    public GameObject firstPerson;
    public GameObject secondPerson;

    private bool hasTapped = false;
    private bool secondTap = false;
    private Text textPopup;
    private Vector3 positionPopup;

    // Start is called before the first frame update
    void Start() {
        textPopup = popup.transform.GetChild(0).GetComponent<Text>();
        positionPopup = popup.transform.position;
        StartCoroutine(TutorialText());
    }

    IEnumerator TutorialText() {
        textPopup.text = "Tutorial";
        popup.SetActive(true);
        yield return new WaitForSeconds(4);
        textPopup.text = "In questo gioco dovrai mettere le mascherine alle persone che vengono incontro";
        yield return new WaitForSeconds(4);
        popup.SetActive(false);
    }

    IEnumerator SetPopupTextAndWait(string s) {
        
        yield return new WaitForSeconds(4);     
    }
    // Update is called once per frame
    void Update(){
        if (firstPerson) {
            if (firstPerson.transform.position.z > 12 && firstPerson.transform.position.z < 20 && !hasTapped) {
                Time.timeScale = 0;
                textPopup.text = "Esegui un tap sul personaggio per mettere la mascherina.";
                //hand.transform.position = new Vector3(0, person.transform.position.y, 0);
                popup.transform.position = new Vector3(popup.transform.position.x, popup.transform.position.y - 100, popup.transform.position.z);
                popup.SetActive(true);
                hand.SetActive(true);
                hasTapped = true;
            }

            if (firstPerson.GetComponentInChildren<SkinnedMeshRenderer>().material.name.Contains("_mask") && hasTapped) {
                popup.SetActive(false);
                hand.SetActive(false);
                Time.timeScale = 1;
            }
        }

        if (!firstPerson && secondPerson) {
            if(secondPerson.transform.position.z > 40  && secondPerson.transform.position.z < 90) {
                popup.transform.position = positionPopup;
                textPopup.text = "Attenzione, non mettere una mascherina a chi la ha già, perderai una vita.";
                popup.SetActive(true);
            }

            if (secondPerson.transform.position.z > 20 && secondPerson.transform.position.z < 40) {
                textPopup.text = "Se un personaggio raggiunge la linea bianca senza mascherina, perderai una vita";
                popup.SetActive(true);
            }

            if (secondPerson.transform.position.z > 12 && secondPerson.transform.position.z < 20 && !secondTap) {
                popup.SetActive(false);
                Time.timeScale = 0;
                hand.SetActive(true);
                secondTap = true;
            }

            if (secondPerson.GetComponentInChildren<SkinnedMeshRenderer>().material.name.Contains("_mask") && secondTap) {
                popup.SetActive(false);
                hand.SetActive(false);
                Time.timeScale = 1;
            }
        }

        if(!firstPerson && !secondPerson) {
            textPopup.text = "Complimenti! Hai completato il tutorial";
            popup.SetActive(true);
            StartCoroutine(SetPopupTextAndWait());
            StartCoroutine(waitLoadScene());
        }
    }

    private IEnumerator waitLoadScene() {
        //Aspetto 4 secondi e carico la nuova scena
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene("MettiLaMascherina", LoadSceneMode.Single);
    }
}
