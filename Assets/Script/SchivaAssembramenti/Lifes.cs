using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifes : MonoBehaviour {
    public GameManager gameManager;
    public Animator skinAnimator;

    private void OnTriggerEnter(Collider other) {
        if (other.name == "Ostacolo") {
            gameManager.Hit();
            skinAnimator.SetTrigger("Hitted");
        }
    }
}
