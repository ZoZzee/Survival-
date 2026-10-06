using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private float _speedAndTimes;
    private void OnEnable()
    {
        transform.localScale = Vector3.zero;
        transform.LeanScale(Vector3.one, _speedAndTimes).setEaseOutBack();
    }
    public void OnClose()
    {
        transform.LeanScale(Vector3.zero, _speedAndTimes).setEaseInOutBounce().setOnComplete(()=>{
            gameObject.SetActive(false);
        });
    }

}
