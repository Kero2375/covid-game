using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManagerMaskOn : MonoBehaviour{

    public GameObject[] coinPrefabs;
    public GameManagerMask gameManager;
    private Spawner spawner = new Spawner();

    private float distanceFromPlayer = 30F;

    private float[] lanes = { -6F, -3F, 0F, 3F, +6F };

    private int lastNumber = 0;

    void Start(){
        InvokeRepeating("Spawn", 0, 3F);
    }
    
    void Spawn() {
        float randLane = lanes[GetRandom(0, 5)];
        float offset = Random.Range(-12F, 12F);
        spawner
           .Spawn(coinPrefabs, "Coin")
           .SetPosition(
               randLane,
               0.9F,
               distanceFromPlayer + offset);
    }

    private int GetRandom(int min, int max) {
        int rand = Random.Range(min, max);
        while (rand == lastNumber) {
            rand = Random.Range(min, max);
        }
        lastNumber = rand;
        return rand;
    }



}
