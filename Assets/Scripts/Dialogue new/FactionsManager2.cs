using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Factions2
{
    public string factionName;
    [SerializeField, Range(-1,1)]
    float _approval;
    public float approval
    {
        set
        {
            _approval = Mathf.Clamp(value, -1, 1);
        }
        get
        {
            return _approval;
        }
    }
    public Factions2(float initialApproval)
    {
        approval = initialApproval;
    }

}

public class FactionsManager2 : MonoBehaviour
{
    //[SerializeField]
    Dictionary<string, Factions2> factions;
    [SerializeField]
    List<Factions2> CreateTheseFactions;

    public static FactionsManager2 instance;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }

        factions = new Dictionary<string, Factions2>();
        foreach (Factions2 faction in CreateTheseFactions)
        {
            factions.Add(faction.factionName, new Factions2(faction.approval));
        } 
    }

    public float? FactionsApproval(string factionName, float value)
    {
        if(factions.ContainsKey(factionName))
        {
            factions[factionName].approval += value;
            return factions[factionName].approval;

        }
        return null;
    }

    public float? FactionsApproval(string factionName)
    {
        if(factions.ContainsKey(factionName))
        {
            return factions[factionName].approval;
        }

        return null;
    }
}