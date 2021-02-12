using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    public GameObject pausePanel;
    public AudioSource music;
    public bool paused = false;

    public void Click() {
        music.Pause();
        Time.timeScale = 0;
        paused = true;
        pausePanel.SetActive(true);
    }

    public void Continue() {
        music.UnPause();
        Time.timeScale = 1;
        paused = false;
        pausePanel.SetActive(false);
    }

    public void GoToHome() {
        Time.timeScale = 1;
        SceneManager.LoadScene("SchermataIniziale", LoadSceneMode.Single);
    }

}
