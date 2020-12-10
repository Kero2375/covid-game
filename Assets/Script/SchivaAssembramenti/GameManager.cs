﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {


    private int life;
    private int points;
    private GameObject[] hearts = new GameObject[3];

    private List<string> morals = new List<string>();
    private int moralIndex;
    public GameObject gameOver;


    private void Start() {
        PopulateMorals();
        moralIndex = Random.Range(0, morals.Count);
        Time.timeScale = 1;
        for (int i = 0; i < 3; i++) {
            hearts[i] = GameObject.Find("Heart " + i);
        }
        life = 1;
        points = 0;
    }

    private void Update() {
        //Se perdo le vite fermo il gioco
        if (life == 0) {
            Time.timeScale = 0;
            
            gameOver.SetActive(true);
            gameOver.transform.GetChild(1).GetComponent<Text>().text = morals[moralIndex];

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

    private void PopulateMorals() {
        morals.Add("Ricorda di evitare i contatti ravvicinati mantenendo la distanza di almeno un metro");
        morals.Add("Mi raccomando, indossa sempre la mascherina, serve per limitare la diffusione");
        morals.Add("Indossa la mascherina correttamente coprendo bene mento, bocca e naso");
        morals.Add("Evita abbracci e strette di mano");
        morals.Add("Non toccarti occhi, naso e bocca con le mani");
        morals.Add("Evita i luoghi affollati");
        morals.Add("Se hai sintomi simili all'influenza, resta a casa e contatta il tuo medico");

    }

}
