using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    public float initSpeed;
    public SwipeManager swipeManager;
    int lane = 1;

    private float speed;
    void Start() {
        speed = initSpeed;
    }

    void Update() {
        gameObject.transform.Translate(new Vector3(0, 0, speed));
        SwipeManager.Direction dir = swipeManager.GetDirection();

        switch (dir) {
            case SwipeManager.Direction.Left:
                if(lane != 0) {
                    gameObject.transform.Translate(Vector3.left * 2.5F);
                    lane--;
                }
                break;
            case SwipeManager.Direction.Right:
                if(lane != 2) {
                    gameObject.transform.Translate(Vector3.right * 2.5F);
                    lane++;
                }
                break;
        }
    }
}
