using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayOnline : MonoBehaviour {

	public void LoadScene()
	{
		SceneManager.LoadScene("Network");
	}

}
