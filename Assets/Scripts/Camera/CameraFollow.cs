using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothing = 1f;

    Vector3 offset;

    void Start()
    {
        offset = transform.position - target.position;
    }

    void FixedUpdate()
    {
        Vector3 targetCamPos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);
        //Camera.main.transform.position = this.transform.position - this.transform.forward*-10 + this.transform.up*3;
        //Camera.main.transform.LookAt(this.transform.position);
        //Camera.main.transform.parent = this.transform;
    }
}
