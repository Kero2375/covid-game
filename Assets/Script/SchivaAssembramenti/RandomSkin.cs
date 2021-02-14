using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkin : MonoBehaviour {

    private void Start() {
        Texture[] skins = Resources.LoadAll<Texture>("skins");
        Material material = Resources.Load("skin") as Material;
        Material mat = new Material(material);

        mat.SetTexture("_MainTex", skins[Random.Range(0, skins.Length)]);

        SaveData.ApplyMask(ref material);
        GetComponentInChildren<SkinnedMeshRenderer>().material = mat;

    }
}
