using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour {
    public string NextLevel;
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Player") && NextLevel != null) {
            SceneManager.LoadScene(NextLevel);
        }
    }
}
