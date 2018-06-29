using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform Player;
    public Vector3 GoalPosition; // 0,5,7
    public Vector3 GoalRotation;// 30,0,0

    // oi screw it for now we just subgroup the player.

    /*
	// Use this for initialization
	void Start () {
		if(Player == null) {
            Debug.Log("oi give main camera the player");
            Debug.Break();
        }
	}
    
    private void FixedUpdate() {
        RaycastHit hit;
        if (Physics.Linecast(transform.position, Player.position, out hit)) {
            if (hit.transform.tag != "Player") {
                transform.position *= 0.98f;
                GoalRotation *= 0.98f;
            } else {
                transform.position = GoalPosition;
                transform.rotation = Quaternion.Euler(GoalRotation);
            }
        }
    }
    */
}
