using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class BlendCameras : MonoBehaviour {

    public Transform followTarget;
    public float initialWeight = 10f;

    private CinemachineMixingCamera vcam;

    void Start()
    {
        if (followTarget)
        {
            vcam = GetComponent<CinemachineMixingCamera>();
            vcam.m_Weight0 = initialWeight;
        }
    }

    void Update()
    {
        if (followTarget)
        {
            vcam.m_Weight1 = Mathf.Abs((Mathf.Abs(followTarget.transform.position.x) + Mathf.Abs(followTarget.transform.position.z)) / Mathf.Abs(followTarget.transform.position.y));
        }
    }
}
