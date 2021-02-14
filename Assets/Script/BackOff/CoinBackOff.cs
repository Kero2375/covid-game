using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBackOff : MonoBehaviour {
    public GameManager gameManager;

    private void Start() {
        Invoke("AutoDestroy", 7F);
    }

    // Update is called once per frame
    void Update() {
        transform.Rotate(new Vector3(0, 200 * Time.deltaTime, 0));
    }

    private void OnMouseDown() {
        gameManager.AddPoints();
        gameManager.playCoinTaken();
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Person") {
            Destroy(gameObject);
        }
    }

    private void AutoDestroy() {
        Destroy(gameObject);
    }

}
