using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupPickup : MonoBehaviour {

    public float timeToRestart;

    bool deactivated;
    //int powerUpNum;


    IEnumerator ActivationTime()
    {
        deactivated = true;
        yield return new WaitForSeconds(timeToRestart);
        deactivated = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Shootable"))
        {
            if (!deactivated)
            {
                //Pickup();
                StartCoroutine(ActivationTime());
            }
        }
    }

   /* void Pickup()
    {
        powerUpNum = Random.Range(0, 4);
        if (powerUpNum == 0)
        {
            gameObject.GetComponent<Invulnerability>().enabled = true;
        }
        if (powerUpNum == 1)
        {
            gameObject.GetComponent<EmpController>().enabled = true;
        }
        if (powerUpNum == 2)
        {
            gameObject.GetComponent<HyperSpeed>().enabled = true;
        }
        if (powerUpNum == 3)
        {
            gameObject.GetComponent<ReflectorShield>().enabled = true;
        }
    } */
}
