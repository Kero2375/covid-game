using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifes : MonoBehaviour {
    public GameManagerSchiva gameManager;

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Ostacolo") {
            gameManager.DecreaseLifes();
        }
    }
}
