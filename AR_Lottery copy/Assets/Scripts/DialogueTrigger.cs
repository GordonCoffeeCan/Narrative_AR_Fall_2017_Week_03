using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour {

    public Dialogue firstArcade, caughtPumpkin, atFloorMap, poopOutcome, dinosaurOutcome, coinsOutcome;

    public void Start() {

    }

    /*
    public void TriggerNextDialogue()
    {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogues[dialogueIndex]);
        dialogueIndex++;
    }
    */

    public void TriggerDialogue(Dialogue dialogue) {
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }


}
