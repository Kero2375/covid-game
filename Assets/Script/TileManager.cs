using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    public GameObject[] tilePrefabs;
    public Transform player;

    private List<GameObject> tiles = new List<GameObject>();

    void Start() {
        InitSpawn();
    }

    void Update() {
        if (player.position.z > tiles[tiles.Count - 2].transform.position.z) {
            Spawn();
        }
    }

    void Spawn() {
        GameObject newTile = Instantiate(tiles[tiles.Count - 1]);
        newTile.transform.Translate(0, 0, 60);
        newTile.name = "Road";
        tiles.Add(newTile);

        Destroy(tiles[0]);
        tiles.RemoveAt(0);
    }

    void InitSpawn() {
        //1
        GameObject newTile = Instantiate(tilePrefabs[0]);
        tiles.Add(newTile);

        //2
        GameObject newTile2 = Instantiate(tilePrefabs[0]);
        newTile2.transform.Translate(0, 0, 60);
        tiles.Add(newTile2);

        //3
        GameObject newTile3 = Instantiate(tilePrefabs[0]);
        newTile3.transform.Translate(0, 0, 120);
        tiles.Add(newTile3);

    }
}
