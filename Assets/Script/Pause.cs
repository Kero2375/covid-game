using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    public GameObject pausePanel;
    public GameObject music;
    public bool paused = false;

    public void Click() {
        try {
            music.GetComponent<AudioSource>().Pause();
        } catch { }
        Time.timeScale = 0;
        paused = true;
        pausePanel.SetActive(true);
    }

    public void Continue() {
        try {
            music.GetComponent<AudioSource>().UnPause();
        } catch { }
        Time.timeScale = 1;
        paused = false;
        pausePanel.SetActive(false);
    }

    public void GoToHome() {
        Time.timeScale = 1;
        SceneManager.LoadScene("SchermataIniziale", LoadSceneMode.Single);
    }
}
