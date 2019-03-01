using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingMat : MonoBehaviour {

    public float scrollSpeed = 0.5f;
    public Material mat;

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        mat.SetTextureOffset("_MainTex", new Vector2(0, offset));
    }
}
