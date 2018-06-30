using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinkingDeadzone : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.position = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
	}
}
