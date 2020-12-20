using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PeopleMovements : MonoBehaviour {
    public Transform targetTransform;

    void Update() {
        transform.LookAt(targetTransform);
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetTransform.position,
            1F * Time.deltaTime);   
    }
}
