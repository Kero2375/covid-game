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
        //Zona di sicurezza ai margini (per evitare che si blocchino fuori)
        Vector3 safeZone = new Vector3(1F, 0, 1F);

        //Calcolo la dimensione del pavimento (* scala)
        float xSize = floor.bounds.size.x * transform.lossyScale.x;
        float zSize = floor.bounds.size.z * transform.lossyScale.z;

        //Trovo una posizione random (relativa) all'interno
        Vector3 randomPositionInsideRect = new Vector3(
            (xSize - 2 * safeZone.x) * Random.value,
            0,
            (zSize - 2 * safeZone.z) * Random.value);

        //Calcolo la posizione (assoluta) a partire dalla posizione del floor
        Vector3 target = (floor.transform.position + safeZone) + randomPositionInsideRect;

        spawner
            .Spawn(coinPrefabs, "Coin")
            .SetPosition(target.x, target.y, target.z)
            .GetObject()
            .GetComponent<CoinBackOff>()
            .gameManager = this.gameManager;
            
    }
}
