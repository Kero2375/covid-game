using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleGrabber : MonoBehaviour {

    private Plane plane;

    void Start() {
        plane = new Plane(Vector3.up, Vector3.zero);
    }

    void Update() {
    }

    private void OnMouseDrag() {
        Vector2 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        plane.Raycast(ray, out float distance);
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(
            mousePos.x,
            mousePos.y,
            distance));

        transform.position = new Vector3(
            pos.x,
            transform.position.y,
            pos.z);
    }
}
