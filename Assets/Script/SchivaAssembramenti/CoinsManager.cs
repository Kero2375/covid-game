using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinsManager : MonoBehaviour {
    public GameObject[] coinPrefabs;
    public Transform playerTransform;
    public GameManagerSchiva gameManager;

    private Spawner spawner = new Spawner();

    // Start is called before the first frame update
    void Start() {
        InvokeRepeating("Spawn", 0, 5F);
    }

    void Spawn() {
        float[] lanes = {-3.2F, 0, 3.2F };
        float rndLane = lanes[Random.Range(0, 3)];

        for (int i=0; i<=Random.Range(0,10); i++) {
            spawner
            .Spawn(coinPrefabs, "Coin")
            .SetPosition(
                rndLane,
                0.9F,
                playerTransform.position.z + 60 + i*2)
            .Rotate(0, Random.Range(0,360), 0)
            .GetObject()
            .GetComponent<Coin>()
            .SetAttributes(playerTransform, gameManager);
        }

    }
}
