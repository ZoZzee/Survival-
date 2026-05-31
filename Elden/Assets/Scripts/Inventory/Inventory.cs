using UnityEngine;

public class Inventory : MonoBehaviour
{
    public Item[] items;
    public int[] counts;
    public Cell[] cells;

    private void Start()
    {
        Refresh();
    }

    public void AddItem(Item newItem)
    {
        bool haveItem = false;
        for (int i = 0; i < items.Length; i++)
        {
            if (items[i] == newItem)
            {
                haveItem = true;
                counts[i] += 1;
                break;
            }
        }

        if (!haveItem)
        {
            for (int i = 0; i < items.Length; i++)
            {
                if (items[i] == null)
                {
                    items[i] = newItem;
                    counts[i] = 1;
                    break;
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

    public void ItemDropped(int index)
    {
        counts[index]--;

        if (counts[index] == 0)
        {
            items[index ] = null;
        }
        Refresh();
    }

    public void Refresh()
    {
        for(int i = 0; i < cells.Length; i++)
        {
            if (counts[i] == 0 && items[i] != null)
            {
                items[i] = null;
            }
            cells[i].RefreshCell(items[i], counts[i]);
        }
    }

}
