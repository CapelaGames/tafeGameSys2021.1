using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDialogue : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, 20))
            {
                //if (hit.transform.tag == "NPC")
                {
                    Dialogue[] npcDialogue = hit.transform.GetComponents<Dialogue>();
                    if (npcDialogue != null)
                    {
                        foreach (Dialogue dialogue in npcDialogue)
                        {
                            if (dialogue.firstDialogue == true)
                            {
                                Cursor.lockState = CursorLockMode.Confined;
                                Cursor.visible = true;

                                DialogueManager.instance.LoadDialogue(dialogue);
                            }
                        }
                    }

                }
            }
        }
    }

}