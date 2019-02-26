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

    void Start()
    {
        shootableMask = LayerMask.GetMask("Shootable");
        path = FindObjectOfType<AIPath>();
        input = GetComponent<PlayerInput>();
        shooting = GetComponentInChildren<PlayerShooting>();

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
        //float directionY;
        float directionZ;

        directionX = relativeVector.x / relativeVector.magnitude;
        //directionY = relativeVector.y / relativeVector.magnitude;
        directionZ = relativeVector.z / relativeVector.magnitude;

        Vector3 nextNodeDirection = nodes[currentNode].position - transform.position;

        float angle = Vector3.SignedAngle(transform.forward, nextNodeDirection, Vector3.up);
        //Debug.Log(angle);

        // Turn left or right if angle between ship and next node is less than -5 or greater than 5. If not, then go forward.
        if (angle < -5.0f)
        {
            //Debug.Log("turn left");
            input.rudder = -directionZ;
        }
        else if (angle > 5.0f)
        {
            //Debug.Log("turn right");
            input.rudder = directionZ;
        }
        else
        {
            //Debug.Log("forward");
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
