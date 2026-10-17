using TMPro;
using UnityEngine;

public class TooltipInstance : MonoBehaviour
{

    public static TooltipInstance instance;

    [SerializeField] TMP_Text text;

    private int animationId = -1;
    void Awake()
    {
        instance = this;
        transform.localScale = Vector3.zero;
    }

    public void ShowTooltip(Transform sourceTransform, string landmarkName)
    {
        transform.position = sourceTransform.position;
        text.text = landmarkName;
        StopAnimation();
        animationId = transform.LeanScale(Vector3.one, 0.5f).setEaseOutExpo().id;
    }

    public void HideTooltip()
    {
        StopAnimation();
        animationId = transform.LeanScale(Vector3.zero, 0.3f).setEaseOutExpo().id;
    }

    private void StopAnimation()
    {
        if(animationId != -1)
        {
            LeanTween.cancel(animationId);
            animationId = -1;
        }
    }
}
