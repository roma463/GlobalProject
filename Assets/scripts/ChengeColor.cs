using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChengeColor : MonoBehaviour
{
    [SerializeField] private Material _mat;
    void Start()
    {
        StartCoroutine(Col());
    }
    private IEnumerator Col()
    {
        while (true)
        {
            var x = Random.Range(0, 0.02f);
            yield return new WaitForSeconds(3f);
            _mat.SetFloat("_forceDistortion", x);
            yield return new WaitForSeconds(.3f);
            _mat.SetFloat("_forceDistortion", -x);
            yield return new WaitForSeconds(.3f);
            _mat.SetFloat("_forceDistortion", 0.001f);
        }
    }
}
