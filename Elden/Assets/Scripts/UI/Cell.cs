using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Cell : MonoBehaviour
{
    public Image icon;

    public TMP_Text countText;

    public GameObject selection;

    public Item itemInCell;

    [HideInInspector] public Inventory inventory;

    public void RefreshCell(Item item,int count)
    {
        if(item != null)
        {
            icon.enabled = true;
            icon.sprite = item.icon;
            itemInCell = item;
            if (count > 1)
            {
                countText.text = count.ToString();
            }
            else
            {
                countText.text = "";
            }
        }
        else
        {
            icon.enabled = false;
            icon.sprite = null;
            itemInCell = null;
            countText.text = "";
        }
    }

}
