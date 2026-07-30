using UnityEngine;

public class ParticleOfResourse : MonoBehaviour
{
    private Resourse resourses;

    //[SerializeField] private ParticleSystem _particl;
    private void Start()
    {
        resourses = GetComponent<Resourse>();
        resourses.onHitResourses += ParticlePlayed;
        
    }

    public void ParticlePlayed(ParticleSystem particle)
    {
        particle.Play();
        Destroy(particle.gameObject, 1f);
    }

    private void OnDestroy()
    {
        resourses.onHitResourses -= ParticlePlayed;
    }
}
