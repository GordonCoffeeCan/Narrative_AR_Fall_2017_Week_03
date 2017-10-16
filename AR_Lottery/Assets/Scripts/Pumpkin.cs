using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pumpkin : MonoBehaviour {

    [HideInInspector] public bool simulatePhysics = true;
    [HideInInspector] public bool selfDestroy = true;

    private float destroyTimer = 3;
    private float gravity = 9.8f;
    private Rigidbody rig;

    //private float currentGravitySpeed = 0;

	// Use this for initialization
	void Start () {
        rig = this.GetComponent<Rigidbody>();

        simulatePhysics = true;
        selfDestroy = true;
}
	
	// Update is called once per frame
	void Update () {
        if (selfDestroy) {
            destroyTimer -= Time.deltaTime;

            if (destroyTimer <= 0) {
                Destroy(this.gameObject);
            }
        }

        Debug.Log(simulatePhysics);

        if (simulatePhysics == false) {
            rig.isKinematic = true;
        }
    }

    private void FixedUpdate() {
        if (simulatePhysics) {
            rig.AddForce(Vector3.back * gravity, ForceMode.Acceleration);
        }
    }
}
