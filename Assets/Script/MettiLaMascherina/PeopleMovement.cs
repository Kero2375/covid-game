using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleMovement : MonoBehaviour{
   
    public float speed;
    public bool haveMask = false;
    private GameManagerMask manager;

    private void Start() {
        manager = GameObject.FindWithTag("GameManagerMask").GetComponent<GameManagerMask>();
        string s = transform.root.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.name;
        if (s.Contains("mask")) {
            haveMask = true;
        }

    }

    void Update(){
        Run();
    }

    private void Run() {
        transform.position -= new Vector3(0, 0, speed) * Time.deltaTime;
    }

    public bool hasMask() {
        return haveMask;
    }

    public void putMask() {
        if(!haveMask)
            haveMask = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Linea")) {  
            if (!haveMask)
                //Tolgo una vita
                manager.Hit();
            //Elimina l'oggetto a cui lo script è attaccato
            Destroy(transform.root.gameObject);
            manager.AddPoints();
        }
        
        if (other.CompareTag("Person")){
            Debug.Log("Collisione tra personaggi");
            Destroy(transform.root.gameObject);
        }

        
    }


}
