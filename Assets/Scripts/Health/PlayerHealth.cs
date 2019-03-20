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
    public GameObject shipCollider;

    public Transform player;
    public Transform respawnPoint;
    public ShipMovement movement;
    public PlayerShooting[] playerShooting;

    public GameObject DeathXplosion;

    public GameObject SparkFX;
    Rigidbody rb;
    UltCharge ultimate;
    float invincibilityCounter;
    bool isDead;
    bool damaged;
    bool isInvulnerable = false;
    int ricochetLayer;
    int shootableLayer;

    PlayerInput input;
    //UltCharge ultCharge;

    public bool isReflective;
    public float reflectCounter;


    void Awake()
    {
        mesh = GetComponentInChildren<MeshRenderer>();
        input = GetComponentInParent<PlayerInput>();
        rb = GetComponentInParent<Rigidbody>();
        meshCollider = GetComponentInChildren<Collider>();
        currentHealth = startingHealth;
        ricochetLayer = LayerMask.NameToLayer("Ricochet");
        shootableLayer = LayerMask.NameToLayer("Shootable");
        ultimate = GetComponentInParent<UltCharge>();
    }

    void Update()
    {
        if (shipCollider.layer == ricochetLayer)
        {
            reflectCounter -= Time.deltaTime;

            if (reflectCounter <= 0)
            {
                shipCollider.layer = shootableLayer;
                StopReflect();
                //GetComponent<ReflectorShield>().enabled = false;
            }
        }

        if (currentHealth > startingHealth)
        {
            currentHealth = startingHealth;
        }

        if(currentHealth < 50)
        {
            SparkFX.GetComponent<ParticleSystem>().Play();
        }
        else
        {
            SparkFX.GetComponent<ParticleSystem>().Pause();
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
            SparkFX.GetComponent<ParticleSystem>().Pause();
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
        rb.velocity = Vector3.zero;
        ultimate.ultPower = 0;
        GameObject clone = (GameObject)Instantiate (DeathXplosion, transform.position, Quaternion.identity);

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
        Destroy(clone, 1f);

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
        shipCollider.layer = ricochetLayer;
        reflectCounter = reflectLength;
        isReflective = true;
    }

    public bool StopReflect()
    {
        isReflective = false;
        return false;
    }
}
