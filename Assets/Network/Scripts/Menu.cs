using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour {

	private string CurMenu;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void ToMenu(string menu){
		CurMenu = menu;
	}

	void OnGUI(){
		if(CurMenu == "Main")
			Main();
		if(CurMenu == "Host")
			Host();
	}

	private void Main(){
		if(GUI.Button(new Rect(0,0,128,32),"Host a Match"))
			ToMenu("Host");
	}

	private void Host(){

	}
}
