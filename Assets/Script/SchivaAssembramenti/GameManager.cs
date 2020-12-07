using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
    private int life;
    private GameObject[] hearts = new GameObject[3];
    public GameObject gameOver;

    private int points;

    private void Start() {
        Time.timeScale = 1;
        for (int i = 0; i < 3; i++) {
            hearts[i] = GameObject.Find("Heart " + i);
        }
        life = 3;
        points = 0;
    }

    private void Update() {
        //Se perdo le vite fermo il gioco
        if (life == 0) {
            Time.timeScale = 0;
            gameOver.SetActive(true);
        }
    }

    public void resetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Hit() {
        life--;
        hearts[life].SetActive(false);
    }

    public void AddPoints() {
        points++;
        gameOver.transform.GetChild(2).GetComponent<Text>().text = "Punti guadagnati: " + points;
    }
}
