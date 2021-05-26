using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> inventory = new List<Item>();
    [SerializeField] private bool showInventory = true;
    private Item selectedItem = null;

    #region Display Inventory
    private Vector2 scrollPosition;
    private string sortType = "All";
    #endregion

    public void AddItem(Item _item)
    {
        inventory.Add(_item);
    }

    public void RemoveItem(Item _item)
    {
        if(inventory.Contains(_item))
            inventory.Remove(_item);
    }

    private void OnGUI()
    {
        if (showInventory)
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
                    //Debug.Log(itemTypes[i]);
                    sortType = itemTypes[i];
                }
            }
            Display();

            if(selectedItem != null)
            {
                DisplaySelectedItem();
            }
        }
        
    }
    private void DisplaySelectedItem()
    {
        GUI.Box(new Rect(Screen.width/4, Screen.height /3,
            Screen.width / 5, Screen.height / 5), 
            selectedItem.Icon);
        GUI.Box(new Rect(Screen.width / 4, (Screen.height / 3) + (Screen.height /5)  ,
            Screen.width / 7, Screen.height / 15),
            selectedItem.Name);
        GUI.Box(new Rect(Screen.width / 4, (Screen.height / 3) + (Screen.height / 3),
            Screen.width / 5, Screen.height / 5), selectedItem.Description +
                    "\nValue: " + selectedItem.Value +
                    "\nAmount: " + selectedItem.Amount);

    }

    private void Display()
    {
        scrollPosition = GUI.BeginScrollView(new Rect(0, 40, Screen.width, Screen.height - 40),
            scrollPosition,
            new Rect(0, 0, 0, inventory.Count * 30),
            false,
            true);

        int count = 0;
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Type.ToString() == sortType || sortType == "All")
            {
                if (GUI.Button(new Rect(30, 0 + (count * 30), 200, 30), inventory[i].Name))
                {
                    selectedItem = inventory[i];
                    selectedItem.OnClicked();
                }
                count++;
            }
        }
        GUI.EndScrollView();
    }


}
