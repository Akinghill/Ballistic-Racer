using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class PlayerUnit : NetworkBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("Point 1");
        if(hasAuthority == true) {
            Debug.Log("Point 2");
            Camera.main.transform.position = this.transform.position - this.transform.forward*-10 + this.transform.up*3;
            Camera.main.transform.LookAt(this.transform.position);
            Camera.main.transform.parent = this.transform;
        }
    }

	Vector3 velocity;

	Vector3 BestGuessPosition;

	// Update is called once per frame
	void Update () {

		transform.Translate(velocity * Time.deltaTime);
		
		if(hasAuthority == false) {
			return;
		}
		if(Input.GetKeyDown(KeyCode.Space)) {
			this.transform.Translate(0,1,0);
		}
		if(Input.GetKeyDown(KeyCode.N)) {
			Destroy(gameObject);
		}

        var x = Input.GetAxis("Horizontal") * Time.deltaTime * -120.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 75.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }

	[Command]
	void CmdUpdateVelocity(Vector3 v, Vector3 p){
		transform.position = p;
		velocity = v;

		//transform.position = p + (v * (LatencyToServer) )

		RpcUpdateVelocity(velocity, transform.position);
	}

	[ClientRpc]
	void RpcUpdateVelocity(Vector3 v, Vector3 p){

		if(hasAuthority){
			return;
		}

		//transform.position = p;

		//BestGuessPosition = p + v
		velocity = v;

		//transform.position = p + (v * (ourLatency + ThreirLatency) )
	}
}
