using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int level;

    public float[] position;
    public float[] rotation;

    public PlayerData(Transform playerTransform, PlayerStats playerStats)
    {
        level = playerStats.level;

        position = new float[] { playerTransform.position.x, playerTransform.position.y, playerTransform.position.z };
        rotation = new float[] { playerTransform.rotation.x, 
                                    playerTransform.rotation.y, 
                                    playerTransform.rotation.z, 
                                    playerTransform.rotation.w };
    }

    public void LoadPlayerData( Transform playerTransform, PlayerStats playerStats)
    {
        playerStats.level = level;

        playerTransform.position = new Vector3(position[0], position[1], position[2]);
        playerTransform.rotation = new Quaternion(rotation[0], rotation[1], rotation[2], rotation[3]);
    }

}
