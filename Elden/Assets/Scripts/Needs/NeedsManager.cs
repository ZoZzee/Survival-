using UnityEngine;
using UnityEngine.Rendering;
using static UnityEditor.Progress;

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

    private SaveSystem _saveSystem;

    private void Awake()
    {
        instance = this;

        Health = new HealthNeed(healthMax);
        Hunger = new HungerNeed(hungerMax, hungerMinus);
        Energy = new EnergyNeed(energyMax);
        Sleep = new SleepNeed(sleepMax,sleepMinus);
    }
    private void Start()
    {
        _saveSystem = SaveSystem.instance;

        _saveSystem.OnSaveRequested += Save;
        _saveSystem.OnLoadRequested += Load;

    }
    private void OnDisable()
    {
        _saveSystem.OnSaveRequested -= Save;
        _saveSystem.OnLoadRequested -= Load;
    }

    private void Save()
    {
        _saveSystem.playerInfo.energy = Energy.Current;
        _saveSystem.playerInfo.health = Health.Current;
        _saveSystem.playerInfo.hunger = Hunger.Current;
        _saveSystem.playerInfo.sleep = Sleep.Current;
    }
    private void Load()
    {
        Energy.CurrentSet(_saveSystem.playerInfo.energy);
        Health.CurrentSet(_saveSystem.playerInfo.health);
        Hunger.CurrentSet(_saveSystem.playerInfo.hunger);
        Sleep.CurrentSet(_saveSystem.playerInfo.sleep);

    }

    private void Update()
    {
        
        float dt = Time.deltaTime;
        Hunger.PermanentMinus(dt);
        Sleep.PermanentMinus(dt);
        if(Hunger.IsStarving)
        {
            Health.CustomPermanentMinus(healthMinusWhenHungry,dt);
        }
        if(!_playerController.isRunning && !Hunger.IsStarving)
        {
            Energy.CustomPermanentRestore(energyPlus,dt);
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

    public void Sleeping(Subject subject)
    {
        Debug.Log(subject);
        Debug.Log(subject.name);
        
        Health.Restore(subject.sleap.healthAmount);
        Hunger.Restore(subject.sleap.hungerAmount);
        Energy.Restore(subject.sleap.energyAmount);
        Sleep.Restore(subject.sleap.sleepAmount);
        _playerController.cantMove(2.7f);
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

    public bool IsfallingAsleep => IsEmpty();
}
