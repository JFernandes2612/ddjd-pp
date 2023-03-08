using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class Tiling : MonoBehaviour
{
    void Start () {
        GetComponent<Renderer>().sharedMaterial.mainTextureScale = new Vector2(gameObject.transform.localScale.x / 2.5f, gameObject.transform.localScale.z / 2.5f);
    }
}
