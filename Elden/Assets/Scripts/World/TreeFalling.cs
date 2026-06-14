using UnityEngine;

public class TreeFalling : MonoBehaviour
{
    private Resourse resourses;

    [SerializeField]private Rigidbody rb;
    [SerializeField]private float randonInpuls;
    private void Start()
    {
        resourses = GetComponent<Resourse>();
        resourses.onGetResourses += Fall;
    }

    public void Fall()
    {
        rb.isKinematic = false;
        rb.useGravity = true;
        Vector3 newDiraction = new Vector3(Random.Range(-randonInpuls, randonInpuls), 2, Random.Range(-randonInpuls, randonInpuls));
        rb.AddForce(newDiraction, ForceMode.Impulse);
        Destroy(gameObject, 5f);
    }

    private void OnDestroy()
    {
        resourses.onGetResourses -= Fall;
    }
}
