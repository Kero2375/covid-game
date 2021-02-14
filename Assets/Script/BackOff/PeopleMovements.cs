using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleMovements : MonoBehaviour {
    public float direction = 1F;
    public MeshRenderer floor;

    Vector3 target;

    private void Start() {
        Material mat = gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material;
        SaveData.ApplyMask(ref mat);

        //Zona di sicurezza ai margini (per evitare che si blocchino fuori)
        Vector3 safeZone = new Vector3(1F, 0, 1F);

        //Calcolo la dimensione del pavimento (* scala)
        float xSize = floor.bounds.size.x * transform.lossyScale.x;
        float zSize = floor.bounds.size.z * transform.lossyScale.z;

        //Trovo una posizione random (relativa) all'interno
        Vector3 randomPositionInsideRect = new Vector3(
            (xSize - 2*safeZone.x) * Random.value,
            0,
            (zSize - 2*safeZone.z) * Random.value);

        //Calcolo la posizione (assoluta) a partire dalla posizione del floor
        target = (floor.transform.position + safeZone) + randomPositionInsideRect;
    }

    void Update() {
        if( Mathf.Abs(transform.position.x - target.x) > .2F &&
            Mathf.Abs(transform.position.z - target.z) > .2F) { //Se non arrivato vicino al punto target
            GetComponent<Animator>().SetBool("idle", false);
            transform.LookAt(target);

            GetComponent<Rigidbody>().AddRelativeForce(0, 0, 3000 * Time.deltaTime);

        } else {
            GetComponent<Animator>().SetBool("idle", true);
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.name.Contains("Floor")) {
            Destroy(gameObject);
            GameObject
                .Find("GameManager")
                .GetComponent<GameManager>()
                .AddPoints();
        }
    }
}
