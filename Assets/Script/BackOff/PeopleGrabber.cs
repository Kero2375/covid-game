﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleGrabber : MonoBehaviour {

    private Plane plane;
    private float posY;
    private bool isInside = false;

    void Start() {
        plane = new Plane(Vector3.up, Vector3.zero);
        posY = transform.position.y;
    }

    private void OnTriggerEnter(Collider other) {
        if(other.name.Contains("Floor"))
            isInside = true;
    }

    private void OnMouseDrag() {
        if (!isInside) 
            return;
        Vector2 mousePos = Input.mousePosition;
        Ray ray = Camera.main.ScreenPointToRay(mousePos);
        plane.Raycast(ray, out float distance);
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(
            mousePos.x,
            mousePos.y,
            distance));

        transform.position = new Vector3(
            pos.x,
            posY,
            pos.z);
    }
}
