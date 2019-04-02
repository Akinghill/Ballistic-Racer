using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSelectionRotate : MonoBehaviour {
    public int rotation;

    void FixedUpdate()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.transform.Rotate(0, (Time.fixedDeltaTime * rotation), 0, Space.Self);
        }
    }
}
