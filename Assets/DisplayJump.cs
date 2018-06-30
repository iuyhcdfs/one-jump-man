using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayJump : MonoBehaviour {
    public Text text;
    public OJM_Player player;
    private float BaseJump = 6;//12

    float BaseSize;
    private void Start() {
        BaseSize = text.fontSize;
    }
    void Update () {
        text.text = "Jump power: " + player.JumpStat.ToString();
        text.fontSize = (int) ( BaseSize + (player.JumpStat - BaseJump));
    }
}
