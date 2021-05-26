using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct EquipmentSlot
{
    [SerializeField] private Item item;
    public Item EquipedItem
    {
        get
        {
            return item;
        }
        set
        {
            item = value;
            itemEquiped.Invoke(this); // change to this
        }
    }
    public Transform visualLocation;
    public Vector3 offset;

    public delegate void ItemEquiped(EquipmentSlot item); //change to equipment slot
    public event ItemEquiped itemEquiped;

}

public class Equipment : MonoBehaviour
{
    public EquipmentSlot primary;

    public EquipmentSlot secondary;

    public EquipmentSlot defensive;

    private void Awake()
    {
        primary.itemEquiped += EquipItem;
        secondary.itemEquiped += EquipItem;
        defensive.itemEquiped += EquipItem;
    }
    private void Start()
    {
        EquipItem(primary);
        EquipItem(secondary);
        EquipItem(defensive);
    }

    public void EquipItem(EquipmentSlot item)// Item item, Transform visualLocation)
    {
        if(item.visualLocation == null)// add item.
        {
            return;
        }

        foreach (Transform child in item.visualLocation)// add item.
        {
            GameObject.Destroy(child.gameObject);
        }

        if (item.EquipedItem.Mesh == null)// add item.EquipedItem.
        {
            return;
        }

        GameObject meshInstance = Instantiate(item.EquipedItem.Mesh, item.visualLocation);//add item.
        meshInstance.transform.localPosition = item.offset; //item.

        OffsetLocation offset = meshInstance.GetComponent<OffsetLocation>();
        if (offset != null)
        {
            meshInstance.transform.localPosition += offset.positionOffset;

            meshInstance.transform.localRotation = Quaternion.Euler(offset.rotationOffset);

            meshInstance.transform.localScale = offset.scaleOffset;
        }
    }

}
