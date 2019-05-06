using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float speed;

    [Header("Speed and Acceleration")]
    public float acceleration;
    public float maxSpeed;
    public float maxReverseSpeed;

    [Header("Decceleration and Braking")]
    public float decceleration;
    public float brakingSpeed;

    [Header("Boost and Energy Drain")]
    public float boostAcceleration;
    public float maxBoostSpeed;
    public float boostEnergyDrain;

    [Header("Ship Banking and Turning Radius")]
    public float bankingAngle;
    public float turnAngle;
    public float airBrakeSpeed;
    public Transform ship;

    [Header("Hover Power, Height, and Ground Layer Mask")]
    public float hoveringDistance;
    public float maxHoveringDistance;
    public float hoverPower;
    public LayerMask whatIsGround;
    public PIDController hoverSmooth;

    [Header("Gravity")]
    public float gravityWhileHovering;
    public float gravityWhileFalling;

    [Header("Thruster Particle and Settings")]
    public ParticleSystem[] thrusterParticle;

    public float particleStartSize;
    public float particleStartSpeed;
    public float particleStartLifeTime;

    public float particleSize;
    public float particleSpeed;
    public float particleLifeTime;

    public float particleBoostSize;
    public float particleBoostSpeed;
    public float particleBoostLifeTime;

    Rigidbody shipRigidbody;
    PlayerInput input;
    float drag;

    bool reverse;
    bool boost;
    PlayerHealth playerHealth;

    [HideInInspector] public bool emp;

    [HideInInspector] public Vector3 normal;

    [HideInInspector] public bool isRaceStarted;

    float height;
    bool isFlippedOver;

    [Header("Raycasts")]
    public Transform backLeft;
    public Transform backRight;
    public Transform frontLeft;
    public Transform frontRight;
    public RaycastHit lr;
    public RaycastHit rr;
    public RaycastHit lf;
    public RaycastHit rf;

    public bool isOnGround;
    public bool m_lrOnGround;
    public bool m_rrOnGround;
    public bool m_lfOnGround;
    public bool m_rfOnGround;

    //LayerMask whatAreWalls;

    double speed2;
    double hurt;
    public float DamageCalc;

    Vector3 lastPosition = Vector3.zero;

    void Start()
    {
        playerHealth = GetComponentInChildren<PlayerHealth>();
        shipRigidbody = GetComponent<Rigidbody>();
        input = GetComponent<PlayerInput>();
        //whatAreWalls = LayerMask.GetMask("Walls");

        drag = acceleration / maxSpeed;

        StartCoroutine(RaceCountdown());
    }

    void FixedUpdate()
    {
        speed = Vector3.Dot(shipRigidbody.velocity, transform.forward);
        speed2 = (transform.position - lastPosition).magnitude;
        lastPosition = transform.position;

        Hover();

        if (isRaceStarted)
        {
            if (!emp)
            {
                Movement();
            }
            else
            {
                EMPMovement();
            }
        }

        ThrusterParticle();

        DamageCalc = (speed*.1f);
    }

    void ThrusterParticle()
    {
        foreach (ParticleSystem particle in thrusterParticle)
        {
            particle.Play();
            var thrusterParticleMain = particle.main;
            var thrusterParticleTrails = particle.trails;
            if (!boost)
            {
                if (input.accelerate == 0f)
                {
                    thrusterParticleMain.startSize = particleStartSize;
                    thrusterParticleMain.startSpeed = particleStartSpeed;
                    thrusterParticleMain.startLifetime = particleStartLifeTime;
                }
                else
                {

                    thrusterParticleMain.startSize = particleSize * input.accelerate;
                    thrusterParticleMain.startSpeed = particleSpeed * input.accelerate;
                    thrusterParticleMain.startLifetime = particleLifeTime * input.accelerate;
                }
            }
            else
            {
                thrusterParticleMain.startSize = particleBoostSize;
                thrusterParticleMain.startSpeed = particleBoostSpeed;
                thrusterParticleMain.startLifetime = particleBoostLifeTime;
            }
            thrusterParticleMain.startSize = Mathf.Clamp(thrusterParticleMain.startSize.constant, particleStartSize, particleBoostSize);
            thrusterParticleMain.startSpeed = Mathf.Clamp(thrusterParticleMain.startSpeed.constant, particleStartSpeed, particleBoostSpeed);
            thrusterParticleMain.startLifetime = Mathf.Clamp(thrusterParticleMain.startLifetime.constant, particleStartLifeTime, particleBoostLifeTime);

            if (speed < 0)
            {
                thrusterParticleTrails.enabled = false;
            }
            else
            {
                thrusterParticleTrails.enabled = true;
            }
        }
    }

    void Hover()
    {
        Ray ray = new Ray(transform.position, -transform.up);
        RaycastHit hit;
        isOnGround = Physics.Raycast(ray, out hit, maxHoveringDistance, whatIsGround);

        Ray rayUp = new Ray(transform.position, transform.up);
        RaycastHit hitUp;
        isFlippedOver = Physics.Raycast(rayUp, out hitUp, 5.0f, whatIsGround);

        m_lrOnGround = Physics.Raycast(backLeft.position, -transform.up, out lr, maxHoveringDistance, whatIsGround);
        m_rrOnGround = Physics.Raycast(backRight.position, -transform.up, out rr, maxHoveringDistance, whatIsGround);
        m_lfOnGround = Physics.Raycast(frontLeft.position, -transform.up, out lf, maxHoveringDistance, whatIsGround);
        m_rfOnGround = Physics.Raycast(frontRight.position, -transform.up, out rf, maxHoveringDistance, whatIsGround);

        //Debug.DrawRay(transform.position, -transform.up * hit.distance, Color.blue);
        //Debug.DrawRay(backLeft.position, -transform.up * lr.distance, Color.blue);
        //Debug.DrawRay(backRight.position, -transform.up * rr.distance, Color.blue);
        //Debug.DrawRay(frontLeft.position, -transform.up * lf.distance, Color.blue);
        //Debug.DrawRay(frontRight.position, -transform.up * rf.distance, Color.blue);

        if (isFlippedOver)
        {
            StartCoroutine(DestroyUpsideDownShip());
        }

        if (isOnGround && m_lrOnGround && m_rrOnGround && m_lfOnGround && m_rfOnGround)
        {
            // Get the vectors that connect the raycast hit points

            Vector3 a = rr.point - lr.point;
            Vector3 b = rf.point - rr.point;
            Vector3 c = lf.point - rf.point;
            Vector3 d = rr.point - lf.point;

            // Get the normal at each corner

            Vector3 crossBA = Vector3.Cross(b, a);
            Vector3 crossCB = Vector3.Cross(c, b);
            Vector3 crossDC = Vector3.Cross(d, c);
            Vector3 crossAD = Vector3.Cross(a, d);

            // Calculate composite normal

            normal = (crossBA + crossCB + crossDC + crossAD + hit.normal).normalized;

            height = (lr.distance + rr.distance + lf.distance + rf.distance + hit.distance) / 5;

            float forceSmooth = hoverSmooth.Seek(hoveringDistance, height);

            Vector3 force = normal * hoverPower * forceSmooth;

            Vector3 gravity = -normal * gravityWhileHovering * height;

            shipRigidbody.AddForce(force, ForceMode.Acceleration);
            shipRigidbody.AddForce(gravity, ForceMode.Acceleration);
        }
        else
        {
            normal = Vector3.up;

            Vector3 gravity = -normal * gravityWhileFalling;
            shipRigidbody.AddForce(gravity, ForceMode.Acceleration);
        }

        Vector3 project = Vector3.ProjectOnPlane(transform.forward, normal);
        Quaternion rotation = Quaternion.LookRotation(project, normal);

        shipRigidbody.MoveRotation(Quaternion.Slerp(shipRigidbody.rotation, rotation, Time.fixedDeltaTime * 10f));

        Quaternion shipRotate = Quaternion.FromToRotation(Vector3.up, normal);

        float angle = bankingAngle * -input.rudder;
        Quaternion bodyRotation = transform.rotation * Quaternion.Euler(shipRotate.x, shipRotate.y, angle);
        ship.rotation = Quaternion.Slerp(ship.rotation, bodyRotation, Time.fixedDeltaTime * 10f);
    }

    void Movement()
    {
        //float move = speed / maxSpeed;

        float turn = turnAngle * input.rudder;

        Quaternion bodyRotation = shipRigidbody.rotation * Quaternion.Euler(0f, turn, 0f);

        shipRigidbody.rotation = Quaternion.Lerp(shipRigidbody.rotation, bodyRotation, Time.fixedDeltaTime * 10f);

        float driftSpeed = Vector3.Dot(shipRigidbody.velocity, transform.right);

        Vector3 driftFriction = -transform.right * (driftSpeed / Time.fixedDeltaTime / 2);

        shipRigidbody.AddForce(driftFriction, ForceMode.Acceleration);

        if (input.airBrake)
        {
            Vector3 airBrakeForce = transform.right * input.rudder * airBrakeSpeed;
            //Debug.Log(airBrakeForce);
            shipRigidbody.AddForce(airBrakeForce, ForceMode.Acceleration);
        }

        if (input.accelerate <= 0f)
        {
            shipRigidbody.velocity *= decceleration;
        }

        //if (!isOnGround && !m_lrOnGround && !m_rrOnGround && !m_lfOnGround && !m_rfOnGround)
        //{
        //    return;
        //}

        if (input.brake > 0)
        {
            shipRigidbody.velocity *= brakingSpeed;
        }

        if (input.brake <= 0)
        {
            if (input.boost && playerHealth.currentNRG > 1)
            {
                float thruster = boostAcceleration * 1f - drag * Mathf.Clamp(speed, 0f, maxBoostSpeed);
                shipRigidbody.AddForce(transform.forward * thruster, ForceMode.Acceleration);
                if (!boost)
                {
                    InvokeRepeating("SubtractNRG", 0f, boostEnergyDrain);
                    //Debug.Log("Boost");
                    boost = true;
                }
            }
            else
            {
                if (speed > maxSpeed)
                {
                    shipRigidbody.velocity *= .9f;
                }
                float thruster = acceleration * input.accelerate - drag * Mathf.Clamp(speed, 0f, maxSpeed);
                shipRigidbody.AddForce(transform.forward * thruster, ForceMode.Acceleration);
                CancelInvoke();
                boost = false;
            }
        }
        else
        {
            float thruster = acceleration * input.brake - drag * Mathf.Clamp(speed, 0f, maxReverseSpeed);
            shipRigidbody.AddForce(transform.forward * -thruster, ForceMode.Acceleration);
        }
    }

    void EMPMovement()
    {
        if (speed > maxSpeed)
        {
            shipRigidbody.velocity *= .9f;
        }
        float thruster = acceleration / 2f * 1f - drag * Mathf.Clamp(speed, 0f, maxSpeed / 2f);
        shipRigidbody.AddForce(transform.forward * thruster, ForceMode.Acceleration);
    }

    public IEnumerator EMPMovementTime(float time)
    {
        emp = true;
        yield return new WaitForSeconds(time);
        emp = false;
    }

    void SubtractHealth()
    {
        playerHealth.currentHealth--;
    }

    void SubtractNRG()
    {
        playerHealth.currentNRG--;
    }

    IEnumerator RaceCountdown()
    {
        yield return new WaitForSeconds(8);
        isRaceStarted = true;
    }

    IEnumerator DestroyUpsideDownShip()
    {
        yield return new WaitForSeconds(2);
        playerHealth.Death();
    }

    void OnCollisionEnter(Collision col)
    {
        if (col.collider.tag == "Wall")
        {
            playerHealth.TakeDamage((int) DamageCalc);
        }

        if (col.collider.tag == "Terrain")
        {
            playerHealth.Death();
        }
    }
}
