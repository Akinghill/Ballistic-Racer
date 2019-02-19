using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ShipAI : MonoBehaviour
{
    [Range(0, 1)]
    public float acceleration;

    AIPath path;

    List<Transform> nodes = new List<Transform>();
    int currentNode = 0;

    int shootableMask;

    [HideInInspector] public bool swapRotation;
    [HideInInspector] public float posOrNeg;

    PlayerInput input;

    PlayerShooting shooting;

    //ShipMovement shipMovement;

    void Start()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        path = FindObjectOfType<AIPath>();
        input = GetComponent<PlayerInput>();
        shooting = GetComponentInChildren<PlayerShooting>();
        //shipMovement = GetComponent<ShipMovement>();

        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();

        nodes = new List<Transform>();

        for (int i = 0; i < pathTransforms.Length; i++)
        {
            if (pathTransforms[i] != path.transform)
            {
                nodes.Add(pathTransforms[i]);
            }
        }
    }

    void FixedUpdate()
    {
        CheckNodeDistance();
        if (input.controllerNumber == 0)
        {
            Rudder();
            Accelerate();
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
        Vector3 relativeVector = transform.InverseTransformPoint(nodes[currentNode].position);

        float directionX;
        float directionZ;

        directionX = relativeVector.x / relativeVector.magnitude;
        directionZ = relativeVector.z / relativeVector.magnitude;

        Vector3 nextNodeDirection = nodes[currentNode].position - transform.position;

        float angle = Vector3.Angle(nextNodeDirection, transform.forward);

        if (relativeVector.x < 0f)
        {
            input.rudder = directionX;
        }
        else if (relativeVector.x > 0f && relativeVector.z > 0f)
        {
            if (angle > 0f && angle < 10f)
            {
                input.rudder = directionZ;
            }
            else
            {
                input.rudder = directionZ * 2.0f;
            }
        }

        //float directionY;

        //directionY = relativeVector.y / relativeVector.magnitude;

        //if (!swapRotation)
        //{
        //    input.rudder = directionX;
        //}
        //else
        //{
        //    input.rudder = directionY * posOrNeg;
        //}
    }

    void Accelerate()
    {
        input.accelerate = acceleration;
    }

    void CheckNodeDistance()
    {
        if (Vector3.Distance(transform.position, nodes[currentNode].position) < 25f)
        {
            if (currentNode == nodes.Count - 1)
            {
                currentNode = 0;
            }
            else
            {
                currentNode++;
            }
        }
    }
}
