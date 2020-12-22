using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour{

    public GameObject schivaAssembramenti;
    //Da salvare in un file
    private int punteggio;
    private bool tutorialOn = true;


    void Start(){
  
    }

    private void Update() {

    }

    public void LoadAssembramenti() {
        if (tutorialOn) {
            SceneManager.LoadScene("SchivaAssembramentiTutorial", LoadSceneMode.Single);
            tutorialOn = false;
        } else {
            SceneManager.LoadScene("SchivaAssembramenti", LoadSceneMode.Single);
        }
    }

    public void LoadMascherina() {
        SceneManager.LoadScene("MettiLaMascherina", LoadSceneMode.Single);
    }
}
