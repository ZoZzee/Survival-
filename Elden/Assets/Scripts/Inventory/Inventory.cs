using System;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Equipment inventory;
    public Equipment bagInventory;


    private void Start()
    {
        Refresh();
    }

    public void AddItem(Item newItem)
    {
        bool haveItem = false;
        bool addInInventory = false;

        for (int i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items[i] == newItem)
            {
                haveItem = true;
                inventory.counts[i] += 1;
                break;
            }
        }
        if (!haveItem)
        {
            for (int i = 0; i < bagInventory.items.Length; i++)
            {
                if (bagInventory.items[i] == newItem)
                {
                    haveItem = true;
                    bagInventory.counts[i] += 1;
                    break;
                }
            }
        }

        if (!haveItem)
        {
            for (int i = 0; i < inventory.items.Length; i++)
            {
                if (inventory.items[i] == null)
                {
                    inventory.items[i] = newItem;
                    inventory.counts[i] = 1;
                    addInInventory = true;
                    break;
                }
            }
            if (!addInInventory)
            {
                for (int i = 0; i < bagInventory.items.Length; i++)
                {
                    if (bagInventory.items[i] == null)
                    {
                        bagInventory.items[i] = newItem;
                        bagInventory.counts[i] = 1;
                        break;
                    }
                }
            }
        }
        Refresh();
    }
    public void AddItemCount(Item item, int count)
    {
        for(int i = 0;i < count; i++)
        {
            AddItem(item);
        }
    }

    public void ItemDropped(Equipment inventory,int index)
    {
        inventory.counts[index]--;

        if (inventory.counts[index] == 0)
        {
            inventory.items[index ] = null;
        }
        Refresh();
    }

    public void Refresh()
    {
        for(int i = 0; i < inventory.cells.Length; i++)
        {
            if (inventory.counts[i] == 0 && inventory.items[i] != null)
            {
                inventory.items[i] = null;
            }
            inventory.cells[i].RefreshCell(inventory.items[i], inventory.counts[i]);
        }
        for (int i = 0; i < bagInventory.cells.Length; i++)
        {
            if (bagInventory.counts[i] == 0 && bagInventory.items[i] != null)
            {
                bagInventory.items[i] = null;
            }
            bagInventory.cells[i].RefreshCell(bagInventory.items[i], bagInventory.counts[i]);
        }
    }

}
[Serializable]
public class Equipment
{
    public Item[] items;
    public int[] counts;
    public Cell[] cells;
}
