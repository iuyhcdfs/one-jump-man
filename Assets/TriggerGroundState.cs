using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerGroundState : MonoBehaviour {

    // subobjects as components begins here.
    // its unfortunately we cant differentiate between triggers.
    OJM_Player parent;

    private void Start() {
        parent = transform.parent.gameObject.GetComponent<OJM_Player>();
    }
    
    private void OnTriggerExit(Collider other) {
        parent.TriggerExit(other);
    }
    private void OnTriggerStay(Collider other) {
        parent.TriggerStay(other);
    }
}
