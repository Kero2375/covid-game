using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Settings : MonoBehaviour{

    public Sprite toggleOn, toggleOff;

    private Toggle musicToggle;
    private Toggle soundToggle;

    void Start(){
        
        musicToggle = GameObject.Find("ButtonMusic").GetComponent<Toggle>();
        soundToggle = GameObject.Find("ButtonSound").GetComponent<Toggle>();
        
        musicToggle.onValueChanged.AddListener(OnMusicToggleValueChanged);
        soundToggle.onValueChanged.AddListener(OnSoundToggleValueChanged);



        ToggleChange(musicToggle, SaveData.GetMusicOn());
        ToggleChange(soundToggle, SaveData.GetSoundOn());

    }

    private void OnSoundToggleValueChanged(bool isOn) {
        ToggleChange(soundToggle, isOn);
        SaveData.SaveSoundPreferences(isOn);
    }

    private void OnMusicToggleValueChanged(bool isOn) {
        ToggleChange(musicToggle, isOn);
        SaveData.SaveMusicPreferences(isOn);
    }

    private void ToggleChange(Toggle toggle, bool isOn) {
        ColorBlock cb = toggle.colors;
        Color red, green;
        ColorUtility.TryParseHtmlString("#C85050", out red);
        ColorUtility.TryParseHtmlString("#6D9B71", out green);

        if (isOn) {
            toggle.isOn = true;
            cb.selectedColor = green;
            cb.normalColor = green;
            cb.highlightedColor = green;
            toggle.image.sprite = toggleOn;
        } else {
            toggle.isOn = false;
            cb.selectedColor = red;
            cb.normalColor = red;
            cb.highlightedColor = red;
            toggle.image.sprite = toggleOff;
        }
        toggle.colors = cb;
    }

    public void LoadTutorialEvitaAssembramenti() {
        LoadTutorial("SchivaAssembramentiTutorial");
    }

    public void LoadTutorialMettiLaMascherina() {
        LoadTutorial("MettiLaMascherinaTutorial");
    }

    public void LoadTutorialBackOff() {
        LoadTutorial("BackOffTutorial");
    }

    private void LoadTutorial(string name) {
        SceneManager.LoadScene(name, LoadSceneMode.Single);
    }

    public void LoadRiferimentiNorme() {
        SceneManager.LoadScene("RiferimentoNormeCovid",LoadSceneMode.Single);
    }

    

}
