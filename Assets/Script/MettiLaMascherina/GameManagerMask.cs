using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerMask : GameManager {

    public PeopleSpawnManager peopleSpawnManager = null;
    public AudioClip putMaskSound;

    public override void Update() {
        base.Update();

        //Non fare niente se in pausa
        Pause pausePanel = GameObject.Find("Pause").GetComponent<Pause>();
        if (pausePanel && pausePanel.paused)
            return;

        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0)) {
       
            //Input Mouse ritorna un Vector3, prendo quindi x e y
            Vector2 pos = Input.touchCount > 0 ? Input.GetTouch(0).position : new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Ray raycast = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;

            if (Physics.Raycast(raycast, out hit)) {
                if (hit.collider.CompareTag("Person")) {
                    //Controllo se il personaggio ha la maschera o no
                    if (!hit.collider.GetComponent<PeopleMovement>().hasMask()) {
                        //Prendo la skin del personaggio
                        Material m = hit.collider.GetComponentInChildren<SkinnedMeshRenderer>().material;
                        //Metto la maschera al personaggio
                        SaveData.ApplyMask(ref m);
                        hit.collider.GetComponent<PeopleMovement>().putMask();
                        playPutMask();
                    } else {                     
                        DecreaseLifes();
                    }
                }
            }
        }
    }

    public void CheckPoints() {
        if (base.GetPoints() % 20 == 0 && base.GetPoints() != 0) {
            peopleSpawnManager.increaseSpeedPeople();
        }
    }

    public void playPutMask() {
        if (soundOn) {
            AudioSource.PlayClipAtPoint(putMaskSound, GameObject.Find("Main Camera").transform.position, 0.5F);
        }
    }

}
