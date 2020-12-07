using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public GameObject[] obstaclePrefabs;
    List<GameObject> obstacles = new List<GameObject>();
    public Transform playerTransform;

    public float minDistance = 60; //minima distanza dello spawn dal player
    float spawningRate = 3F;
    float timer;
    float timerIncrement = 20;

    void Start() {
        //timer = spawningRate;
        InvokeRepeating("Spawn", 0, spawningRate);
    }

    void Update() {
        //timer -= Time.deltaTime;
        //timerIncrement -= Time.deltaTime;

        //if(timer <= 0F) {
        //    Spawn();
        //    timer = spawningRate;
        //}

        //if(timerIncrement <= 0F && spawningRate > 1) {
        //    spawningRate -= 0.5F;
        //    timerIncrement = 20;
        //}
        
        if(obstacles.Count > 0 && 
            obstacles[0].transform.position.z < playerTransform.position.z - 20) {
            Destroy(obstacles[0]);
            obstacles.RemoveAt(0);
        }
    
    }

    void Spawn() {
        GameObject newObstacle = Instantiate(obstaclePrefabs[0]);
        newObstacle.name = "Assembramento";
        float randomDistance = minDistance + Random.Range(0, 30);
        float[] lanes = { -2.5F, 0F, 2.5F };
        float randomLane = lanes[Random.Range(0, 2)];

        Vector3 pos = new Vector3(
            randomLane,
            playerTransform.position.y,
            playerTransform.position.z + randomDistance);

        newObstacle.transform.position = pos;
        obstacles.Add(newObstacle);
    }
}
