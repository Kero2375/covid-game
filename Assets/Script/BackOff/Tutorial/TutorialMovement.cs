using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TutorialMovement : MonoBehaviour {

    public GameObject hand;

    private Vector3 target = new Vector3(0, -.32F, 6);
    public MeshRenderer floor;

    private void Start() {
        Vector3 random = new Vector3(
            floor.bounds.size.x * Random.value,
            0,
            floor.bounds.size.z * Random.value);
        target = floor.transform.position + random;
    }
    

    void Update() {
        transform.LookAt(target);
        transform.position = Vector3.MoveTowards(
            transform.position,
            target,
            Time.deltaTime);
    }

    private void OnMouseDrag() {           
        hand.SetActive(false);
        Time.timeScale = 1;      
    }

    private void OnTriggerExit(Collider other) {   
        if (other.name.Contains("Floor")) {
            Destroy(gameObject);

        }
    }
  
}
