using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OJM_Player : MonoBehaviour {

    /*
    jumping and movement
    sweep test for ludicrous speed
    decent turning with the analog stick conversion
    trigger reaction for touching the ground and being able to jump
    trigger reaction for 
    */
    private enum OJM_State { Ground, Air, Stun };
    public int State;
    private int StunFrames;
    private float BaseRun = 10;
    private float BaseTurnGround = 2f;
    private float BaseTurnAir = 0.045f;
    private float BaseJump = 12;
    public float Run;
    public float TurnGround;
    public float TurnAir;
    public float Jump;
    private float Horizontal;
    private float Vertical;
    public KeyCode JumpKey;
    public KeyCode LoadKey;
    public Rigidbody Body;
    private Quaternion LastRotation;
    private Vector3 LastPosition;
    private int LastState;

    private int JumpLeniency = 5;
    private int AirTime = 0;

    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("OOB")) {
            LoadLast();
            return;
        }
    }
    private void OnTriggerEnter(Collider other) {
        AirTime = 0;
        if (other.CompareTag("OOB")) {
            LoadLast();
            return;
        }
        if (State == (int)OJM_State.Stun) {
            return;
        }
        SaveLast();
    }
    private void OnTriggerStay(Collider other) {
        AirTime = 0;
        if (other.CompareTag("OOB")) {
            LoadLast();
            return;
        }
        if (State == (int)OJM_State.Stun) {
            return;
        }
        State = (int)OJM_State.Ground;
    }
    private void OnTriggerExit(Collider other) {
        if (State != (int)OJM_State.Stun) {
            AirTime = JumpLeniency;
        }
        State = (int)OJM_State.Air;
    }

    // Use this for initialization
    void Start () {
        Application.targetFrameRate = 60;
        JumpKey = KeyCode.Space;
        LoadKey = KeyCode.L;
        State = (int)OJM_State.Air;
        Run = BaseRun;
        TurnGround = BaseTurnGround;
        TurnAir = BaseTurnAir;
        Jump = BaseJump;
        SaveLast();
	}

    public void Stun(int frames) {
        State = (int)OJM_State.Stun;
        if (frames <= 0) frames = 1;
        StunFrames = frames;
    }

    public void SaveLast() {
        LastRotation = transform.rotation;
        LastPosition = transform.position;
        LastState = State;
    }

    public void LoadLast() {
        Body.velocity = Vector3.zero;
        transform.rotation = LastRotation;
        transform.position = LastPosition;
        State = LastState;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (Input.GetKeyDown(LoadKey)) {
            LoadLast();
        }
        if (State == (int)OJM_State.Stun) {
            StunFrames--;
            if (StunFrames == 0) {
                State = (int)OJM_State.Air;
            }
            return; // ===
        }

        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        if (State == (int)OJM_State.Air) {
            if (AirTime > 0) {
                AirTime--;
                if (Input.GetKeyDown(JumpKey)) {
                    Stun(5);
                    Body.velocity = Body.velocity + new Vector3(0, Jump, 0);
                }
            }
            transform.Rotate(0, TurnAir * Horizontal / 2 / Mathf.PI * 360, 0);
            float cos = Mathf.Cos(TurnAir * Horizontal);
            float sin = Mathf.Sin(TurnAir * Horizontal);
            Body.velocity = new Vector3(Body.velocity.x * cos + Body.velocity.z * sin, Body.velocity.y, Body.velocity.x * -sin + Body.velocity.z * cos);
        } else if (State == (int)OJM_State.Ground) {
            transform.Rotate(0, TurnGround * Horizontal, 0);
            Vector3 zed = transform.forward;
            zed = Vector3.Normalize(zed);
            Body.velocity = zed * (Run) * Vertical;
            if (Input.GetKeyDown(JumpKey)) {
                Stun(5);
                Body.velocity = Body.velocity + new Vector3(0, Jump, 0);
            }
        }
    }

}

/*
* CODE GRAVEYARD
            
// during ojmstate ground for strafe controls
Vector3 ex = Vector3.zero;
Vector3 ex = transform.right * Horizontal; 
Vector3 diff = Vector3.Normalize(zed + ex);
transform.position += diff * BaseRun;
            
*/
