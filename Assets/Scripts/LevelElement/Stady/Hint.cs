using System.Collections;
using TMPro;
using UnityEngine;

public class Hint : MonoBehaviour
{
    [SerializeField] private GameObject _hintObject;

    [SerializeField] private Gradient _changeColor;
    [SerializeField] private float _speedChenge;
    [SerializeField] private float _positionChange;
    private TMP_Text _textMesh;
    private float _currentColorGradient;
    private bool _currentCorutine;

    private void Start()
    {
        _textMesh = _hintObject.GetComponent<TMP_Text>();
        _hintObject = _textMesh.gameObject;
        _textMesh.color = _changeColor.Evaluate(0);
        _hintObject.transform.position += Vector3.up * _positionChange;
    }

    public virtual void AnimationShow()
    {

        if (_currentCorutine)
        {
            StopAllCoroutines();
        }
        StartCoroutine(ActivityText(true, (Vector2)_hintObject.transform.position + Vector2.down * _positionChange));
    }

    public virtual void AnimationHide()
    {
        if (_currentCorutine)
        {
            StopAllCoroutines();
        }
        StartCoroutine(ActivityText(false, (Vector2)_hintObject.transform.position + Vector2.up * _positionChange));
    }

    private IEnumerator ActivityText(bool isActiveText, Vector2 targetPosition)
    {
        int targetPositionGradient = isActiveText ? 1 : 0;
        _currentCorutine = true;
        while (_currentColorGradient != targetPositionGradient)
        {
            _currentColorGradient = Mathf.MoveTowards(_currentColorGradient, targetPositionGradient, _speedChenge * Time.deltaTime);
            _textMesh.color = _changeColor.Evaluate(_currentColorGradient);

            var currentPosition = _hintObject.transform.position;
            currentPosition.y = Mathf.MoveTowards(currentPosition.y, targetPosition.y, _positionChange * (_speedChenge * Time.deltaTime));
            _hintObject.transform.position = currentPosition;
            yield return null;
        }
        _currentCorutine = false;
    }
}
