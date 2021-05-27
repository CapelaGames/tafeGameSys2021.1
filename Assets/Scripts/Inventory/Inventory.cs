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
    [NonSerialized] public Item selectedItem = null;

    #region Canvas Inventory
    [SerializeField] private Button ButtonPrefab;
    [SerializeField] private GameObject InventoryGameObject;
    [SerializeField] private GameObject InventoryContent;
    [SerializeField] private GameObject FilterContent;

    [Header("Selected Item Display")]
    [SerializeField] private RawImage itemImage;
    [SerializeField] private Text itemName;
    [SerializeField] private Text ItemDescription;
    #endregion

    #region Display Inventory
    private Vector2 scrollPosition;
    private string sortType = "All";
    #endregion

    public void AddItem(Item _item)
    {
        AddItem(_item, _item.Amount);
    }

    public void AddItem(Item _item, int count)
    {
        Item foundItem = inventory.Find((x)=> x.Name == _item.Name);

        if(foundItem == null)
        {
            inventory.Add(_item);
        }
        else
        {
            foundItem.Amount += count;
        }
        DisplayItemsCanvas();
        DisplaySelectedItemOnCanvas(selectedItem);
    }

    public void RemoveItem(Item _item)
    {
        if (inventory.Contains(_item))
            inventory.Remove(_item);

        DisplayItemsCanvas();
        DisplaySelectedItemOnCanvas(selectedItem);
    }

    #region now copy this
    private void Start()
    {
        DisplayFiltersCanvas();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            if (InventoryGameObject.activeSelf)
            {
                InventoryGameObject.SetActive(false);
            }
            else
            {
                InventoryGameObject.SetActive(true);
                DisplayItemsCanvas();
            }
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

            int x = i;
            //buttonGO.onClick.AddListener(() => { ChangeFilter(itemTypes[x]); });
            buttonGO.onClick.AddListener(delegate { ChangeFilter(itemTypes[x]); });
        }
    }
    private void ChangeFilter(string itemType)
    {
        sortType = itemType;
        DisplayItemsCanvas();
    }
    void DestroyAllChildren(Transform parent)
    {
        foreach (Transform child in parent)
        {
            Destroy(child.gameObject);
        }
    }
    private void DisplayItemsCanvas()
    {
        DestroyAllChildren(InventoryContent.transform);
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Type.ToString() == sortType || sortType == "All")
            {
                Button buttonGO = Instantiate<Button>(ButtonPrefab, InventoryContent.transform);
                Text buttonText = buttonGO.GetComponentInChildren<Text>();
                buttonGO.name = inventory[i].Name + " button";
                buttonText.text = inventory[i].Name;

                Item item = inventory[i];
                buttonGO.onClick.AddListener(delegate { DisplaySelectedItemOnCanvas(item); });
            }
        }
    }

    void DisplaySelectedItemOnCanvas(Item item)
    {
        selectedItem = item;

        itemImage.texture = selectedItem.Icon;
        itemName.text = selectedItem.Name;
        ItemDescription.text = selectedItem.Description +
                    "\nValue: " + selectedItem.Value +
                    "\nAmount: " + selectedItem.Amount;
    }
    #endregion


    private void OnGUI()
    {
        if (showIMGUIInventory)
        {
            GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");

            List<string> itemTypes = new List<string>(Enum.GetNames(typeof(Item.ItemType)));
            itemTypes.Insert(0, "All");

            for (int i = 0; i < itemTypes.Count; i++)
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
            if (selectedItem != null)
            {
                DisplaySelectedItem();
            }
        }

    }
    private void DisplaySelectedItem()
    {
        GUI.Box(new Rect(Screen.width / 4, Screen.height / 3,
            Screen.width / 5, Screen.height / 5),
            selectedItem.Icon);

        GUI.Box(new Rect(Screen.width / 4, (Screen.height / 3) + (Screen.height / 5),
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
