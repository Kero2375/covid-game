using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawnManager : MonoBehaviour{

    public GameObject[] peoplePrefabs;

    private Spawner spawner = new Spawner();


    //Altezza del suolo y = 0.6
    //Posizione player = 0 0 0 
    //cordinata x centro lane -6 -3 0 3 +6
    void Start(){
        float x = -6F;
        for(int i = 0; i < 5; i++) {
            spawner.Spawn(peoplePrefabs).SetPosition(x, 0.6F, 10F).Rotate(0,180,0);
            x += 3;
        }
        
    }

    // Update is called once per frame
    void Update(){
        
    }
}
