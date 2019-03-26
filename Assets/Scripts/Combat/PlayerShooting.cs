using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int damagePerShot = 20;
    public float timeBetweenBullets = 0.15f;
    public float range = 100f;

    float timer;
    Ray shootRay = new Ray();
    RaycastHit shootHit;
    int shootableMask;
    int ricochetMask;
    ParticleSystem gunParticles;
    LineRenderer gunLine;
    AudioSource gunAudio;
    Light gunLight;
    float effectsDisplayTime = 0.2f;

    PlayerInput input;

    void Awake()
    {
        input = GetComponentInParent<PlayerInput>();
        shootableMask = LayerMask.GetMask("Shootable");
        ricochetMask = LayerMask.GetMask("Ricochet");
        gunParticles = GetComponent<ParticleSystem>();
        gunLine = GetComponent<LineRenderer>();
        gunAudio = GetComponent<AudioSource>();
        gunLight = GetComponent<Light>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (input.shoot && timer >= timeBetweenBullets && Time.timeScale != 0)
        {
            Shoot();
        }

        if (timer >= timeBetweenBullets * effectsDisplayTime)
        {
            DisableEffects();
        }
    }

    public void DisableEffects()
    {
        gunLine.enabled = false;
        gunLight.enabled = false;
    }

    void Shoot()
    {
        timer = 0f;

        gunAudio.Play();

        gunLight.enabled = true;

        gunParticles.Stop();
        gunParticles.Play();

        gunLine.enabled = true;
        gunLine.SetPosition(0, new Vector3(0f, 0f, 0f));

        shootRay.origin = transform.position;
        shootRay.direction = transform.forward;

        if (Physics.Raycast(shootRay, out shootHit, range, shootableMask))
        {
            PlayerHealth playerHealth = shootHit.collider.GetComponentInParent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damagePerShot, shootHit.point);
            }
            gunLine.SetPosition(1, shootHit.point);

        }
        else
        {
            gunLine.SetPosition(1, new Vector3(0f, 0f, 1f * range));
        }

        //No idea if this will work correctly, needs testing.
        if (Physics.Raycast(shootRay, out shootHit, range, ricochetMask))
        {
            Vector3 reflectV = Vector3.Reflect(transform.forward, shootHit.normal);
            transform.position = shootHit.point;
            transform.rotation = Quaternion.LookRotation(reflectV);
        }
        else
        {
            gunLine.SetPosition(1, new Vector3(0f, 0f, 1f * range));
        }
    }
}
