 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGroundSave : MonoBehaviour {

    // subobjects as components begins here.
    // its unfortunately we cant differentiate between triggers.
    OJM_Player parent;

    private void Start() {
        parent = transform.parent.gameObject.GetComponent<OJM_Player>();
    }
    
    private void OnTriggerEnter(Collider other) {
        if (!other.CompareTag("fkncylinder") && !other.CompareTag("OOB") && !other.CompareTag("slide")) {
            parent.SaveLast();
        }
    }

}
