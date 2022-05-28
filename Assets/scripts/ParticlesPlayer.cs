using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticlesPlayer : MonoBehaviour
{

    public enum ViewParticle
    {
        TelePort
    }
    [SerializeField] private ParticleSystem _teleport;
    public void Play(ViewParticle particlePlay)
    {
        if(particlePlay == ViewParticle.TelePort)
        {
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
