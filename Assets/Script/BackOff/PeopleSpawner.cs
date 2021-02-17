using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawner : MonoBehaviour {

    public GameObject[] peoplePrefab;
    public MeshRenderer floor;

    private Spawner spawner = new Spawner();
    private float spawningRate = 2F;
    private float timer = 0;
    private float spawningTimer = 0;

    private void Update() {
        timer += Time.deltaTime;
        spawningTimer += Time.deltaTime;
        if(timer > spawningRate) { 
            Spawn();
            timer = 0;
        }
        //ogni 10 sec diminuisce spawningRate di 0.1, fino a 0.7
        if(spawningTimer > 10 && spawningRate > .7F) {
            spawningTimer = 0;
            spawningRate -= .1F;
        }
    }

    void Spawn() {
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(
            Random.Range(0,2),
            Random.value,
            0));
        plane.Raycast(ray, out float distance);
        Vector3 pos = ray.GetPoint(distance);

        spawner
            .Spawn(peoplePrefab, "Person")
            .SetPosition(
                pos.x,
                null,
                pos.z)
            .GetObject()
            .GetComponent<PeopleMovementBackOff>()
            .floor = this.floor;
    }
}
