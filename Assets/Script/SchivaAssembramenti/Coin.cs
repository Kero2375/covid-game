using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {
    private Transform playerTransform;
    private GameManagerSchiva gameManager;
    

    // Update is called once per frame
    void Update() {
        transform.Rotate(new Vector3(0, 200 * Time.deltaTime, 0));
        if (transform.position.z <= playerTransform.position.z - 20) {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Player") {
            gameManager.AddPoints();
            gameManager.playCoinTaken();
        }
        Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other) {
        Destroy(gameObject);
    }

    public void SetAttributes(Transform playerTransform, GameManagerSchiva gameManager) {
        this.playerTransform = playerTransform;
        this.gameManager = gameManager;
    }
}
