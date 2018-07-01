using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostTile : MonoBehaviour {

    public float power;

    private void Start() {
        power = 500;
    }
    private void OnCollisionEnter(Collision collision) {
        
        if (collision.collider.GetComponentInParent<Rigidbody>() != null) {
            collision.collider.GetComponentInParent<Rigidbody>().AddForce(Vector3.Normalize(transform.up) * - 50);
        }
    }
    private void OnTriggerStay(Collider other) {
        //Debug.Log(transform.up);
        if (other.GetComponentInParent<Rigidbody>() != null) {
            other.GetComponentInParent<Rigidbody>().AddForce(Vector3.Normalize(transform.up) * power);
        }
    }
}
