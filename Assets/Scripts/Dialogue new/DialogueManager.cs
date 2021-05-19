using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;


public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance;//alternatve is :new class instant

    private Dialogue loadedDialogue;

    [SerializeField] GameObject buttonPrefab;
    [SerializeField] Transform dialogueButtonPanel;
    [SerializeField] Text responseText;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

    }

    public void LoadDialogue(Dialogue dialogue)
    {
        transform.GetChild(0).gameObject.SetActive(true);
        loadedDialogue = dialogue;
        ClearButtons();

        Button spawnedButton;
        int index = 0;
        foreach (LineOfDialogue data in dialogue.dialogueOptions)
        {
            float? currentApproval = FactionsManager2.instance.FactionsApproval(loadedDialogue.faction);
            if (currentApproval != null && currentApproval > data.minApproval)
            {
                spawnedButton = Instantiate(buttonPrefab, dialogueButtonPanel).GetComponent<Button>();
                spawnedButton.GetComponentInChildren<Text>().text = data.statement;
                data.buttonID = index;
                spawnedButton.onClick.AddListener(() => ButtonPressed(data.buttonID));
            }
            index++; //() same as delegate
        }

        spawnedButton = Instantiate(buttonPrefab, dialogueButtonPanel).GetComponent<Button>();
        spawnedButton.GetComponentInChildren<Text>().text = dialogue.goodBye.statement;
        dialogue.goodBye.buttonID = index;
        spawnedButton.onClick.AddListener(() => EndConversation());

        DisplayResponse(loadedDialogue.greeting);
    }

    private void EndConversation()
    {
        ClearButtons();
        DisplayResponse(loadedDialogue.goodBye.response);

        if (loadedDialogue.goodBye.nextDialogue != null)
        {
            LoadDialogue(loadedDialogue.goodBye.nextDialogue);
        }
        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        
    }

    private void ButtonPressed(int index)
    {
        //changes approval
        FactionsManager2.instance.FactionsApproval(loadedDialogue.faction,
            loadedDialogue.dialogueOptions[index].changeApproval);


        //adds quest
        if (loadedDialogue.dialogueOptions[index].quest.Length > 0)
        {
            foreach (Quest quest in loadedDialogue.dialogueOptions[index].quest)
            {
                if (quest != null)
                {
                    QuestManager.instance.AccpectQuest(quest);
                }
            }
        }

        //complete quest
        if (loadedDialogue.dialogueOptions[index].completeQuests.Length > 0)
        {
            foreach (Quest quest in loadedDialogue.dialogueOptions[index].completeQuests)
            {
                if(quest != null)
                {
                    QuestManager.instance.CompleteQuest(quest);
                }
            }
        }

        //moves on to the next dialogue
        if (loadedDialogue.dialogueOptions[index].nextDialogue != null)
        {
            LoadDialogue(loadedDialogue.dialogueOptions[index].nextDialogue);
        }
        else
        {
            DisplayResponse(loadedDialogue.dialogueOptions[index].response);
        }
    }

    private void DisplayResponse(string response)
    {
        //print(response);
        //Debug.Log(response);
        responseText.text = response;
    }

    private void ClearButtons()
    {
        foreach (Transform child in dialogueButtonPanel)
        {
            Destroy(child.gameObject);
        }
    }












}