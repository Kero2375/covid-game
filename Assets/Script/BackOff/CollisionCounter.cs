using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionCounter : MonoBehaviour {
    public GameManager gameManager;
    public GameObject warningTxt;
    int count = 0;
    int seconds = 5;
    bool damaging = false;

    private void Update() {
        //sostituire con chiamata alla classe GameManager
        if (count >= 6) {
            warningTxt.SetActive(true);
            warningTxt.GetComponent<Text>().text = "ATTENZIONE!\n" + count + " PERSONE";
            if (!damaging)
                StartCoroutine(DamageCroutine());
        } else {
            warningTxt.SetActive(false);
            StopAllCoroutines();
            damaging = false;
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name.Contains("Person"))
            count++;
    }

    private void OnTriggerExit(Collider other) {
        if(other.name.Contains("Person"))
            count--;
    }

    IEnumerator DamageCroutine() {
        damaging = true;
        yield return new WaitForSeconds(seconds);
        try {
            gameManager.DecreaseLifes();
        } catch { }
        damaging = false;
    }


}
