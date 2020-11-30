using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour{

    public GameObject[] tilePrefabs;
    public float zSpawn = 0;
    private float tileLength = 23;
    private int numberOfTiles = 3;

    private List<GameObject> activeTiles = new List<GameObject>();

    public Transform playerTransform;

    void Start(){
        for(int i = 0; i < numberOfTiles; i++){
            SpawnTile(0);
        }
    }

    // Update is called once per frame
    void Update(){
        if(playerTransform.position.z - 23 > zSpawn - (numberOfTiles * tileLength)){
            SpawnTile(0);
            DeleteTile();
        }
    }

    private void DeleteTile(){
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    public void SpawnTile(int tileIndex){
        GameObject go = Instantiate(tilePrefabs[tileIndex], transform.forward * zSpawn , transform.rotation);
        activeTiles.Add(go);
        zSpawn += tileLength;
    }
}
