using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour {

    [HideInInspector] public float gravity = 9.8f;
    private Rigidbody rig;

    //private float currentGravitySpeed = 0;

	// Use this for initialization
	void Start () {
        rig = this.GetComponent<Rigidbody>();
    }
	
	// Update is called once per frame
	void Update () {
        
    }

    private void FixedUpdate() {
        rig.AddForce(Vector3.back * gravity, ForceMode.Acceleration);
    }
}
