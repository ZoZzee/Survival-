using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class AnimalController : MonoBehaviour
{
    [Header("Wander Settings")]
    [SerializeField] private float _wanderRadius = 25f;
    [SerializeField] private float _maxDistanseToThePoint = 2f;
    [SerializeField] private float minIdleTime = 2f;
    [SerializeField] private float maxIdleTime = 5f;
    [SerializeField] private float maxSpeed = 5f;
    private NavMeshAgent _agent;
    private Vector3 _spawnPoint;
    private bool _isWaiting;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _agent = GetComponent<NavMeshAgent>();
        _spawnPoint = transform.position;

        StartCoroutine(WanderRoutin());
    }

    private void Update()
    {
        float currentSpeed = _agent.velocity.magnitude;
        float normalizeSpeed = Mathf.Clamp01(currentSpeed / maxSpeed);

        _animator.SetFloat("State",normalizeSpeed);
    }

    private IEnumerator WanderRoutin()
    {
        while(true)
        {
            if(!_agent.pathPending &&  !_agent.hasPath && !_isWaiting)
            {
                _isWaiting = true;
                float waitTime = Random.Range(minIdleTime, maxIdleTime);
                yield return new WaitForSeconds(waitTime);

                Vector3 destination = GetRandompointInRadius(_spawnPoint,_wanderRadius);
                _agent.SetDestination(destination);

                _isWaiting =false;
            }
            yield return null;
        }
    }

    private Vector3 GetRandompointInRadius(Vector3 center, float radius)
    {
        for (int i = 0; i < 30; i++)
        {
            Vector3 newPoint = Random.insideUnitSphere * radius;
            newPoint.y = center.y;
            if (NavMesh.SamplePosition(newPoint, out NavMeshHit hit, _maxDistanseToThePoint, NavMesh.AllAreas))
            {
                return hit.position;
            }
        }
        return center;
    }
}
