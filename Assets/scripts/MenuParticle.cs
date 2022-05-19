using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuParticle : MonoBehaviour
{
    [SerializeField] private ParticleSystem _exc;
    [SerializeField] private ParticleSystem _standart;
    private int x;
    public void Click()
    {
        x++;
        if (x%2==0)
        {
            _exc.gameObject.SetActive(false);
            _standart.gameObject.SetActive(true);
        }
        else
        {
            _exc.gameObject.SetActive(true);
            //_standart.Pause();
            _standart.gameObject.SetActive(false);
        }
    }
}
