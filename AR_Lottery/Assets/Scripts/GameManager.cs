using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    [SerializeField] private Pumpkin pumpkin;
    [SerializeField] private Transform portal;
    [SerializeField] private float timer = 3f;

    [SerializeField] private Transform coins;
    [SerializeField] private Transform cash;
    [SerializeField] private Transform t_rex;
    [SerializeField] private Transform poop;

    [SerializeField] private bool gotAPumpkin = false;
    [SerializeField] private bool isPumpkinOpened = false;

    public bool outputPortalFound = false;
    public bool floorMapFound = false;
    public bool inputPortalFound = false;
    public bool pumpkinGoesIn = false;

    private int luckyNumber = 0;

    private Pumpkin catchedPumpkin;

    private float currentTimer = 0;

	// Use this for initialization
	void Start () {
        instance = this;
        luckyNumber = Random.Range(1, 15);
        coins.gameObject.SetActive(false);
        cash.gameObject.SetActive(false);
        t_rex.gameObject.SetActive(false);
        poop.gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit _hit;

        ResetGame();

        if (outputPortalFound && gotAPumpkin == false) {
            currentTimer -= Time.deltaTime;
            if (currentTimer <= 0) {
                float _force = Random.Range(0.5f, 2.5f);
                float _dirX = Random.Range(-1.5f, 1.5f);
                float _dirZ = Random.Range(1f, 2.5f);
                Pumpkin _pumpkinClone;

                _pumpkinClone = (Pumpkin)Instantiate(pumpkin, portal.position, Quaternion.identity);
                _pumpkinClone.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(_dirX, _force, _dirZ), _pumpkinClone.transform.position, ForceMode.Impulse);
                currentTimer = timer;
            }
        }

        if (Input.GetMouseButtonDown(0) && gotAPumpkin == false) {
            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out _hit)) {
                if(_hit.collider.tag == "Pumpkin") {
                    catchedPumpkin = _hit.collider.transform.GetComponent<Pumpkin>();
                    catchedPumpkin.simulatePhysics = false;
                    catchedPumpkin.selfDestroy = false;
                    gotAPumpkin = true;
                }
            }
        }

        if(catchedPumpkin != null && pumpkinGoesIn == false) {
            catchedPumpkin.transform.position = Vector3.Lerp(catchedPumpkin.transform.position, Camera.main.transform.forward * 0.5f, 0.15f);
            catchedPumpkin.transform.rotation = Quaternion.Slerp(catchedPumpkin.transform.rotation, Quaternion.Euler(Vector3.zero), 0.15f);
        }

        if (inputPortalFound) {
            if (pumpkinGoesIn) {
                catchedPumpkin.transform.position = Vector3.Lerp(catchedPumpkin.transform.position, Camera.main.transform.forward * 5, 0.035f);

                if (Vector3.Distance(catchedPumpkin.transform.position, Camera.main.transform.position) > 4.5f ) {
                    Destroy(catchedPumpkin.gameObject);
                    isPumpkinOpened = true;
                }
            }
        }

        TestLuck();
    }

    private void TestLuck() {
        if (isPumpkinOpened) {
            switch (luckyNumber) {
                case 3:
                    coins.gameObject.SetActive(true);
                    break;
                case 10:
                    cash.gameObject.SetActive(true);
                    break;
                case 8:
                    t_rex.gameObject.SetActive(true);
                    break;
                default:
                    poop.gameObject.SetActive(true);
                    break;
            }
        }
    }

    private void ResetGame() {
        if (outputPortalFound && isPumpkinOpened == true) {
            gotAPumpkin = false;
            isPumpkinOpened = false;
            pumpkinGoesIn = false;

            if (catchedPumpkin != null) {
                catchedPumpkin.simulatePhysics = true;
                catchedPumpkin.selfDestroy = true;
                gotAPumpkin = false;
            }

            luckyNumber = Random.Range(1, 15);
            coins.gameObject.SetActive(false);
            cash.gameObject.SetActive(false);
            t_rex.gameObject.SetActive(false);
            poop.gameObject.SetActive(false);
        }
    }
}
