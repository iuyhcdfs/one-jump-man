using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showvertical : MonoBehaviour {

    public Text text;
    
	
	// Update is called once per frame
	void Update () {
        text.text = Input.GetAxis("Vertical").ToString();
	}
}
