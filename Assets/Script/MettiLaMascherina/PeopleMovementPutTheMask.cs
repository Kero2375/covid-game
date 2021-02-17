using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleMovementPutTheMask : MonoBehaviour{
   
    public float speed;
    public bool haveMask = false;
    private GameManagerMask manager;

    private void Start() {
        manager = GameObject.FindWithTag("GameManagerMask").GetComponent<GameManagerMask>();
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
                manager.DecreaseLifes();
            //Elimina l'oggetto a cui lo script è attaccato
            Destroy(transform.root.gameObject);
            manager.AddPoints();
            manager.CheckPoints();
        }
        
        //Se due persone vengono tra di loro collidono, distruggo una delle due
        if (other.CompareTag("Person")){
            Destroy(transform.root.gameObject);
        }

        
    }


}
