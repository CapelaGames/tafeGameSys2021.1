using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public string faction;
    public string greeting;
    public LineOfDialogue goodBye;
    public LineOfDialogue[] dialogueOptions;

    public bool firstDialogue;

    private void Update()
    {
        //only if you havn't done a raycast
        if (!firstDialogue) return;
        if(Input.GetKeyDown(KeyCode.E))
        {
            DialogueManager.instance.LoadDialogue(this);
        }
    }

}
