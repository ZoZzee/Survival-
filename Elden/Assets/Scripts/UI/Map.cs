using UnityEngine;

public class Map : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;

    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
        _canvasGroup.alpha = 0f;
    }
    public void inActivate()
    {
        if (_canvasGroup.alpha == 1f)
        {
            offActive();
        }
        else
        {
            _canvasGroup.alpha = 1f;
        }
    }
    private void offActive()
    {
        _canvasGroup.alpha = 0f;
    }
}
