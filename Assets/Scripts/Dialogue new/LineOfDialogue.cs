using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LineOfDialogue
{
    public string statement, response;

    public float minApproval = -1f;
    public float changeApproval = 0f;

    public Quest[] quest;
    public Quest[] mustHaveQuest;//come back to it
    public Quest[] completeQuests;


    public Dialogue nextDialogue;

    [System.NonSerialized]
    public int buttonID;

}
