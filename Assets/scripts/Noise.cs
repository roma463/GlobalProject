using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Noise : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _obj;
    [SerializeField] private Gradient _gradient;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(z());
        }
    }
    private IEnumerator z()
    {
        for (float i = 0; i <= 1; i+=Time.deltaTime)
        {
            yield return null;
            _obj.color = _gradient.Evaluate(i);
        }
    }

}
