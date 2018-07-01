using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayRun : MonoBehaviour {
    public Text text;
    public OJM_Player player;
    private float BaseRun = 4;//10
    float BaseSize;
    private void Start() {
        BaseSize = text.fontSize;
    }
    void Update() {
        text.text = "Run power : " + player.RunStat.ToString();
        text.fontSize = (int)(BaseSize + (player.RunStat - BaseRun));
        if (text.fontSize > 44) {
            text.fontSize = 44;
        }
    }
}
