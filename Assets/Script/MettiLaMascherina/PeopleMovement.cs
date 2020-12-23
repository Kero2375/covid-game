using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleMovement : MonoBehaviour{
   
    public float speed;
    private bool haveMask = false;
    private GameManagerMask manager;

    private void Start() {
        manager = GameObject.FindWithTag("GameManagerMask").GetComponent<GameManagerMask>();
    }

    void Update(){
        Run();
        if(transform.position.z < 6F){
            if (!haveMask)
                //Tolgo una vita
                manager.Hit();
            //Elimina l'oggetto a cui lo script è attaccato
            Destroy(transform.root.gameObject);
            manager.AddPoints();

        }


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
        
        if (other.CompareTag("Person")){
            Debug.Log("Collisione tra personaggi");
            Destroy(transform.root.gameObject);
        }
    }


}
