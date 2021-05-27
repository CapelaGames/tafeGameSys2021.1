using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPickUpItem : MonoBehaviour
{
    [SerializeField] private Inventory inventory;
    [SerializeField] private Transform dropPoint;
    [SerializeField] private Camera camera;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F))
        {
            Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            RaycastHit hitInfo;

            if(Physics.Raycast(ray, out hitInfo, 50f))
            {
                DroppedItem droppedItem = hitInfo.collider.GetComponent<DroppedItem>();

                if(droppedItem != null)
                {
                    inventory.AddItemToInventory(droppedItem.item);
                    Destroy(hitInfo.collider.gameObject);
                }
                
            }
        }
    }

    public void DropItem()
    {
        if(inventory.selectedItem == null)
        {
            return;
        }

        //spawn item and drop it
        GameObject mesh = inventory.selectedItem.Mesh;
        if(mesh != null)
        {
            GameObject spawnedMesh = Instantiate(mesh,null);

            spawnedMesh.transform.position = dropPoint.position;

            DroppedItem droppedItem = mesh.GetComponent<DroppedItem>();
            droppedItem.item = new Item(inventory.selectedItem, 1);
        }

        // remove from inventory if none left
        inventory.selectedItem.Amount--;
        if(inventory.selectedItem.Amount <= 0)
        {
            //inventory.inventory.Remove(inventory.selectedItem);
            inventory.RemoveItemFromInventory(inventory.selectedItem);
            inventory.selectedItem = null;
        }
    }
}
