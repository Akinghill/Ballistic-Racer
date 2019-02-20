using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour {


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ship")
        {
            other.gameObject.GetComponent<PlayerHealth>().Death();
        }
    }
}
