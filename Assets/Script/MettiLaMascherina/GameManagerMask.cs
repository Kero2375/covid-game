using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerMask : MonoBehaviour {

    private int life;
    private int points;
    private GameObject[] hearts = new GameObject[3];

    private List<string> morals = new List<string>();
    private int moralIndex;

    public GameObject gameOver;
    public GameObject pointsUI;


    private void Start() {
        PopulateMorals();
        moralIndex = Random.Range(0, morals.Count);
        Time.timeScale = 1;
        for (int i = 0; i < 3; i++) {
            hearts[i] = GameObject.Find("Heart " + i);
        }
        life = 3;
        points = 0;
    }

    private void Update() {
        //Se perdo le vite fermo il gioco
        if (life == 0) {
            Time.timeScale = 0;
            gameOver.SetActive(true);
            gameOver.transform.GetChild(1).GetComponent<Text>().text = morals[moralIndex];
        }

        if((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0)) {
            //Input Mouse ritorna un Vector3, prendo quindi x e y
            Vector2 pos = Input.touchCount > 0 ? Input.GetTouch(0).position : new Vector2(Input.mousePosition.x,Input.mousePosition.y);
            Ray raycast = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;

            if(Physics.Raycast(raycast,out hit)) {
                if (hit.collider.CompareTag("Person")) {
                    //Controllo se il personaggio ha la maschera o no
                    if (!hit.collider.GetComponent<PeopleMovement>().hasMask()) {
                        //Prendo il nome della skin del personaggio
                        string s = hit.collider.GetComponentInChildren<SkinnedMeshRenderer>().material.name.Split(' ')[0] + "_mask";
                        //Aggiungo _mask per andare a prendere la skin dello stesso personaggio però con la mascherina
                        Material newMat = Resources.Load<Material>(s);
                        //Imposto la nuova skin
                        hit.collider.GetComponentInChildren<SkinnedMeshRenderer>().material = newMat;
                        //Metto la maschera al personaggio
                        hit.collider.GetComponent<PeopleMovement>().putMask();
                    } else {
                        Hit();
                    }
                }
            }
        }
    }

    public void resetScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void backToMenu() {
        SceneManager.LoadScene("SchermataIniziale");
    }

    public void Hit() {
        life--;
        hearts[life].SetActive(false);
    }

    public void AddPoints() {
        points++;
        pointsUI.transform.GetComponent<Text>().text = points.ToString();
        gameOver.transform.GetChild(2).GetComponent<Text>().text = "Punti guadagnati: " + points;
    }

    private void PopulateMorals() {
        morals.Add("Ricorda di evitare i contatti ravvicinati mantenendo la distanza di almeno un metro");
        morals.Add("Indossa sempre la mascherina, serve per limitare la diffusione");
        morals.Add("Indossa la mascherina correttamente coprendo bene mento, bocca e naso");
        morals.Add("Evita abbracci e strette di mano");
        morals.Add("Non toccarti occhi, naso e bocca con le mani");
        morals.Add("Evita i luoghi affollati");
        morals.Add("Se hai sintomi simili all'influenza, resta a casa e contatta il tuo medico");
    }

}
