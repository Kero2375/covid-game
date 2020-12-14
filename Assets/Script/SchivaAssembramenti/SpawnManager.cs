using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public GameObject[] obstaclePrefabs;
    List<GameObject> obstacles = new List<GameObject>();
    public Transform playerTransform;

    public float distanceFromPlayer = 60; //distanza dello spawn dal player
    public float lineX;

    private float spawningRate = 3F;

    void Start() {
        //InvokeRepeating("Spawn", 0, spawningRate);
        Invoke("Spawn", spawningRate);
    }

    void Update() {
        if(obstacles.Count > 0 && 
            obstacles[0].transform.position.z < playerTransform.position.z - 20) {
            Destroy(obstacles[0]);
            obstacles.RemoveAt(0);
        }
    }
    
    /*public void reduceSpawingRate() {
        if (spawningRate != 1) {
            spawningRate -= 0.5F;
        }
        Debug.Log("Reducing Spawining Rate, new value:" + spawningRate);
    }*/

    void Spawn() {
        float[] lanes = { -lineX, 0F, lineX };
        int lastIndex = -1;

        for(int i=0; i<=Random.Range(0,2); i++) {
            //Seleziona la lane (diversa dalla precedente)
            int laneIndex;
            do {
                laneIndex = Random.Range(0, 2);   
            } while (laneIndex == lastIndex);
            lastIndex = laneIndex;

            //Spawna l'oggetto
            GameObject newObj = new Spawner(obstaclePrefabs, "Ostacolo")
                .SetPosition(
                    lanes[laneIndex],
                    playerTransform.position.y,
                    playerTransform.position.z + distanceFromPlayer + Random.Range(-10, 10))
                .Rotate(0, Random.Range(0, 360), 0)
                .GetObject();
                    
            obstacles.Add(newObj); 
        }
        
        Invoke("Spawn", spawningRate);
    }

}
