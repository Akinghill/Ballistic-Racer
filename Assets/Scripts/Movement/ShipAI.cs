using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipAI : MonoBehaviour
{
    [Range(0, 1)]
    public float acceleration;

    //AIPath path;
    //List<Transform> nodes = new List<Transform>();
    Transform[] nodes;
    public int currentNode = 0;

    int shootableMask;

    [HideInInspector] public bool swapRotation;
    [HideInInspector] public float posOrNeg;

    PlayerInput input;

    ShipMovement shipMovement;

    PlayerShooting shooting;

    PlayerHealth playerHealth;

    [HideInInspector] public GameObject pathOne;
    [HideInInspector] public Transform[] m_pathOne;

    [HideInInspector] public GameObject pathTwo;
    [HideInInspector] public Transform[] m_pathTwo;

    [HideInInspector] public GameObject pathThree;
    [HideInInspector] public Transform[] m_pathThree;

    public int randomPath;

    void Start()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        //path = FindObjectOfType<AIPath>();
        input = GetComponent<PlayerInput>();
        shooting = GetComponentInChildren<PlayerShooting>();
        playerHealth = GetComponentInChildren<PlayerHealth>();
        shipMovement = GetComponent<ShipMovement>();

        //Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        //nodes = new List<Transform>();
        //for (int i = 0; i < pathTransforms.Length; i++)
        //{
        //    if (pathTransforms[i] != path.transform)
        //    {
        //        nodes.Add(pathTransforms[i]);
        //    }
        //}

        pathOne = GameObject.FindGameObjectWithTag("AI Path 1");
        int numOfNodesOnPathOne = pathOne.transform.childCount;
        m_pathOne = new Transform[numOfNodesOnPathOne];

        for (int i = 0; i < numOfNodesOnPathOne; i++)
        {
            m_pathOne[i] = pathOne.transform.GetChild(i);
        }

        pathTwo = GameObject.FindGameObjectWithTag("AI Path 2");
        int numOfNodesOnPathTwo = pathTwo.transform.childCount;
        m_pathTwo = new Transform[numOfNodesOnPathTwo];

        for (int i = 0; i < numOfNodesOnPathTwo; i++)
        {
            m_pathTwo[i] = pathTwo.transform.GetChild(i);
        }

        pathThree = GameObject.FindGameObjectWithTag("AI Path 3");
        int numOfNodesOnPathThree = pathThree.transform.childCount;
        m_pathThree = new Transform[numOfNodesOnPathThree];

        for (int i = 0; i < numOfNodesOnPathThree; i++)
        {
            m_pathThree[i] = pathThree.transform.GetChild(i);
        }

        randomPath = Random.Range(1, 11);
    }

    void FixedUpdate()
    {
        if (input.controllerNumber == 0)
        {
            Rudder();
            Accelerate();
            CheckNodeDistance();
            if (input.canShoot)
            {
                DetectEnemy();
            }
        }
    }

    void DetectEnemy()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shooting.range, shootableMask))
        {
            input.shoot = true;
        }
        else
        {
            input.shoot = false;
        }
    }

    void Rudder()
    {
        if (playerHealth.currentHealth < 75)
        {
            if (randomPath <= 5)
            {
                nodes = m_pathTwo;
            }
            else
            {
                nodes = m_pathThree;
            }
        }
        else
        {
            nodes = m_pathOne;
        }

        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);

        float directionX;
        //float directionY;
        float directionZ;

        directionX = relativeVector.x / relativeVector.magnitude;
        //directionY = relativeVector.y / relativeVector.magnitude;
        directionZ = relativeVector.z / relativeVector.magnitude;

        Vector3 nextNodeDirection = nodes[currentNode].position - transform.position;

        float angle = Vector3.SignedAngle(transform.forward, nextNodeDirection, shipMovement.normal);
        //Debug.Log(angle);

        if (angle < -5.0f)
        {
            if (angle < -40.0f)
            {
                input.brake = 1.0f;
                input.rudder = directionX;
                input.brake = 0.0f;
            }
            else
            {
                input.brake = 1.0f;
                input.rudder = -directionZ;
                input.brake = 0.0f;
            }
        }
        else if (angle > 5.0f)
        {
            if (angle > 40.0f)
            {
                input.brake = 1.0f;
                input.rudder = directionX;
                input.brake = 0.0f;
            }
            else
            {
                input.brake = 1.0f;
                input.rudder = directionZ;
                input.brake = 0.0f;
            }
        }
        else
        {
            input.rudder = directionX;
        }

        // If next node is greater than 2000 away from ship, then boost.
        if (nextNodeDirection.magnitude > 2000.0f)
        {
            input.boost = true;
        }
        else
        {
            input.boost = false;
        }
    }

    void Accelerate()
    {
        input.accelerate = acceleration;
    }

    void CheckNodeDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 50f)
        {
            if (currentNode == nodes.Length)
            {
                currentNode = 0;
                randomPath = Random.Range(1, 11);
            }
            else
            {
                currentNode++;
            }
        }
    }
}
