using System;
using Unity.VisualScripting;
using UnityEngine;

public class Resourse : MonoBehaviour
{

    public Item resourseItem;

    public int minCount;
    public int maxCount;

    [SerializeField] private bool getItemEveryHit;
    [SerializeField] private bool destroySelf;

    [SerializeField] private ParticleSystem particle;


    public float durability;
    public Tool.ToolType toolType;

    public event Action onGetResourses;
    public event Action<ParticleSystem> onHitResourses;

    public void TryHit(Tool tool, Inventory inventory, Vector3 pointOfCick)
    {
        if(tool.type == toolType)
        {
            durability -= tool.effectiveness;
            ParticleSystem newParticl = Instantiate(particle,pointOfCick, Quaternion.identity);
            onHitResourses?.Invoke(newParticl);
            
            
            if(durability <= 0)
            {
                
                GetResourses(inventory);
                if(destroySelf) Destroy(gameObject);
            }
            else if(getItemEveryHit)
            {
                GetResourses(inventory);
            }
        }
    }

    private void GetResourses(Inventory inventory)
    {
        inventory.AddItemCount(resourseItem, UnityEngine.Random.Range(minCount, maxCount));
        onGetResourses?.Invoke();
    }
}
