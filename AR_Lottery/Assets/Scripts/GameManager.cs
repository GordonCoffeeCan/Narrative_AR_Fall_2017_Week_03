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

    private int luckyNumber = 0;

    public bool outputPortalFound = false;
    public bool floorMapFound = false;
    public bool inputPortalFound = false;

    private float currentTimer = 0;

	// Use this for initialization
	void Start () {
        instance = this;
        TestLuck();
    }
	
	// Update is called once per frame
	void Update () {
        if (outputPortalFound) {
            currentTimer -= Time.deltaTime;

            if (currentTimer <= 0) {
                float _force = Random.Range(0.5f, 2.5f);
                float _dirX = Random.Range(-1.5f, 1.5f);
                float _dirZ = Random.Range(3f, 5f);
                Pumpkin _pumpkinClone;
                _pumpkinClone = (Pumpkin)Instantiate(pumpkin, portal.position, Quaternion.identity);
                _pumpkinClone.GetComponent<Rigidbody>().AddForceAtPosition(new Vector3(_dirX, _force, _dirZ), _pumpkinClone.transform.position, ForceMode.Impulse);
                Destroy(_pumpkinClone.gameObject, 3);
                currentTimer = timer;
            }
        }
	}

    private void TestLuck() {
        luckyNumber = Random.Range(1, 20);

        coins.gameObject.SetActive(false);
        cash.gameObject.SetActive(false);
        t_rex.gameObject.SetActive(false);
        poop.gameObject.SetActive(false);

        switch (luckyNumber) {
            case 5:
                coins.gameObject.SetActive(true);
                break;
            case 16:
                cash.gameObject.SetActive(true);
                break;
            case 20:
                t_rex.gameObject.SetActive(true);
                break;
            default:
                poop.gameObject.SetActive(true);
                break;
        }
    }


}
