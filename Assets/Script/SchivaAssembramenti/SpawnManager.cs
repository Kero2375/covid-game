﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour {
    public GameObject[] obstaclePrefabs;
    List<GameObject> obstacles = new List<GameObject>();
    public Transform playerTransform;

    public float minDistanceFromPlayer = 60; //minima distanza dello spawn dal player
    public float minDistanceFromPrefabs = 10;
    public float lastDistance;

    private float spawningRate = 3F;
    float timer;


    void Start() {
        lastDistance = 0;
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
    
    public void reduceSpawingRate() {
        if (spawningRate != 1) {
            spawningRate -= 0.5F;
        }
        Debug.Log("Reducing Spawining Rate, new value:" + spawningRate);
    }

    void Spawn() {
        int index = Random.Range(0, obstaclePrefabs.Length);
        GameObject newObstacle = Instantiate(obstaclePrefabs[index]);
        if (index == 2) {
            newObstacle.name = "Tombino";
        } else {
            newObstacle.name = "Assembramento";
        }
        
        float randomDistance = minDistanceFromPlayer + Random.Range(0, 30);

        float[] lanes = { -2.5F, 0F, 2.5F };
        float randomLane = lanes[Random.Range(0, 2)];

      
        
        Vector3 pos = new Vector3(
            randomLane,
            playerTransform.position.y,
            playerTransform.position.z + randomDistance);

        newObstacle.transform.position = pos;
        newObstacle.transform.Rotate(0, Random.Range(0, 360), 0);
        obstacles.Add(newObstacle);
        lastDistance = pos.z;
        
        Invoke("Spawn", spawningRate);
    }

}
