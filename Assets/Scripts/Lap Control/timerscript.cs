using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerscript : MonoBehaviour {
    
    public float counter;

    public Text countertext;
	void Update () 
	{
		if(Input.GetKeyDown(KeyCode.Space))
        {
            counter -= 5;
        }
        countertext.text = counter.ToString();
	}

    private void OnTriggerEnter(Collider col)
    {
        countertext.text += "goofy";
    }
}
