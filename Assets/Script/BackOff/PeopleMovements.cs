using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleMovements : MonoBehaviour {
    public float direction = 1F;
    public MeshRenderer floor;

    Vector3 target;

    private void Start() {
        Vector3 random = new Vector3(
            (floor.bounds.size.x - .4F) * Random.value,
            0,
            (floor.bounds.size.z - .4F) * Random.value);
        target = floor.transform.position + random;
    }

    void Update() {
        if( Mathf.Abs(transform.position.x - target.x) > .2F &&
            Mathf.Abs(transform.position.z - target.z) > .2F) { //se arrivato vicino al punto target
            GetComponent<Animator>().SetBool("idle", false);
            transform.LookAt(target);
            transform.position = Vector3.MoveTowards(
                transform.position,
                target,
                direction * Time.deltaTime);   
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
