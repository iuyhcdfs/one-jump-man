using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class OJM_Player : MonoBehaviour {

    public Text runtext, jumptext, loadtext;
    private int loadtextcount = 0;

    /*
    jumping and movement
    sweep test for ludicrous speed
    decent turning with the analog stick conversion
    trigger reaction for touching the ground and being able to JumpStat
    trigger reaction for 
    */
    private enum OJM_State { Ground, Air, Stun };
    public int State;
    private int StunFrames;

    // change me, change the display run display jump please
    private float BaseRun = 4;//10
    private float BaseJump = 6;//12
    private float BaseTurnGround = 2f;
    private float BaseTurnAir = 0.045f;
    public float RunStat;
    public float TurnGround;
    public float TurnAir;
    public float JumpStat;

    private float Horizontal;
    private float Vertical;
    public KeyCode JumpKey;
    public KeyCode LoadKey;
    public KeyCode ResetKey;
    public Rigidbody Body;
    private Quaternion LastRotation;
    private Vector3 LastPosition;
    private Quaternion LastRotation2;
    private Vector3 LastPosition2;
    private int LastState;

    private int JumpLeniency = 7;
    private int AirTime = 0;
    public CameraFollow cam;

    private float ExerciseJump = 2.1f;
    private float ExerciseJumpMash = 0.32f;
    
    private void OnCollisionEnter(Collision collision) {
        if (collision.collider.CompareTag("OOB")) {
            LoadLast();
            return;
        }
    }/*
    public void TriggerEnter(Collider other) {
        if (other.CompareTag("OOB")) {
            LoadLast();
            return;
        }
        AirTime = 0;

        if (State == (int)OJM_State.Stun) {
            return;
        }
    }*/
    public void TriggerStay(Collider other) {
        AirTime = 0;
        if (other.CompareTag("OOB")) {
            LoadLast();
            return;
        }
        if (State == (int)OJM_State.Stun) {
            return;
        }
        if (!other.CompareTag("slide")) {
            State = (int)OJM_State.Ground;
            cam.Ground();
        }
    }
    public void TriggerExit(Collider other) {
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
        ResetKey = KeyCode.R;
        State = (int)OJM_State.Air;
        RunStat = BaseRun;
        TurnGround = BaseTurnGround;
        TurnAir = BaseTurnAir;
        JumpStat = BaseJump;
        SaveLast();
	}

    public void Stun(int frames) {
        State = (int)OJM_State.Stun;
        if (frames <= 0) frames = 1;
        StunFrames = frames;
    }

    public void SaveLast() {
        LastRotation2 = LastRotation;
        LastPosition2 = LastPosition;
        LastRotation = transform.rotation;
        LastPosition = transform.position;
        LastState = State;
    }

    public void LoadLast() {
        loadtext.text = "Reloads: " + ++loadtextcount;
        Body.velocity = Vector3.zero;
        transform.rotation = LastRotation2;
        // new
        transform.position = LastPosition2 + new Vector3(0,1,0);
        State = (int)OJM_State.Air;
        //State = LastState;
    }

    private void Jump() {
        cam.Jump();
        Stun(5);
        JumpStat += ExerciseJump;
        jumptext.color = new Color(0, 120, 120);
        Body.velocity = Body.velocity + new Vector3(0, JumpStat, 0);
    }

    // Update is called once per frame
    void FixedUpdate() {

        if (Input.GetKeyDown(ResetKey)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }

        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");

        
        if (Input.GetKeyDown(JumpKey)) {
            JumpStat += ExerciseJumpMash/2;
            jumptext.color = new Color(0, 120, 120);
        }

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


        if (State == (int)OJM_State.Air) {
            runtext.color = new Color(50, 50, 50);
            if (AirTime > 0) {
                AirTime--;
                if (Input.GetKeyDown(JumpKey)) {
                    Jump();
                }
            }
            if (Input.GetKeyDown(JumpKey)) {
                JumpStat += ExerciseJump / 4;
                jumptext.color = new Color(0, 255, 0);
            }
            transform.Rotate(0, TurnAir * Horizontal / 2 / Mathf.PI * 360, 0);
            float cos = Mathf.Cos(TurnAir * Horizontal);
            float sin = Mathf.Sin(TurnAir * Horizontal);
            Body.velocity = new Vector3(Body.velocity.x * cos + Body.velocity.z * sin, Body.velocity.y, Body.velocity.x * -sin + Body.velocity.z * cos);

        } else if (State == (int)OJM_State.Ground) {
            jumptext.color = new Color(50, 50, 50);
            cam.Ground();
            if (Vertical != 0) {
                runtext.color = new Color(0, 255, 0);
                RunStat += 0.15f;
            } else {
                
            }
            transform.Rotate(0, TurnGround * Horizontal, 0);
            Vector3 zed = transform.forward;
            zed = Vector3.Normalize(zed);
            Body.velocity = zed * (RunStat) * Vertical;
            if (Input.GetKeyDown(JumpKey)) {
                Jump();
            }
        }
    }

}

/*
* CODE GRAVEYARD
            
        if (collision.collider.CompareTag("boost")) {
            return;
        }
// during ojmstate ground for strafe controls
Vector3 ex = Vector3.zero;
Vector3 ex = transform.right * Horizontal; 
Vector3 diff = Vector3.Normalize(zed + ex);
transform.position += diff * BaseRun;
            
*/
