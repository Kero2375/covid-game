﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public abstract class GameManager : MonoBehaviour {

    public AudioClip damageSound;

    public GameObject gameOver;
    public GameObject pointsUI;
    public AudioSource music;

    private int lifes;
    private int points;
    private float speedFactor;

    private GameObject[] hearts;

    protected bool soundOn;
    
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
        SaveData.Load();
        if(SaveData.GetMusicOn())
            music.Play();
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
        if (SaveData.GetMusicOn() && !music.isPlaying && !GameObject.Find("Pause").GetComponent<Pause>().paused) 
            music.Play();
        else if (!SaveData.GetMusicOn())
            music.Stop();
        soundOn = SaveData.GetSoundOn();
        if(lifes == 0 && !gameOver.activeSelf) {
            StopTime();
            if(SaveData.GetMusicOn())
                music.Stop();
            gameOver.SetActive(true);
            gameOver.transform.GetChild(1).GetComponent<Text>().text = MORALS[moralIndex];
            gameOver.transform.GetChild(2).GetComponent<Text>().text = "Punti guadagnati: " + points;
            SaveData.AddPoints(points);
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
        playDamageTaken();
        lifes--;
        hearts[lifes].SetActive(false);
        GameObject.Find("Damage").GetComponent<Animator>().SetTrigger("damage");
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

    public int GetPoints() {
        return points;
    }
    public void playDamageTaken() {
        if (soundOn) {
            AudioSource.PlayClipAtPoint(damageSound, GameObject.Find("Main Camera").transform.position, 0.05F);
        }
    }


}
