using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleGrabber : MonoBehaviour {

    private Plane plane;
    private float posY;
    private bool moveEnabled = false;

    private Vector3 startPos;
    private Vector3 throwForce = Vector3.zero;

    void Start() {
        plane = new Plane(Vector3.up, Vector3.zero);
        posY = transform.position.y;
    }

    private void Update() {
        GetComponent<Rigidbody>().AddForce(throwForce * 20);
    }

    private void OnTriggerEnter(Collider other) {
        if (other.name.Contains("Floor"))
            moveEnabled = true;
    }

    public void SetMovable() {
        moveEnabled = true;
    }

    private void OnMouseDrag() {
        //Non fare niente se in pausa
        Pause pausePanel = GameObject.Find("Pause").GetComponent<Pause>();
        if (pausePanel && pausePanel.paused)
            return;

        if (!moveEnabled) 
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

    private void OnMouseDown() {
        startPos = transform.position;

    }

    private void OnMouseUp() {
        Vector3 start = new Vector3(startPos.x, 0, startPos.z);
        Vector3 end = new Vector3(transform.position.x, 0, transform.position.z);

        throwForce = end - start;
    }

}
