﻿using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour{

    public Text points; 

    private void Start() {
        SaveData.Load();
        points.text = SaveData.GetPoints().ToString();
    }

    public void LoadAssembramenti() {
        if (SaveData.IsTutorialDone(SaveData.GAMES.EvitaAssembramenti)) {
            SceneManager.LoadScene("SchivaAssembramenti", LoadSceneMode.Single);
        } else {
            SceneManager.LoadScene("SchivaAssembramentiTutorial", LoadSceneMode.Single);
        }
    }
    public void LoadBackOff() {
        if (SaveData.IsTutorialDone(SaveData.GAMES.FuoriDiQua)) {
            SceneManager.LoadScene("BackOff", LoadSceneMode.Single);
        } else {
            SceneManager.LoadScene("BackOffTutorial", LoadSceneMode.Single);
        }
    }
    
    public void LoadMascherina() {
        if (SaveData.IsTutorialDone(SaveData.GAMES.MettiLaMascherina)) {
            SceneManager.LoadScene("MettiLaMascherina", LoadSceneMode.Single);
        } else {
            SceneManager.LoadScene("MettiLaMascherinaTutorial", LoadSceneMode.Single);
        }
        
    }
}
