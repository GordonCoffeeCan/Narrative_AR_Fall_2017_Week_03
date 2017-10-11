using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttatchToDo : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision) {

        if (this.gameObject.name == "T_Rex") {
            if (collision.gameObject.name == "Mushroom_01") {
                Debug.Log("eating!");
            }
        } else if (this.gameObject.name == "Mushroom_01") {
            if (collision.gameObject.name == "T_Rex") {
                Debug.Log("eating!");
            }
        }

        
    }
}
