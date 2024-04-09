using System.Collections;
using UnityEngine;

public class Door : ActionObject, Action
{
    [SerializeField] private float _speed;
    [SerializeField] private float _scaleOpen = 1;
    [SerializeField] private AudioSource _openSound;
    [SerializeField] private Transform _scaleTransform;
    private float _startSize;
    private Coroutine _currentCorutine;

    public override void Start()
    {
        base.Start();
        _startSize = _scaleTransform.localScale.y;
        if (IsActive)
        {
            ChangeSize(_scaleOpen);
        }
    }
    
    public override void launch()
    {
        if (_currentCorutine != null)
        {
            StopCoroutine(_currentCorutine);
            _currentCorutine = null;
        }
        if (IsActive == false)
            _currentCorutine = StartCoroutine(ActiviyDoor(_scaleOpen));
        else
            _currentCorutine = StartCoroutine(ActiviyDoor(_startSize));
        base.launch();
    }
    
    private IEnumerator ActiviyDoor(float targetScale)
    {
        while(_scaleTransform.localScale.y != targetScale)
        {
            float scaleY = Mathf.MoveTowards(_scaleTransform.localScale.y, targetScale, Time.deltaTime * _speed);
            _scaleTransform.localScale = new Vector3(_scaleTransform.localScale.x, scaleY, 1);
            yield return null;
        }
        _currentCorutine = null;
    }

    private void ChangeSize(float size)
    {
        var scale = _scaleTransform.localScale;
        scale.y = size;
        _scaleTransform.localScale = scale;
    }
}

