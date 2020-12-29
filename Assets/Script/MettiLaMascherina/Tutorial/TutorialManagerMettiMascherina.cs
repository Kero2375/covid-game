using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManagerMettiMascherina : MonoBehaviour{


    public GameObject popup;
    public GameObject hand;
    public GameObject person;

    private bool hasTapped = false;
    private Text textPopup;

    // Start is called before the first frame update
    void Start() {
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

    // Update is called once per frame
    void Update(){
        if (person) {
            if (person.transform.position.z > 12 && person.transform.position.z < 20 && !hasTapped) {
                Time.timeScale = 0;
                textPopup.text = "Esegui un tap sul personaggio per mettere la mascherina.";
                //hand.transform.position = new Vector3(0, person.transform.position.y, 0);
                popup.transform.position = new Vector3(popup.transform.position.x, popup.transform.position.y - 100, popup.transform.position.z);
                popup.SetActive(true);
                hand.SetActive(true);
                hasTapped = true;
            }


            if (person.GetComponentInChildren<SkinnedMeshRenderer>().material.name.Contains("_mask") && hasTapped) {
                popup.SetActive(false);
                hand.SetActive(false);
                Time.timeScale = 1;
            }
        }
    }
}
