using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawnManager : MonoBehaviour{

    public GameObject[] peoplePrefabs;
    
    public GameManagerMask spawnManager;

    private Spawner spawner = new Spawner();
    private float[] lanes = { -6F, -3F, 0F, 3F, +6F };


    private float rate = 2;

    //Altezza del suolo y = 0.6
    //Posizione player = 0 0 0 
    //cordinata x centro lane -6 -3 0 3 +6

    void Start(){
        /*float x = -6F;
        for(int i = 0; i < 5; i++) {
            spawner.Spawn(peoplePrefabs).SetPosition(x, 0.6F, 10F).Rotate(0,180,0);
            x += 3;
        }*/
        InvokeRepeating("Spawn", 0, rate);

    }

    // Update is called once per frame
    void Update(){
        /*if(people.Count > 0 && people[0].transform.position.z < 6) {
            Destroy(people[0]);
            people.RemoveAt(0);
        }*/
    }

    private void Spawn() {
        float randLane = lanes[Random.Range(0, 5)];
        spawner
            .Spawn(peoplePrefabs)
            .SetPosition(randLane, 0.6F, 70F)
            .Rotate(0, 180, 0)
            .GetObject();
    }
}
