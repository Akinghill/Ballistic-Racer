using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

	void Update () 
	{
		Quaternion targetRotation = Quaternion.LookRotation(GameObject.FindGameObjectWithTag("Cameralookpoint").transform.transform.position - transform.position);
		transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10 * Time.deltaTime);
	}
}
