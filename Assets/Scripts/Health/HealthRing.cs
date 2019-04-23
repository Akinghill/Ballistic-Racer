using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthRing : MonoBehaviour
{
    public int healthAmmount;

    public float timeToRestart;

    bool deactivated;

    IEnumerator ActivationTime()
    {
        deactivated = true;
        yield return new WaitForSeconds(timeToRestart);
        deactivated = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.transform.parent.tag == "Ship")
        {
            if (!deactivated)
            {
                other.GetComponentInParent<PlayerHealth>().currentHealth += healthAmmount;
                StartCoroutine(ActivationTime());
            }
        }
    }
}
