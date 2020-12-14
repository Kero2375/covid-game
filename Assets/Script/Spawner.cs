using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner {
    private GameObject obj;

    public Spawner(GameObject[] prefabs, string name = null) {
        int index = Random.Range(0, prefabs.Length);
        GameObject newObject = GameObject.Instantiate(prefabs[index]);
        if(name != null) {
            newObject.name = name;
        }
        obj = newObject;
    }

    public Spawner SetPosition(float x, float y, float z) {
        obj.transform.position = new Vector3(x, y, z);
        return this;
    }

    public Spawner Rotate(float x, float y, float z) {
        obj.transform.Rotate(x, y, z);
        return this;
    }

    public GameObject GetObject() {
        return obj;
    }
}