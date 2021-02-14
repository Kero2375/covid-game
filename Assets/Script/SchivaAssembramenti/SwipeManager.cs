using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour {
    public enum Direction { None, Left, Right, Up, Down };

    Vector2 startPos, endPos;
    Direction direction = Direction.None;

    void Update() {
        //Non fare niente se in pausa
        GameObject pausePanel = GameObject.Find("Pause");
        if (pausePanel && pausePanel.GetComponent<Pause>().paused)
            return;

        if (Input.touchCount > 0) {
            Touch t = Input.GetTouch(0);

            if (t.phase == TouchPhase.Began) {
                startPos = t.position;
            } else if (t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled) {
                endPos = t.position;

                Vector2 delta = endPos - startPos;

                // se ho fatto più swipe sull'asse x
                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y)) {
                    //destra o sinistra?
                    if (delta.x < 0F) {
                        direction = Direction.Left;
                    } else {
                        direction = Direction.Right;
                    }
                } else {
                    if (delta.y < 0F) {
                        direction = Direction.Down;
                    } else {
                        direction = Direction.Up;
                    }
                }
            } else {
                direction = Direction.None;
            }
        } else {
            if (Input.GetKeyDown(KeyCode.LeftArrow)) {
                direction = Direction.Left;
            } else if (Input.GetKeyDown(KeyCode.RightArrow)) {
                direction = Direction.Right;
            } else if (Input.GetKeyDown(KeyCode.DownArrow)) {
                direction = Direction.Down;
            } else if (Input.GetKeyDown(KeyCode.UpArrow)) {
                direction = Direction.Up;
            } else {
                direction = Direction.None;
            }    
        }
    }

    public Direction GetDirection() {
        return direction;
    }
}
