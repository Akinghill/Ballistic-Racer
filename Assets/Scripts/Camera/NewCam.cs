using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewCam : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Camera.main.transform.position = this.transform.position - this.transform.forward*-10 + this.transform.up*3;
        Camera.main.transform.LookAt(this.transform.position);
        Camera.main.transform.parent = this.transform;
	}
	
	// Update is called once per frame
	void Update () {
        Camera.main.transform.position = this.transform.position - this.transform.forward*-10 + this.transform.up*3;
    	Camera.main.transform.LookAt(this.transform.position);
        Camera.main.transform.parent = this.transform;
		
	}
}
