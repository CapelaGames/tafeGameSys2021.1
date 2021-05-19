using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public List<Quest> CurrentQuests;

    static public QuestManager instance;

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

    public void Update()
    {
        //This could be in your character scripts instead
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            ViewQuests();
        }


    }

    public void ViewQuests()
    {
        foreach (Quest quest in CurrentQuests)
        {
            Debug.Log("Quest Name: " + quest.QuestName
                        + "\n Description: " + quest.Discription
                        + "\n Reward Xp: " + quest.XPReward);
        }
    }

    public void AccpectQuest(Quest quest)
    {
        CurrentQuests.Add(quest);
    }

    public void CompleteQuest(Quest quest)
    {
        //accpet reward

        CurrentQuests.Remove(quest);
    }
}
