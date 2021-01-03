using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollisionCounter : MonoBehaviour {
    public GameManager gameManager;
    public GameObject warningTxt;
    int count = 0;
    float countdown = 11;
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
        /*if(count >= 6) {
            warningTxt.SetActive(true);
            countdown -= Time.deltaTime;
            warningTxt.GetComponent<Text>().text = "ATTENZIONE!\n" + (int)countdown;
        } else {
            warningTxt.SetActive(false);
            countdown = 11F;
        }*/
    }

    private void OnTriggerEnter(Collider other) {
        count++;
    }

    private void OnTriggerExit(Collider other) {
        count--;
    }

    IEnumerator DamageCroutine() {
        damaging = true;
        yield return new WaitForSeconds(5);
        gameManager.Hit();
        damaging = false;
    }


}
