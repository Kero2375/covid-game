using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerMask : GameManager {

    public PeopleSpawnManager peopleSpawnManager;
    public AudioClip putMaskSound;

    public override void Update() {
        base.Update();


        if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0)) {
            //Input Mouse ritorna un Vector3, prendo quindi x e y
            Vector2 pos = Input.touchCount > 0 ? Input.GetTouch(0).position : new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Ray raycast = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;

            if (Physics.Raycast(raycast, out hit)) {
                if (hit.collider.CompareTag("Person")) {
                    //Controllo se il personaggio ha la maschera o no
                    if (!hit.collider.GetComponent<PeopleMovement>().hasMask()) {
                        //Prendo il nome della skin del personaggio
                        string s = hit.collider.GetComponentInChildren<SkinnedMeshRenderer>().material.name.Split(' ')[0] + "_mask";
                        //Aggiungo _mask per andare a prendere la skin dello stesso personaggio però con la mascherina
                        Material newMat = Resources.Load<Material>(s);
                        //Imposto la nuova skin
                        hit.collider.GetComponentInChildren<SkinnedMeshRenderer>().material = newMat;
                        //Metto la maschera al personaggio
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
        AudioSource.PlayClipAtPoint(putMaskSound, GameObject.Find("Main Camera").transform.position, 0.5F);
    }

}
