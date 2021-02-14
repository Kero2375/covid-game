using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerBackOff : GameManager {

    public GameObject[] rooms;

    override public void Start() {
        base.Start();

        Instantiate(rooms[Random.Range(0, rooms.Length)]);
    }

}
