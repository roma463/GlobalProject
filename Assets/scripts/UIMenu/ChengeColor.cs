using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChengeColor : MonoBehaviour
{
    [SerializeField] private Material _shaderManerial;
    void Start()
    {
        StartCoroutine(ChangeCoroutine());
    }
    private IEnumerator ChangeCoroutine()
    {
        while (true)
        {
            var x = Random.Range(0, 0.02f);
            yield return new WaitForSeconds(3f);
            _shaderManerial.SetFloat("_forceDistortion", x);
            yield return new WaitForSeconds(.3f);
            _shaderManerial.SetFloat("_forceDistortion", -x);
            yield return new WaitForSeconds(.3f);
            _shaderManerial.SetFloat("_forceDistortion", 0.001f);
        }
    }
}
