using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManagerBackOff : MonoBehaviour {

    public GameObject person;
    public GameObject hand;

    void Update() {
        if (person.transform.position.z < 12F) {
            Time.timeScale = 0;

            hand.SetActive(true);
            hand.transform.Translate(new Vector3(20, 80, 0) * Time.unscaledDeltaTime);
            if (hand.GetComponent<RectTransform>().anchoredPosition.y >= 250F) {
                hand.GetComponent<RectTransform>().anchoredPosition = new Vector3(120F, 0, 0);
            }
        }
    }
}
