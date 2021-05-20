using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class Inventory : MonoBehaviour
{
    [SerializeField] private List<Item> inventory = new List<Item>();
    [SerializeField] private bool showIMGUIInventory = true;
    private Item selectedItem = null;

    #region Canvas Inventory
    [SerializeField] private Button ButtonPrefab;
    [SerializeField] private GameObject InventoryGameObject;
    [SerializeField] private GameObject InventoryContent;
    [SerializeField] private GameObject FilterContent;
    #endregion

    #region Display Inventory
    private Vector2 scrollPosition;
    private string sortType = "All";
    #endregion

    #region now copy this
    private void Start()
    {
        //DisplayFiltersCanvas();
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            InventoryGameObject.SetActive(true);
            DisplayItemsCanvas();
        }
    }
    private void DisplayFiltersCanvas()
    {
        List<string> itemTypes = new List<string>(Enum.GetNames(typeof(Item.ItemType)));
        itemTypes.Insert(0, "All");

        for (int i = 0; i < itemTypes.Count; i++)
        {
            Button buttonGO = Instantiate<Button>(ButtonPrefab, FilterContent.transform);
            Text buttonText = buttonGO.GetComponentInChildren<Text>();
            buttonGO.name = itemTypes[i] + " filter";
            buttonText.text = itemTypes[i];

            //int x = i;
            //buttonGO.onClick.AddListener(() => { sortType = itemTypes[x];});
            buttonGO.onClick.AddListener(delegate { ChangeFilter(itemTypes[i]); });
        }
    }
    private void ChangeFilter(string itemType)
    {
        sortType = itemType;
        DisplayItemsCanvas();
    }
    void DestroyAllChildren(Transform parent)
    {
        foreach(Transform child in parent)
        {
            Destroy(child);
        }
    }
    private void DisplayItemsCanvas()
    {
        //DestroyAllChildren(InventoryContent.transform);
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Type.ToString() == sortType || sortType == "All")
            {
                Button buttonGO = Instantiate<Button>(ButtonPrefab, InventoryContent.transform);
                Text buttonText = buttonGO.GetComponentInChildren<Text>();
                buttonGO.name = inventory[i].Name + " button";
                buttonText.text = inventory[i].Name;

            }
        }
    }
    #endregion
    private void OnGUI()
    {
        if (showIMGUIInventory)
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

        GUI.Box(new Rect(Screen.width / 4, (Screen.height / 3) + (Screen.height /5),
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
                }
                count++;
            }
        }
        GUI.EndScrollView();
    }
}
