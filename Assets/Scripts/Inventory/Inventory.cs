using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> inventory = new List<Item>();

    [SerializeField] private bool showInventory = true;


    private void OnGUI()
    {
        if(showInventory)
        {
            GUI.Box(new Rect(0,0,Screen.width, Screen.height), "");

            List<string> itemTypes = new List<string>(Enum.GetNames(typeof(Item.ItemType)));
            itemTypes.Insert(0,"All");

            for (int i = 0; i < itemTypes.Count ; i++)
            {
                if (GUI.Button(new Rect(
                      (Screen.width / itemTypes.Count) * i
                    , 10
                    , Screen.width / itemTypes.Count
                    , 20), itemTypes[i]))
                {
                    Debug.Log(itemTypes[i]);
                }
            }
        }
    }
}
