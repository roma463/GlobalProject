using System.Collections;
using UnityEngine;

public class Door : ActionObject, IAction
{
    [SerializeField] private float _speed;
    [SerializeField] private float _scaleOpen = 1;
    [SerializeField] private AudioSource _openSound;
    [SerializeField] private Transform _scaleTransform;

    [SerializeField] private Transform _edgePoint;
    [SerializeField] private Transform _cirleEdge;
    private float _scaleClosed;
    private Coroutine _currentCorutine;

    public override void Start()
    {
        base.Start();
        ChangeScale();
        _scaleClosed = _scaleTransform.localScale.y;
        if (IsActive)
        {
            ChangeSize(_scaleOpen);
        }
    }

    private void OnValidate()
    {
        ChangeScale();
    }

    private void ChangeScale()
    {
        if (transform.localScale == Vector3.one)
            return;
        _scaleTransform.localScale = transform.localScale;
        transform.localScale = Vector3.one;
    }

    public override void Launch(bool state)
    {
        if (_currentCorutine != null)
        {
            StopCoroutine(_currentCorutine);
            _currentCorutine = null;
        }
        base.Launch(state);
    }

    public override void PressState()
    {
        _currentCorutine = StartCoroutine(ActiviyDoor(_scaleOpen));
    }

    public override void Release()
    {
        _currentCorutine = StartCoroutine(ActiviyDoor(_scaleClosed));
    }

    private IEnumerator ActiviyDoor(float targetScale)
    {
        while(_scaleTransform.localScale.y != targetScale)
        {
            float scaleY = Mathf.MoveTowards(_scaleTransform.localScale.y, targetScale, Time.deltaTime * _speed);
            _scaleTransform.localScale = new Vector3(_scaleTransform.localScale.x, scaleY, 1);
            SetEdgeCircle();
            yield return null;
        }
        _currentCorutine = null;
    }

    private void SetEdgeCircle()
    {
        _cirleEdge.position = _edgePoint.position;
    }

    private void ChangeSize(float size)
    {
        var scale = _scaleTransform.localScale;
        scale.y = size;
        _scaleTransform.localScale = scale;
        SetEdgeCircle();
    }
}

