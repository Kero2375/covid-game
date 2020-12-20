using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleSpawner : MonoBehaviour {

    public GameObject[] peoplePrefab;
    public Transform player;

    private Spawner spawner = new Spawner();

    void Start() {
        InvokeRepeating("Spawn", 0, 2);
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
            .Spawn(peoplePrefab)
            .SetPosition(
                pos.x,
                null,
                pos.z)
            .GetObject();
    }
}
