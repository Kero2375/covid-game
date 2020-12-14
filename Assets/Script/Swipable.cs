using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swipable : MonoBehaviour {
    public SwipeManager swipeManager;

    virtual protected void Update() {
        SwipeManager.Direction dir = swipeManager.GetDirection();

        switch (dir) {
            case SwipeManager.Direction.Right:
                OnRightSwipe();
                break;
            case SwipeManager.Direction.Left:
                OnLeftSwipe();
                break;
            case SwipeManager.Direction.Up:
                OnUpSwipe();
                break;
            case SwipeManager.Direction.Down:
                OnDownSwipe();
                break;
        }
    }

    virtual protected void OnRightSwipe() { }
    virtual protected void OnLeftSwipe() { }
    virtual protected void OnUpSwipe() { }
    virtual protected void OnDownSwipe() { }
}
