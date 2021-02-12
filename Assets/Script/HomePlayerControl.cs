using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HomePlayerControl : MonoBehaviour {
    public float rotationFactor;
    public Texture[] skins;
    private Material material;

    private void Start() {
        material = GetComponentInChildren<SkinnedMeshRenderer>().material;
        InvokeRepeating("ChangeTexture", 0, 10);
        SaveData.ApplyMask(ref material);
    }

    void Update() {
        gameObject.transform.Rotate(new Vector3(0, rotationFactor*Time.deltaTime, 0));
    }

    private void ChangeTexture() {
        material.SetTexture("_MainTex", skins[Random.Range(0, skins.Length)]);
    }
}
