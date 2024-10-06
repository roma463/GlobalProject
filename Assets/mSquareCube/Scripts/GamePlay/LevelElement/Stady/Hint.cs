using System.Collections;
using TMPro;
using UnityEngine;

public class Hint : MonoBehaviour
{
    [SerializeField] private TMP_Text _hintObject;
    [SerializeField] private Gradient _changeColor;
    [SerializeField] private float _speedOffset;
    [SerializeField] private float _offset;
    private float _currentColorGradient;
    private Vector2 _positionShow, _positionHide;
    private Vector2 _currentTextPosition;
    private bool _coroutineIsActive = false;

    private void Start()
    {
        _currentTextPosition = _hintObject.transform.position;
        _positionShow = (Vector2)_hintObject.transform.position + (_offset * Vector2.down);
        _positionHide = (Vector2)_hintObject.transform.position + (_offset * Vector2.up);

        _hintObject.color = _changeColor.Evaluate(0);
        _hintObject.transform.position = _positionHide;
    }

    public virtual void AnimationShow()
    {

        if (_coroutineIsActive)
        {
            StopAllCoroutines();
        }
        StartCoroutine(Animation(true, _positionShow));
    }

    public virtual void AnimationHide()
    {
        if (_coroutineIsActive)
        {
            StopAllCoroutines();
        }
        StartCoroutine(Animation(false, _positionHide));
    }

    private IEnumerator Animation(bool isActiveText, Vector2 targetPosition)
    {
        _coroutineIsActive = true;
        int targetPositionGradient = isActiveText ? 1 : 0;
        while (_currentColorGradient != targetPositionGradient)
        {
            _currentColorGradient = Mathf.MoveTowards(_currentColorGradient, targetPositionGradient, _speedOffset * Time.deltaTime);
            _hintObject.color = _changeColor.Evaluate(_currentColorGradient);

            _currentTextPosition.y = Mathf.MoveTowards(_currentTextPosition.y, targetPosition.y, _offset * (_speedOffset * Time.deltaTime));
            _hintObject.transform.position = _currentTextPosition;
            yield return null;
        }
        _coroutineIsActive = false;
    }
}
