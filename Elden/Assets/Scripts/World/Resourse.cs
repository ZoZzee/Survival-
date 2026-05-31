using UnityEngine;

public class Resourse : MonoBehaviour
{

    public Item resourseItem;

    public int minCount;
    public int maxCount;

    public float durability;
    public Tool.ToolType toolType;

    public void TryHit(Tool tool, Inventory inventory)
    {
        if(tool.type == toolType)
        {
            durability -= tool.effectiveness;
            if(durability <= 0)
            {
                GetResourses(inventory);
            }
        }
    }

    private void GetResourses(Inventory inventory)
    {
        inventory.AddItemCount(resourseItem, Random.Range(minCount, maxCount));
    }
}
