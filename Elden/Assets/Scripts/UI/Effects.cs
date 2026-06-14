using UnityEngine;

public class Effects : MonoBehaviour
{
    [SerializeField]private Animator UI_Animator;
    public void Sleep()
    {
        UI_Animator.SetTrigger("Sleep");
    }
}
