using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDisableParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem[] _particals;
    public void StopParticals()
    {
        Destroy(gameObject, 5f);
        for (int i = 0; i < _particals.Length; i++)
        {
            _particals[i].Stop();
        }
    }
}
