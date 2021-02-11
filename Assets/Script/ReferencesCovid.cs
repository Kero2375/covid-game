using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ReferencesCovid : MonoBehaviour {

    public void LoadSettings() {
        SceneManager.LoadScene("Settings", LoadSceneMode.Single);
    }

    public void OpenSite() {
        Application.OpenURL("http://www.salute.gov.it/");
    }

}
