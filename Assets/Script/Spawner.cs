using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner {
    public class Object {
        private GameObject obj; 

        public Object(GameObject obj) {
            this.obj = obj;
        }

        public Object SetPosition(float x, float y, float z) {
            obj.transform.position = new Vector3(x, y, z);
            return this;
        }

        public Object Rotate(float x, float y, float z) {
            obj.transform.Rotate(x, y, z);
            return this;
        }
        public GameObject GetObject() {
            return obj;
        }
    }

    private Object spawned;

    public Object Spawn(GameObject[] prefabs, string name = null) {
        int index = Random.Range(0, prefabs.Length);
        GameObject newObject = GameObject.Instantiate(prefabs[index]);
        if(name != null) {
            newObject.name = name;
        }
        spawned = new Object(newObject);
        return spawned;
    }
}

