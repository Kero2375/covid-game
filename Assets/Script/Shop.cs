using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour {
    public static readonly int COLORS_NUM = 12;

    public GameObject buyPanel;
    public GameObject fewPointsPanel;

    Sprite lockSprite;
    Sprite selSprite;
    int selectedIndex;
    ShopColor[] colors;
    int buying = -1;

    void Start() {
        lockSprite = Resources.Load<Sprite>("lock");
        selSprite = Resources.Load<Sprite>("check");

        Populate();
    }

    private void Update() {
        SaveData.ModifyColorAcquired(ref colors);
        for (int i = 0; i < COLORS_NUM; i++) {
            Image overlay = GetColorOverlay(i);
            if (!colors[i].acquired) {
                overlay.color = Color.white;
                overlay.sprite = lockSprite;
            } else if (i == SaveData.GetSelectedColor()) {
                overlay.color = Color.white;
                overlay.sprite = selSprite;
            } else {
                overlay.color = Color.clear;
            }
        }
    }

    public void ColorClicked(int index) {
        if (colors[index].acquired) {
            GetColorOverlay(index).color = Color.white;
            GetColorOverlay(index).sprite = selSprite;
            SaveData.SetSelectedColor(index);
        } else {
            if (SaveData.GetPoints() >= colors[index].price) {
                buyPanel.SetActive(true);
                Text buyText = buyPanel.transform
                    .GetChild(0)
                    .GetChild(0)
                    .gameObject
                    .GetComponent<Text>();

                buyText.text = "Comprare il colore " +
                    colors[index].name.ToUpper() + " per " + 
                    colors[index].price.ToString() + " punti?";
                buying = index;
            } else {
                fewPointsPanel.SetActive(true);
            }
        }

    }

    private Image GetColorOverlay(int index) {
        GameObject button = GameObject.Find("Color_" + index);
        return button.transform.GetChild(0).gameObject.GetComponent<Image>();
    }

    public void Buy() {
        if (buying == -1)
            return;
        if (SaveData.RemovePoints(colors[buying].price)) {
            SaveData.Acquired(buying);
            SaveData.SetSelectedColor(buying);
            ClosePanel();
        }
    }

    public void ClosePanel() {
        buyPanel.SetActive(false);
        fewPointsPanel.SetActive(false);
    }

    private void Populate() {
        ShopColor[] colors = {
            new ShopColor("azzurro", 100),
            new ShopColor("verde", 100),
            new ShopColor("arancione", 100),
            new ShopColor("rosso", 100),
            new ShopColor("rosa", 100),
            new ShopColor("giallo", 100),
            new ShopColor("oliva", 100),
            new ShopColor("viola", 100),
            new ShopColor("blu", 100),
            new ShopColor("bianco", 100),
            new ShopColor("nero", 100),
            new ShopColor("rainbow", 100)
        };
        this.colors = colors;
    }
}
public class ShopColor {
    public string name;
    public int price;
    public bool acquired;
    public ShopColor(string name, int price) {
        this.name = name;
        this.price = price;
        this.acquired = false;
    }
}