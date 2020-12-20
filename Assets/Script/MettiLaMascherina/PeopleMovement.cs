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
                manager.Hit();
            Destroy(transform.root.gameObject);

        }
    }

    private void Run() {
        transform.position -= new Vector3(0, 0, speed) * Time.deltaTime;
    }

}
