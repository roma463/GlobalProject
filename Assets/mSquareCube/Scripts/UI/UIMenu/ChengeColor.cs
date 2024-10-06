using System.Collections;
using UnityEngine;

public class ChengeColor : MonoBehaviour
{
    [SerializeField] private Material _shaderManerial;
    private const string NAME_FLOAT_PARAMETR_IN_SHADER = "_forceDistortion";

    private void Start()
    {
        StartCoroutine(ChangeCoroutine());
    }

    private IEnumerator ChangeCoroutine()
    {
        while (true)
        {
            var offset = Random.Range(0, 0.02f);
            yield return new WaitForSeconds(3f);
            _shaderManerial.SetFloat(NAME_FLOAT_PARAMETR_IN_SHADER, offset);
            yield return new WaitForSeconds(.3f);
            _shaderManerial.SetFloat(NAME_FLOAT_PARAMETR_IN_SHADER, -offset);
            yield return new WaitForSeconds(.3f);
            _shaderManerial.SetFloat(NAME_FLOAT_PARAMETR_IN_SHADER, 0.001f);
        }
    }
}
