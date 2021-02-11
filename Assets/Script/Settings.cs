using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour{

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

    private void ToggleChange(Toggle toggle,bool isOn) {
        ColorBlock cb = toggle.colors;
        if (isOn) {
            toggle.isOn = true;
            cb.selectedColor = Color.green;
            cb.normalColor = Color.green;
            cb.highlightedColor = Color.green;
        } else {
            toggle.isOn = false;
            cb.selectedColor = Color.red;
            cb.normalColor = Color.red;
            cb.highlightedColor = Color.red;
        }
        toggle.colors = cb;
    }

    // Update is called once per frame
    void Update(){
        
    }
}
