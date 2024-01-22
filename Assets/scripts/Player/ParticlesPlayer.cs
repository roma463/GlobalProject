using System.Collections;
using UnityEngine;

public class ParticlesPlayer : MonoBehaviour
{
    public enum ViewParticle
    {
        TelePort
    }
    [SerializeField] private ParticleSystem _teleport;
    [SerializeField] private GameObject _teleportAnim;

    [System.Obsolete]
    public void Play(ViewParticle particlePlay)
    {
        if(particlePlay == ViewParticle.TelePort)
        {
            //Instantiate(_teleportAnim, transform.position, Quaternion.identity);
           ParticleSystem  particle =  Instantiate(_teleport, transform.position, Quaternion.identity);
            StartCoroutine(Delited(particle.duration, particle.gameObject));
        }
    }
    private IEnumerator Delited(float  time, GameObject deletedObject)
    {
        yield return new WaitForSeconds(time);
        Destroy(deletedObject);
    }
}
