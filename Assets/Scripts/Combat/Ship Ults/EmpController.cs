using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpController : MonoBehaviour
{
    public float MaxBlastRadius = 60f;
    public float currentRadius;
    public float timeTillCollapse;

    public float empEffectTime;
    public GameObject shipPower;

    public Collider empCollider;
    public Collider shipCollider;
    ParticleSystem electricParticle;

    int shootableMask;

    float timer;
    float EmpRest = .5f;

    //int shootableMask;

    bool bursting;
    bool returning;

    //SphereCollider EmpBlast;
    PlayerInput input;
    UltCharge ultCharge;

    void Start()
    {
        shootableMask = LayerMask.NameToLayer("Shootable");
        input = GetComponentInParent<PlayerInput>();
        //EmpBlast = GetComponent<SphereCollider>();
        //shootableMask = LayerMask.GetMask("Shootable");
        electricParticle = GetComponent<ParticleSystem>();
        ultCharge = GetComponentInParent<UltCharge>();
        //EmpRest = EmpBlast.radius;
    }

    void Update()
    {
        //if (ultCharge.ultCharged)
        //{
        if (input.powerUp)
        {
            bursting = true;
            ultCharge.CancelInvoke("UltimateCharge");
            StartCoroutine(EMPBurst());

            ultCharge.ultPower = 0;
        }
        //}
    }

    void ChangeParticle(float radius, float size)
    {
        var electricParticleMain = electricParticle.main;
        var electricParticleShape = electricParticle.shape;

        electricParticleShape.radius = radius;

        electricParticleMain.startSize = size;
    }

    private void Burst()
    {

    }

    IEnumerator EMPBurst()
    {
        while (bursting)
        {
            empCollider.enabled = true;
            if (currentRadius < MaxBlastRadius)
            {
                currentRadius += Time.deltaTime * 100f;
                EMPRange(currentRadius);
                ChangeParticle(15f, 4f);
            }
            else
            {
                bursting = false;
                returning = true;
            }
            yield return null;
        }
        yield return new WaitForSeconds(timeTillCollapse);
        while (returning)
        {
            if (currentRadius > EmpRest)
            {
                currentRadius -= Time.deltaTime * 100f;
                EMPRange(currentRadius);
            }
            else
            {
                ultCharge.InvokeRepeating("UltimateCharge", ultCharge.chargeRate, ultCharge.chargeRate);
                ChangeParticle(0.0001f, 1f);
                currentRadius = EmpRest;

                returning = false;
            }
            yield return null;
            empCollider.enabled = false;
            shipPower.GetComponent<EmpController>().enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ship" && other != shipCollider)
        {

        }
    }

    void EMPRange(float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, radius);
        foreach (Collider hitCollider in hitColliders)
        {
            if (hitCollider != shipCollider && hitCollider.gameObject.layer == shootableMask)
            {
                StartCoroutine(hitCollider.GetComponentInParent<ShipMovement>().EMPMovementTime(empEffectTime));
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, currentRadius);
    }
}
