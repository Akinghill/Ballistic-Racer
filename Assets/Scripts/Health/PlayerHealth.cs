using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
    public float timeTillRespawn = 5f;
    public float reflectLength;
    MeshRenderer mesh;
    public ParticleSystem[] particles;
    Collider meshCollider;

    public Transform player;
    public Transform respawnPoint;
    public ShipMovement movement;
    public PlayerShooting[] playerShooting;

    float invincibilityCounter;
    bool isDead;
    bool damaged;
    bool isInvulnerable = false;

    //PlayerInput input;
    //UltCharge ultCharge;

    bool isReflective = false;
    float reflectCounter;


    void Awake()
    {
        mesh = GetComponent<MeshRenderer>();
        //input = GetComponentInParent<PlayerInput>();
        meshCollider = GetComponent<Collider>();
        currentHealth = startingHealth;
    }

    void Update()
    {
        if (isReflective == true)
        {
            reflectCounter -= Time.deltaTime;

            if (reflectCounter <= 0)
            {
                meshCollider.enabled = true;
                StopReflect();
            }
        }

        if (currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }

        if (isInvulnerable == true)
        {
            invincibilityCounter -= Time.deltaTime;

            if (invincibilityCounter <= 0)
            {
                isInvulnerable = false;
            }
        }
        if (damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp(damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;

        healthSlider.value = currentHealth;

        if (transform.position.y <= -160)
        {
            Death();
        }
    }

    public void TakeDamage(int amount)
    {
        if (isInvulnerable == false)
        {
            damaged = true;
            currentHealth -= amount;
            //healthSlider.value = currentHealth;
        }
        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }

    public void TakeDamage(int amount, Vector3 hitPoint)
    {
        if (isInvulnerable == false)
        {
            damaged = true;
            currentHealth -= amount;
            //healthSlider.value = currentHealth;
        }

        if (currentHealth <= 0 && !isDead)
        {
            Death();
        }
    }


    public void Death()
    {
        isDead = true;

        foreach (PlayerShooting playerShooting in playerShooting)
        {
            playerShooting.DisableEffects();
            playerShooting.enabled = false;
        }
        foreach (ParticleSystem particle in particles)
        {
            particle.Stop();
        }
        mesh.enabled = false;
        meshCollider.enabled = false;
        movement.enabled = false;

        StartCoroutine(StartRespawn());
    }

    void Respawn()
    {
        if (isDead == true)
        {
            player.transform.position = respawnPoint.transform.position;
            player.transform.rotation = respawnPoint.rotation;
            mesh.enabled = true;
            meshCollider.enabled = true;
            movement.enabled = true;
            foreach (PlayerShooting playerShooting in playerShooting)
            {
                playerShooting.enabled = true;
            }
            foreach (ParticleSystem particle in particles)
            {
                particle.Play();
            }
            currentHealth = startingHealth;
            //healthSlider.value = startingHealth;
            isDead = false;
        }
    }

    IEnumerator StartRespawn()
    {
        yield return new WaitForSeconds(timeTillRespawn);
        Respawn();
    }

    public void ReflectorShield()
    {
        isReflective = true;
        meshCollider.enabled = false;
        reflectCounter = reflectLength;
    }

    public bool StopReflect()
    {
        isReflective = false;
        return false;
    }
}
