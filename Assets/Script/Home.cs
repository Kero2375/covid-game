using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour{

    public GameObject schivaAssembramenti;

    //Da salvare in un file
    private int punteggio;
    private bool tutorialSchivaAssembramenti = true;
    private bool tutorialMettiMascherina = true;

    public void LoadAssembramenti() {
        if (tutorialSchivaAssembramenti) {
            SceneManager.LoadScene("SchivaAssembramentiTutorial", LoadSceneMode.Single);
            tutorialSchivaAssembramenti = false;
        } else {
            SceneManager.LoadScene("SchivaAssembramenti", LoadSceneMode.Single);
        }
    }
    public void LoadBackOff() {
        SceneManager.LoadScene("BackOffTutorial", LoadSceneMode.Single);
    }
    
    public void LoadMascherina() {
        if (tutorialMettiMascherina) {
            SceneManager.LoadScene("MettiLaMascherinaTutorial", LoadSceneMode.Single);
            tutorialMettiMascherina = false;
        } else {
            SceneManager.LoadScene("MettiLaMascherina", LoadSceneMode.Single);
        }
        
    }
}
