using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;

public class AnimationNumber : MonoBehaviour
{
    [SerializeField] private TextMeshPro _numberText;
    [SerializeField] private string _number;
    [SerializeField] private float _delayPer;
    [SerializeField] private float _countPer;
    [SerializeField] private CinemachineVirtualCamera _camera;

    public void StartAnimation()
    {
        StartCoroutine(Animation());
    }

    private IEnumerator Animation()
    {
        string currentNumber = "";
        foreach (var item in _number)
        {
            for (int i = 0; i < _countPer; i++)
            {
                var randomValue = Random.Range(0, 9);
                _numberText.text = currentNumber + randomValue;
                yield return new WaitForSeconds(_delayPer);
            }
            currentNumber += item;
            _numberText.text = currentNumber;
        }
        yield return new WaitForSeconds(.3f);
        _camera.gameObject.SetActive(false);
    }
}
