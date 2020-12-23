using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawnManager : MonoBehaviour{

    public GameObject[] peoplePrefabs;
    
    public GameManagerMask spawnManager;

    private Spawner spawner = new Spawner();
    private float[] lanes = { -6F, -3F, 0F, 3F, +6F };

    private float rate = 3F;
    private float distanceFromPlayer = 70F;

    private int lastNumber = 0;
    //Altezza del suolo y = 0.6
    //Posizione player = 0 0 0 
    //cordinata x centro lane -6 -3 0 3 +6

    void Start(){
       InvokeRepeating("Spawn", 0, rate);
    }
    private void Spawn() {
        //Number of people to spawn
        float numberPeople = Random.Range(0, 6);
        float offset = 0F;
        if(numberPeople == 5) {
            for(int i = 0; i < 5; i++) {
                offset = Random.Range(-8F, 8F);
                spawner.Spawn(peoplePrefabs).SetPosition(lanes[i], 0.6F, distanceFromPlayer + offset);
            }
        } else {
            for (int i = 0; i < numberPeople; i++) {
                //Offset per far in modo che tutti i personaggi non siano allineati perfettamente
                offset = Random.Range(-8F, 8F);       
                float randLane = lanes[GetRandom(0, 5)];
                spawner
                    .Spawn(peoplePrefabs)
                    .SetPosition(randLane, 0.6F, distanceFromPlayer + offset);
            }
        }          
    }

    private int GetRandom(int min,int max) {
        int rand = Random.Range(min, max);
        while(rand == lastNumber) {
            rand = Random.Range(min, max);
        }
        lastNumber = rand;
        return rand;
    }
    
}
