using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour {

    void Update()
    {
        transform.Rotate(0, (360 * (Time.deltaTime * 10)), 0);
    }


    void OnCollisionEnter(Collider other)
    {
        if (other.gameObject.tag == "Ship")
        {
            other.gameObject.GetComponent<PlayerHealth>().Death();
        }
    }
}
