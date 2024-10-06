using System.Collections;
using UnityEngine;

public class ParticlesPlayer : MonoBehaviour
{
    [SerializeField] private ParticleSystem _teleportParticle;
    [SerializeField] private GameObject _teleportAnim;
    [SerializeField] private Teleport _teleport;

    public void Teleport()
    {
        ParticleSystem  particle =  Instantiate(_teleportParticle, transform.position, Quaternion.identity);
        StartCoroutine(Delited(particle.duration, particle.gameObject));
    }
    private IEnumerator Delited(float  time, GameObject deletedObject)
    {
        yield return new WaitForSeconds(time);
        Destroy(deletedObject);
    }

    private void OnEnable()
    {
        _teleport.Effects_E += Teleport;

    }

    private void OnDisable()
    {
        _teleport.Effects_E -= Teleport;
    }
}
