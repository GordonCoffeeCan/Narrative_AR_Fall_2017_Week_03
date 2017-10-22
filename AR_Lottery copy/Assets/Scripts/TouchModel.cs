//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class TouchModel : MonoBehaviour {

//	// Use this for initialization
//	void Start () {
		
//	}
	
//	// Update is called once per frame
//	void Update () {
//        if (Input.touchCount > 0 && Input.GetTouch((0).phase == TouchPhase.Began)
//        {
//            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
//            RaycastHit hit;

//            if (Physics.Raycast(ray, out hit) && hit.transform.gameObject.name == "myGameObjectName")
//            {
//                hit.GetComponent<TouchObjectScript>().ApplyForce();
//            }
//        }
//	}
//}
