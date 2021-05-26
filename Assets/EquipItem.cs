using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipItem : MonoBehaviour
{
    [SerializeField]
    public Item item;

    public void EquipIt()
    {
        Equipment equipment = FindObjectOfType<Equipment>();

        equipment.primary.EquipedItem = item;
    }
}
