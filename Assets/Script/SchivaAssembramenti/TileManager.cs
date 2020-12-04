using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour {
    public GameObject[] tilePrefabs;
    public Transform player;
    public float tileLenght = 60;

    private List<GameObject> tiles = new List<GameObject>();

    void Start() {
        for (int i = 0; i < 2; i++) {
            Spawn();
        }
    }

    void Update() {
        if (player.position.z > tiles[tiles.Count - 2].transform.position.z) {
            Spawn();
        }
    }

    void Spawn() {
        GameObject newTile;
        if (tiles.Count == 0) { 
            //Se non ho tiles, istanzio il primo (base)
            newTile = Instantiate(tilePrefabs[0]);
        } else { 
            //Altrimenti, copio il precedente
            newTile = Instantiate(tiles[tiles.Count - 1]);
            newTile.transform.Translate(0, 0, tileLenght); //sposto in avanti
        }
        newTile.name = "Road";
        tiles.Add(newTile); //aggiungo alla lista

        //Se ho più di 3 tiles, cancello il più vecchio
        if (tiles.Count > 3) {
            Destroy(tiles[0]);
            tiles.RemoveAt(0);
        }
    }
}
