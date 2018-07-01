using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CylinderLevelC : MonoBehaviour {

    public float EulerSpeed;

	// Use this for initialization
	void Start () {
        EulerSpeed = -0.3f;
	}
	
	// Update is called once per frame
	void Update () {
        transform.rotation = transform.rotation * Quaternion.Euler(0, EulerSpeed, 0);
	}
}
