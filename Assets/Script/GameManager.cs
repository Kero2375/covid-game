using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class GameManager : MonoBehaviour {
    public GameObject gameOver;
    public GameObject pointsUI;

    private int lifes;
    private int points;
    private float speedFactor;

    private GameObject[] hearts;
    
    private int moralIndex;
    private static readonly string[] MORALS = {
        "Ricorda di evitare i contatti ravvicinati mantenendo la distanza di almeno un metro",
        "Indossa sempre la mascherina, serve per limitare la diffusione",
        "Indossa la mascherina correttamente coprendo bene mento, bocca e naso",
        "Evita abbracci e strette di mano",
        "Non toccarti occhi, naso e bocca con le mani",
        "Evita i luoghi affollati",
        "Se hai sintomi simili all'influenza, resta a casa e contatta il tuo medico"};

    public virtual void Start() {
        moralIndex = Random.Range(0, MORALS.Length);
        lifes = 3;
        points = 0;
        speedFactor = 1;
        hearts = new GameObject[3];
        for (int i = 0; i < 3; i++) {
            hearts[i] = GameObject.Find("Heart " + i);
        }
        StartTime();
    }

    public virtual void Update() {
        if(lifes == 0) {
            StopTime();
            gameOver.SetActive(true);
            gameOver.transform.GetChild(1).GetComponent<Text>().text = MORALS[moralIndex];
            gameOver.transform.GetChild(2).GetComponent<Text>().text = "Punti guadagnati: " + points;
        }
    }

    // GameOver menu 
    public void ResetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void BackToMenu() {
        SceneManager.LoadScene("SchermataIniziale");
    }

    // Lifes and Points
    public void DecreaseLifes() {
        lifes--;
        hearts[lifes].SetActive(false);
    }
    public void AddPoints(int point = 1) {
        points += point; 
        pointsUI.transform.GetComponent<Text>().text = points.ToString();
    }

    // Game speed
    public float GetSpeedFactor() {
        return speedFactor;
    }
    public void IncreaseSpeed(float value) {
        speedFactor += value;
    }
    private void StartTime() { Time.timeScale = 1; }
    private void StopTime() { Time.timeScale = 0; }

}
