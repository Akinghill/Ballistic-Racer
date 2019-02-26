using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
	public bool rotating;
	public Vector3 rotation;
	public bool moving;
	public float speed;
	public GameObject pos1;
	public GameObject pos2;

	void Update()
	{
		if (rotating) 
		{
			transform.Rotate (rotation * Time.deltaTime);
		}
		if (moving) 
		{
			transform.position = Vector3.Lerp (pos1.transform.position, pos2.transform.position, Mathf.PingPong (Time.time * speed, 1f));
		}
	}
	void OnTriggerStay (Collider other)
	{
		if (other.CompareTag ("Ship")) 
		{
			other.gameObject.transform.parent.parent = this.gameObject.transform;
		}
	}

	void OnTriggerExit (Collider other)
    {
        if (other.CompareTag("Ship"))
        {
            other.gameObject.transform.parent.parent = null;
        }
	}

}
