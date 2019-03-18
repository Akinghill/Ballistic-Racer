using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapDamage : MonoBehaviour {
	public bool rotate;
    void Update()
    {
		if (rotate) {
			transform.Rotate (0, (360 * (Time.deltaTime * 10)), 0);
		}
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ship")
        {
            other.gameObject.GetComponent<PlayerHealth>().Death();
        }
    }
}
