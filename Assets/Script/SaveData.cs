using System;
using System.Collections.Generic;
using UnityEngine;

public static class SaveData {
    private static int points;
    private static IDictionary<string, bool> tutorial = new Dictionary<string, bool>();
    private static bool musicOn, soundOn;
    private static int selectedMask = 0;

    public enum GAMES {
        EvitaAssembramenti,
        MettiLaMascherina,
        FuoriDiQua
    };
    public static bool IsTutorialDone(GAMES game) {
        bool value = false;
        tutorial.TryGetValue(game.ToString(), out value);
        return value;
    }
    public static void SetTutorial(GAMES game) {
        if(tutorial.ContainsKey(game.ToString())) {
            tutorial[game.ToString()] = true;
        } else {
            tutorial.Add(game.ToString(), true);
        }
        PlayerPrefs.SetInt(game.ToString(), 1);
        PlayerPrefs.Save();
    }
    public static void AddPoints(int p) {
        if (PlayerPrefs.HasKey("Points")) {
            p += PlayerPrefs.GetInt("Points");
        }
        PlayerPrefs.SetInt("Points", p);
        PlayerPrefs.Save();
        SaveData.points = p;
    }
    public static bool RemovePoints(int p) {
        if (PlayerPrefs.HasKey("Points")) {
            p = PlayerPrefs.GetInt("Points") - p;
            if (p > 0) {
                PlayerPrefs.SetInt("Points", p);
                PlayerPrefs.Save();
                SaveData.points = p;
                return true;
            } else {
                return false;
            }
        }
        PlayerPrefs.SetInt("Points", 0);
        PlayerPrefs.Save();
        return false;
    }
    public static int GetPoints() {
        return points;
    }

    public static bool GetMusicOn() {
        return musicOn;
    }

    public static bool GetSoundOn() {
        return soundOn;
    }

    public static void SaveMusicPreferences(bool value){
        musicOn = value;
        int valueMusic = 0;
        if (value) {
            valueMusic = 1;
        }
        PlayerPrefs.SetInt("Music", valueMusic);
        PlayerPrefs.Save();
    }
    public static void SaveSoundPreferences(bool value) {
        soundOn = value;
        int valueSound = 0;
        if (value) {
            valueSound = 1;
        }
        PlayerPrefs.SetInt("Sound", valueSound);
        PlayerPrefs.Save();
    }

    public static void Load() {
        LoadPoints();
        LoadTutorial();
        LoadColor();
        LoadMusicSoundPreferences();
    }

    private static void LoadPoints() {
        if (PlayerPrefs.HasKey("Points")) {
            points = PlayerPrefs.GetInt("Points");
        } else {
            PlayerPrefs.SetInt("Points", 0);
            PlayerPrefs.Save();
            points = 0;
        }
    }
    private static void LoadTutorial() {
        string[] gameNames = Enum.GetNames(typeof(GAMES));
        foreach (string game in gameNames) {
            if (PlayerPrefs.HasKey(game)) {
                if (!tutorial.ContainsKey(game)) {
                    tutorial.Add(game, PlayerPrefs.GetInt(game) == 1 ? true : false);
                }
            } else {
                PlayerPrefs.SetInt(game, 0);
                PlayerPrefs.Save();
            }
        }
    }

    private static void LoadColor() {
        if (PlayerPrefs.HasKey("SelectedColor")) {
            selectedMask = PlayerPrefs.GetInt("SelectedColor");
        } else {
            PlayerPrefs.SetInt("SelectedColor", 0);
            PlayerPrefs.Save();
            selectedMask = 0;
        }
    }

    private static void LoadMusicSoundPreferences() {
        Debug.Log("Music" + PlayerPrefs.GetInt("Music"));

        Debug.Log("Sound" + PlayerPrefs.GetInt("Sound"));
        musicOn = PlayerPrefs.GetInt("Music") > 0 ? true : false;
        soundOn = PlayerPrefs.GetInt("Sound") > 0 ? true : false;
    }

    //Quando chiamato, modifico l'array di colori passato (ref)
    public static void ModifyColorAcquired(ref ShopColor[] colors) { 
        for (int i=0; i<Shop.COLORS_NUM; i++) {
            if (PlayerPrefs.HasKey("Color_" + i)) {
                //imposto se è stato acquistato il colore (int = 1)
                colors[i].acquired = PlayerPrefs.GetInt("Color_" + i) == 1;
            } else {
                //se non c'è lo salvo e imposto a false (primo avvio)
                PlayerPrefs.SetInt("Color_" + i, 0);
                PlayerPrefs.Save();
                colors[i].acquired = false;
            }
        }
    }

    public static void Acquired(int index) {
        PlayerPrefs.SetInt("Color_" + index, 1);
    }

    public static void SetSelectedColor(int colorIndex) {
        PlayerPrefs.SetInt("SelectedColor",colorIndex);
        PlayerPrefs.Save();
        selectedMask = colorIndex;
    }

    public static int GetSelectedColor() {
        if(PlayerPrefs.HasKey("SelectedColor")) {
            return PlayerPrefs.GetInt("SelectedColor");
        } else {
            PlayerPrefs.SetInt("SelectedColor", 0);
            return 0;
        }
    }

    public static void ApplyMask(ref Material mat) {
        Texture color = Resources.Load("mask/Color_" + selectedMask) as Texture;
        Texture mask = Resources.Load("mask/mask") as Texture;
        mat.SetTexture("_MaskColor", color);
        mat.SetTexture("_MaskTex", mask);
    }
}
