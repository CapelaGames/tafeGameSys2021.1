using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;

    public float[] position;
    public float[] rotation;

    public Stats strength;      
    public Stats dexterity;     
    public Stats constitution;  
    public Stats wisdom;        
    public Stats intelligence;  
    public Stats charisma;      

    public PlayerData(Transform playerTransform, PlayerStats playerStats)
    {
        level = playerStats.level;

        strength        = playerStats.strength;
        dexterity       = playerStats.dexterity;
        constitution    = playerStats.constitution;
        wisdom          = playerStats.wisdom;
        intelligence    = playerStats.intelligence;
        charisma        = playerStats.charisma;

        //position = new float[] { playerTransform.position.x, playerTransform.position.y, playerTransform.position.z };
        //rotation = new float[] { playerTransform.rotation.x, 
        //                            playerTransform.rotation.y, 
        //                            playerTransform.rotation.z, 
        //                            playerTransform.rotation.w };
    }

    public void LoadPlayerData( Transform playerTransform, PlayerStats playerStats)
    {
        playerStats.level = level;

        playerStats.strength = strength;
        playerStats.dexterity = dexterity;
        playerStats.constitution = constitution;
        playerStats.wisdom = wisdom;
        playerStats.intelligence = intelligence;
        playerStats.charisma = charisma;

        //playerTransform.position = new Vector3(position[0], position[1], position[2]);
        //playerTransform.rotation = new Quaternion(rotation[0], rotation[1], rotation[2], rotation[3]);
    }


    void Start()
    {
        myClass something = new myClass();
        //x==4
        something.x = 4;
        another(something);
        //x ==5
    }

    void another(myClass classExample)
    {
        //x ==4
        classExample.x++;
        //x==5
    }
}


public class myClass
{
    public int x;
}