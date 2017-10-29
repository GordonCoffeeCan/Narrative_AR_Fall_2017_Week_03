using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager instance;

    public bool outputPortalFound = false;
    public bool floorMapFound = false;
    public bool inputPortalFound = false;
    public bool pumpkinGoesIn = false;
    [SerializeField] private bool gotAPumpkin = false;
    [SerializeField] private bool isPumpkinOpened = false;

    [SerializeField] private Pumpkin pumpkin;
    [SerializeField] private Transform portal;
    [SerializeField] private float timer = 3f;

    [SerializeField] private Transform coins;
    [SerializeField] private Transform cash;
    [SerializeField] private Transform t_rex;
    [SerializeField] private Transform poop;

    [SerializeField] private Button resetGameBtn;

    private int luckyNumber = 0;

    private Pumpkin catchedPumpkin;

    private float currentTimer = 0;

    bool firstMessagePlayed;

    public DialogueTrigger myDialogueTrigger;

	// Use this for initialization
	void Start () {
        instance = this;
        luckyNumber = Random.Range(1, 15);
        coins.gameObject.SetActive(false);
        cash.gameObject.SetActive(false);
        t_rex.gameObject.SetActive(false);
        poop.gameObject.SetActive(false);

        if(resetGameBtn != null) {
            resetGameBtn.gameObject.SetActive(false);
        }
        firstMessagePlayed = false;
    }
	
	// Update is called once per frame
	void Update () {
        RaycastHit _hit;

        if (inputPortalFound && isPumpkinOpened == true) {
            resetGameBtn.gameObject.SetActive(true);
        }

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
                    myDialogueTrigger.TriggerDialogue(myDialogueTrigger.caughtPumpkin);
                }
            }
        }

        if(catchedPumpkin != null && pumpkinGoesIn == false && !inputPortalFound) {
            catchedPumpkin.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 2;
            catchedPumpkin.transform.rotation = Camera.main.transform.rotation;
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
                    myDialogueTrigger.TriggerDialogue(myDialogueTrigger.coinsOutcome);
                    break;
                case 10:
                    cash.gameObject.SetActive(true);
                    myDialogueTrigger.TriggerDialogue(myDialogueTrigger.coinsOutcome);
                    break;
                case 8:
                    t_rex.gameObject.SetActive(true);
                    myDialogueTrigger.TriggerDialogue(myDialogueTrigger.dinosaurOutcome);
                    break;
                default:
                    poop.gameObject.SetActive(true);
                    myDialogueTrigger.TriggerDialogue(myDialogueTrigger.poopOutcome);
                    break;
            }
        }
    }

    public void ResetGame() {
        SceneManager.LoadScene(0);
    }
}
