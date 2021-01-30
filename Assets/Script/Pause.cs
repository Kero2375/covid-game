using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
    public GameObject pausePanel;

    public void Click() {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
    }

    public void Continue() {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void GoToHome() {
        Time.timeScale = 1;
        SceneManager.LoadScene("SchermataIniziale", LoadSceneMode.Single);
    }
}
