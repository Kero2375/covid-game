using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop : MonoBehaviour {
    /*
     * color class:
     *  - nome
     *  - prezzo
     *  - isacquired
     * */

    void Start() {
        //prende array(classe) dalle pref
        //prende colore selezionato
        //evidenzia il btn
        //mette la X su quelli non acquistati
    }

    public void ColorClicked(int index) {
        //controlla se è comprato
            //si
                //seleziona
                //imposta le pref
            //no
                //apre popup
                //se lo compra
                    //controlla che abbia abbastanza punti (exit)
                    //cambia punti
                    //cambia pref (colori + punti + selezione)

    }
}
