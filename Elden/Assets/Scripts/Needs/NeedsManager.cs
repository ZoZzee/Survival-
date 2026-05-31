using UnityEngine;
using UnityEngine.Rendering;

public class NeedsManager : MonoBehaviour
{
    public float healthMax;
    public float healthMinusWhenHungry;
    public float hungerMax;
    public float hungerMinus;
    public float energyMax;
    public float energyMinus;
    public float energyPlus;
    public float sleepMax;
    public float sleepMinus;

    [SerializeField] private PlayerController _playerController;

    public HealthNeed Health {  get; private set; }
    public HungerNeed Hunger {  get; private set; }
    public EnergyNeed Energy {  get; private set; }
    public SleepNeed Sleep { get; private set; }

    public static NeedsManager instance;

    private void Awake()
    {
        instance = this;

        Health = new HealthNeed(healthMax);
        Hunger = new HungerNeed(hungerMax, hungerMinus);
        Energy = new EnergyNeed(energyMax);
        Sleep = new SleepNeed(sleepMax,sleepMinus);
    }

    private void Update()
    {
        float dt = Time.deltaTime;
        Hunger.PermanentMinus(dt);

        if(Hunger.IsStarving)
        {
            Health.CustomPermanentMinus(healthMinusWhenHungry,dt);
        }
        if(!_playerController.isRunning && !Hunger.IsStarving)
        {
            Energy.PermanentRestore(energyPlus,dt);
        }

    }

    public void Eat(float amouint)
    {
        Hunger.Restore(amouint);
    }
    public void Heal(float amount)
    {
        Health.Restore(amount);
    }
    public void Running()
    {
        Energy.CustomPermanentMinus(energyMinus, Time.deltaTime);
    }

    public void UseItem(Item item)
    {
        Health.Restore(item.usable.healthAmount);
        Hunger.Restore(item.usable.hungerAmount);
        Energy.Restore(item.usable.energyAmount);
    }

    public void Sleeping(float amount)
    {
        Sleep.Restore(amount);
    }
}

public class HealthNeed : Need
{
    public HealthNeed(float max) : base(max, 0) { }

}

public class HungerNeed : Need
{
    public HungerNeed(float max, float tickRate) : base(max, tickRate){}

    public bool IsStarving => IsEmpty();

}

public class EnergyNeed : Need
{
    public EnergyNeed(float max) : base (max, 0f) { }

    public bool CanRun => GetPercentage() > 0.01f;
}

public class SleepNeed : Need
{
    public SleepNeed(float max, float tickRate) : base(max, tickRate) { }

    public bool IsStarving => IsEmpty();
}
