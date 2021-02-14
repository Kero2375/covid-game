using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManagerBackOff : MonoBehaviour{

    public GameObject[] coinPrefabs;
    public GameManagerBackOff gameManager;
    private Spawner spawner = new Spawner();

    public MeshRenderer floor;

    void Start(){
        InvokeRepeating("Spawn", 0, 5F);
    }

    void Spawn() {
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(
            Random.Range(0, 2),
            Random.value,
            0));

        plane.Raycast(ray, out float distance);

        Vector3 pos = ray.GetPoint(distance);

        spawner
            .Spawn(coinPrefabs, "Coin")
            .SetPosition(
                pos.x,
                null,
                pos.z)
            .GetObject()
            .GetComponent<PeopleMovements>()
            .floor = this.floor;

    }
}
