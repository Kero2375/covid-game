﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TilesManager : MonoBehaviour {
    public GameObject[] tilePrefabs;
    public Transform player;
    public float tileLenght = 60;
    public GameManagerSchiva gameManager;

    private List<GameObject> tiles = new List<GameObject>();
    private Spawner spawner = new Spawner();

    //private int counter = 10;
    void Start() {
        for (int i = 0; i < 2; i++) {
            Spawn();
        }
    }

    void Update() {
        if (player.position.z > tiles[tiles.Count - 2].transform.position.z) {
            Spawn();
        }

        //Se ho più di 3 tiles, cancello il più vecchio
        if (tiles.Count > 3) {
            Destroy(tiles[0]);
            tiles.RemoveAt(0);
            //Incrementa il punteggio
            gameManager.AddPoints();
            gameManager.IncreaseSpeed(.1F);
        }
    }

    void Spawn() {
        GameObject newTile;
        if (tiles.Count == 0) {
            //Se non ho tiles, istanzio il primo (base)
            newTile = Instantiate(tilePrefabs[0]);
        } else {
            //Altrimenti, copio uno a random
            Transform last = tiles[tiles.Count - 1].transform;
            newTile = spawner
                .Spawn(tilePrefabs)
                .SetPosition(
                    last.position.x,
                    last.position.y,
                    last.position.z + tileLenght)
                .GetObject();
        }
        newTile.name = "Road";
        tiles.Add(newTile); //aggiungo alla lista
    }
}
