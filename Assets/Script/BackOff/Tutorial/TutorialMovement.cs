using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialMovement : MonoBehaviour {
    public GameObject hand;

    private Vector3 target = new Vector3(0, -.32F, 6);

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
