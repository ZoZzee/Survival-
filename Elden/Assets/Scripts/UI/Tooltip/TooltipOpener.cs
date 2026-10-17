using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class TooltipOpener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{

    [SerializeField] private string _landmarkName;
    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipInstance.instance.ShowTooltip(transform, _landmarkName);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipInstance.instance.HideTooltip();
    }
}
    