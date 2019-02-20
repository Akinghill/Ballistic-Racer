using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerConnectionObject : NetworkBehaviour {

	// Use this for initialization
	void Start () {
		
		if( isLocalPlayer == false)
		{

			return;
		}

		Debug.Log("PlayerObject::Start -- Spawning My own Personal Unit.");
		CmdSpawnMyUnit();
	}

	public GameObject PlayerUnitPrefab;


	[SyncVar(hook="OnPlayerNameChanged")]
	public string PlayerName = "Anonymous";
	
	// Update is called once per frame
	void Update () {

		if( isLocalPlayer == false) {
			return;
		}

		if(Input.GetKeyDown(KeyCode.M)) {
			CmdSpawnMyUnit();
		}
		
		if(Input.GetKeyDown(KeyCode.Q)){
			string n ="User" + Random.Range(1,100);

			Debug.Log("Sendeing server Name Request");
			CmdChangePlayerName(n);
		}
	}

	void OnPlayerNameChanged(string NewName){
		Debug.Log("onPlayerNameChanged: OldName: "+PlayerName+" NewName: " + NewName);

		PlayerName = NewName;

		gameObject.name = "PlayerConnectionObject["+NewName+"]";
	}

	//////////////////////Commands (only on server)

	[Command]
	void CmdSpawnMyUnit() {
		GameObject go = Instantiate(PlayerUnitPrefab);
		//go.GetComponent<NetworkIdentity>().AssignClientAuthority(connectionToClient);
		NetworkServer.SpawnWithClientAuthority(go, connectionToClient);

	}

	[Command]
	void CmdChangePlayerName(string n){
		Debug.Log("CmdChangePlayerName: " + n);
		PlayerName = n;

		//RpcChangePlayerName(PlayerName);
	}


	/////////////////////RPC (Only activated on clients)
	//[ClientRpc]
	//void RpcChangePlayerName(string n){
	//	Debug.Log("RpcChangeServerName: Chainging player name on a particular PlayerConnectionObject" + n);
	//}

}
