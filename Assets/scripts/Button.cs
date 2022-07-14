using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject _objectAction;
    [Range(0,1)]
    [SerializeField] private float _scaleByClick;
    [SerializeField] private Transform _sptireButton;
    [SerializeField] private float _speedChenge;
    private float _startScale;
    private Action _action;
    private List<GameObject> _clickedButton = new List<GameObject>();
    private float _currentScale;
    private void Start()
    {
        _startScale = _sptireButton.localScale.y;
        _currentScale = _startScale;
        if (_objectAction == null)
            return;
        if (!_objectAction.TryGetComponent(out _action))
        {
            _objectAction = null;
        }
    }
    public Transform GetTransformActiveObject()
    {
        if(_objectAction != null)
        {
           return _objectAction.transform;
        }
        else
        {
            return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_clickedButton.Count == 0)
        {
            StopAllCoroutines();
           StartCoroutine(ColorByClick());
            _action.launch();
        }
        _clickedButton.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _clickedButton.Remove(collision.gameObject);
        if (_clickedButton.Count == 0)
        {
            StopAllCoroutines();
            StartCoroutine(ColorByClickUp());
            _action.launch();
        }
    }
    private IEnumerator ColorByClick()
    {
        for (; _currentScale > _scaleByClick; _currentScale -= Time.deltaTime * _speedChenge)
        {
            _sptireButton.localScale = new Vector2(_sptireButton.localScale.x, _currentScale);
            yield return null;

        }

    }
    private IEnumerator ColorByClickUp()
    {
        for (; _currentScale < _startScale; _currentScale += Time.deltaTime * _speedChenge)
        {
            _sptireButton.localScale = new Vector2(_sptireButton.localScale.x, _currentScale);
            yield return null;
        }

    }
}
public interface Action
{
    void launch();
}

