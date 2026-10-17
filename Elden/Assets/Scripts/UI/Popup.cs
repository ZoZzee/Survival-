using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private float _speedAndTimes;
    [SerializeField] private Transform _popupContent;
    [SerializeField] private CanvasGroup _darkCanvasGroup;


    private void OnEnable()
    {
        _popupContent.localScale = Vector3.zero;
        _popupContent.LeanScale(Vector3.one, _speedAndTimes).setEaseOutBack();

        _darkCanvasGroup.alpha = 0;
        _darkCanvasGroup.LeanAlpha(1f, _speedAndTimes);

    }
    public void OnClose()
    {
        _popupContent.LeanScale(Vector3.zero, _speedAndTimes).setEaseOutBounce().setOnComplete(()=>{
            gameObject.SetActive(false);
        });

        _darkCanvasGroup.LeanAlpha(0f, _speedAndTimes);
    }

}
