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
        //Questo lo faccio perchè, se prendo danno mentre Time.timescale = 0, non si vedrebbe il rosso
        //Nel momento in cui riparte il tempo, viene mostrato anche il rosso 
        GameObject.Find("Damage").GetComponent<Animator>().enabled = false;

        textPopup = popup.transform.GetChild(0).GetComponent<Text>();
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
                popup.SetActive(true);
                hand.SetActive(true);
                hasTapped = true;
            }

            if (firstPerson.GetComponentInChildren<SkinnedMeshRenderer>().material.GetTexture("_MaskTex").name == "mask" && hasTapped) {
                popup.SetActive(false);
                hand.SetActive(false);
                Time.timeScale = 1;
            }
        }

        if (!firstPerson && secondPerson) {
            if(secondPerson.transform.position.z > 40  && secondPerson.transform.position.z < 90) {
                textPopup.text = "Attenzione, non mettere una mascherina a chi la ha già, perderai una vita.";
                popup.SetActive(true);
            }

            if (secondPerson.transform.position.z > 20 && secondPerson.transform.position.z < 40) {
                textPopup.text = "Se un personaggio raggiunge la linea bianca senza mascherina, perderai una vita";
                popup.SetActive(true);
            }

            if (secondPerson.transform.position.z > 12 && secondPerson.transform.position.z < 20 && !secondTap) {
                Time.timeScale = 0;
                hand.SetActive(true);
                secondTap = true;
            }

            if (secondPerson.GetComponentInChildren<SkinnedMeshRenderer>().material.GetTexture("_MaskTex").name == "mask" && secondTap) {
                popup.SetActive(false);
                hand.SetActive(false);
                Time.timeScale = 1;
            }
        }

        if(!firstPerson && !secondPerson) {
            textPopup.text = "Complimenti! Hai completato il tutorial";
            popup.SetActive(true);
            StartCoroutine(waitLoadScene());
        }
    }

    private IEnumerator waitLoadScene() {
        //Aspetto 4 secondi e carico la nuova scena
        yield return new WaitForSeconds(2);
        SaveData.SetTutorial(SaveData.GAMES.MettiLaMascherina);
        SceneManager.LoadScene("MettiLaMascherina", LoadSceneMode.Single);
    }

    public void skipTutorial() {
        SaveData.SetTutorial(SaveData.GAMES.MettiLaMascherina);
        SceneManager.LoadScene("MettiLaMascherina", LoadSceneMode.Single);
    }
}
