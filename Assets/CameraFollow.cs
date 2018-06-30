using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform Player;
    public Vector3 GoalPosition; // 0,5,7
    public Quaternion GoalRotation;// 30,0,0
    public float speed = 42000F;
    public Vector3 JumpPosition;
    public Quaternion JumpRotation;
    int target;
    float timestamp;
    float interp;
    float jumpaccel;
    // oi screw it for now we just subgroup the player.
    public Vector3 BirdPosition;
    public Quaternion BirdRotation;

    // Use this for initialization
    void Start () {
		if(Player == null) {
            Debug.Log("oi give main camera the player");
            Debug.Break();
        }
        target = 0;
        interp = 0;
        GoalPosition = new Vector3(0, 5, -7);
        GoalRotation = Quaternion.Euler(30, 0, 0);
        JumpPosition = new Vector3(0, 9f, -5);
        JumpRotation = Quaternion.Euler(40, 0, 0);
        BirdPosition = new Vector3(0, 11, 0);
        BirdRotation = Quaternion.Euler(80, 0, 0);
}

    public void Jump() {
        if (target == 0) {
            interp = 0.2f;
            jumpaccel = 0.02f;
        }
        target = 1;
    }
    public void Ground() {
        if (target == 1) {
            interp = 0.6f;
        }
        target = 0;
    }

    public void MaxJump(float inter) {
        interp = inter;
    }
    private void FixedUpdate() {
        RaycastHit hit;

        if (target == 1) {
            if (Physics.Raycast(JumpPosition, Player.position - JumpPosition, out hit, Mathf.Infinity, ~0)) {
                if (!hit.collider.CompareTag("Player")) {
                    transform.localPosition = BirdPosition;
                    transform.localRotation = BirdRotation;
                    return;
                }
            }
            transform.localPosition = Vector3.Lerp(GoalPosition, JumpPosition, interp);
            transform.localRotation = Quaternion.Lerp(GoalRotation, JumpRotation, interp);
            //transform.localPosition = GoalPosition;
            //transform.localRotation = GoalRotation;
            interp += jumpaccel;
            jumpaccel += 0.08f;
        } else if (target == 0) {
            if (Physics.Raycast(GoalPosition, Player.position - GoalPosition, out hit, Mathf.Infinity, ~0)) {
                if (!hit.collider.CompareTag("Player")) {
                    transform.localPosition = BirdPosition;
                    transform.localRotation = BirdRotation;
                    return;
                }
            }
            transform.localPosition = Vector3.Lerp(JumpPosition, GoalPosition, interp);
            transform.localRotation = Quaternion.Lerp(JumpRotation, GoalRotation, interp);
            //            transform.localRotation = JumpRotation;
            //            transform.localPosition = JumpPosition;
            interp += 0.17f;
        }
    }
}
