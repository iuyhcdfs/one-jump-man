using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ALevel : MonoBehaviour {

    public Scene next;
    public Transform prefab;
    Transform[] temp;
    
    private void OnTriggerEnter(Collider other) {
        // new scene    
    }

    void Start () {
        int total = 5 * 50;
        temp = new Transform[total];
        int count = 0;
        for (int x = -2; x <= 2; ++x) {
            for (int z = 5; z < 55; ++z) {
                temp[count] = GameObject.Instantiate<Transform>(prefab);
                temp[count].SetParent(transform);
                temp[count].localPosition = new Vector3(x, 0.05f, z);
                temp[count].localRotation = Quaternion.Euler(90,180,0);
                count++;
            }
        } 
	}
	
}
