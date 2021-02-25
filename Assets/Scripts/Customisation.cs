using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customisation : MonoBehaviour
{
    private void OnGUI()
    {
        CustomiseOnGUI();
    }

    private void CustomiseOnGUI()
    {
        float currentY = 40;
        int i = 0;


        //skin
        if (GUI.Button(new Rect(20, currentY, 20, 20), "<"))
        {
            Debug.Log("Left");
        }
        GUI.Label(new Rect(45, currentY, 60, 20), "Skin");
        if (GUI.Button(new Rect(90, currentY, 20, 20), ">"))
        {
            Debug.Log("Right");
        }
        currentY += 30;

        //eyes
        if (GUI.Button(new Rect(20, currentY, 20, 20), "<"))
        {
            Debug.Log("Left");
        }
        GUI.Label(new Rect(45, currentY, 60, 20), "Eyes");
        if (GUI.Button(new Rect(90, currentY, 20, 20), ">"))
        {
            Debug.Log("Right");
        }
        currentY += 30;

        //mouth
        if (GUI.Button(new Rect(20, currentY, 20, 20), "<"))
        {
            Debug.Log("Left");
        }
        GUI.Label(new Rect(45, currentY, 60, 20), "Mouth");
        if (GUI.Button(new Rect(90, currentY, 20, 20), ">"))
        {
            Debug.Log("Right");
        }
        currentY += 30;

        //hair
        if (GUI.Button(new Rect(20, currentY, 20, 20), "<"))
        {
            Debug.Log("Left");
        }
        GUI.Label(new Rect(45, currentY, 60, 20), "Hair");
        if (GUI.Button(new Rect(90, currentY, 20, 20), ">"))
        {
            Debug.Log("Right");
        }
        currentY += 30;
    }
}
