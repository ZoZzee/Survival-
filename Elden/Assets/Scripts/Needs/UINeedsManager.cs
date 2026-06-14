using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class UINeedsManager : MonoBehaviour
{
    [SerializeField] private Slider heaithBar;
    [SerializeField] private Image hungerBar;
    [SerializeField] private Image energyBar;
    [SerializeField] private Image waterBar;
    [SerializeField] private Image sleepBar;
    [SerializeField] private NeedsManager _needs;


    private void Update()
    {

        heaithBar.value =_needs.Health.GetPercentage();
        sleepBar.fillAmount = _needs.Sleep.GetPercentage();
        hungerBar.fillAmount =_needs.Hunger.GetPercentage();
        energyBar.fillAmount =_needs.Energy.GetPercentage();
        

        //Debug.Log(_needs.Health.GetPercentage() + "    " + _needs.Hunger.GetPercentage());
    }

}
