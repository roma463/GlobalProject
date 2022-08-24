using System.Collections;
using UnityEngine;

public class Door : MonoBehaviour, Action
{
    [SerializeField] private float _speed;
    [SerializeField] private float _scaleOpen = 1;
    [SerializeField] private bool _startIsOpen;
    [SerializeField] private AudioSource _open;
    private bool _isOpen;
    private float _startSize;
    private Coroutine _currentCorutine;

    private void Start()
    {
        _startSize = transform.localScale.y;
        if (_startIsOpen)
        {
            ChangeSize(_scaleOpen);
        }
        _isOpen = _startIsOpen;
    }
    public void launch()
    {
        if (_currentCorutine != null)
        {
            StopCoroutine(_currentCorutine);
            _isOpen = !_isOpen;
            _currentCorutine = null;
        }
        if (_isOpen == false)
            _currentCorutine = StartCoroutine(Raise());
        else
            _currentCorutine = StartCoroutine(Closed());
    }
    private IEnumerator Raise()
    {
        if(_open != null)
        _open.Play();
        for (float i = transform.localScale.y; i > _scaleOpen; i -= Time.deltaTime * _speed)
        {
            ChangeSize(i);
            yield return null;
        }
        _currentCorutine = null;
        _isOpen = true;
        ChangeSize(_scaleOpen);
        if(_open != null)
            _open.Stop();
    }
    private IEnumerator Closed()
    {
        if (_open != null)
            _open.Play();
        for (float i = transform.localScale.y; i < _startSize; i += Time.deltaTime * _speed)
        {
            ChangeSize(i);
            yield return null;
        }
        _currentCorutine = null;
        _isOpen = false;
        ChangeSize(_startSize);
        if (_open != null)
            _open.Stop();
    }
    private void ChangeSize(float size)
    {
        var scale = transform.localScale;
        scale.y = size;
        transform.localScale = scale;
    }
}

