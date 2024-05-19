using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Button : MonoBehaviour
{
    [SerializeField] private float _delayLaunch;
    [SerializeField] private List<GameObject> _actionsObject = new List<GameObject>();
    [Range(0,1)]
    [SerializeField] private float _scaleByClick;
    [SerializeField] private Transform _sptireButton;
    [SerializeField] private float _speedChenge;
    [SerializeField] private LayerMask _layers;
    private List<Action> _actions = new List<Action>();
    private float _startScale;
    private List<GameObject> _clickedButton = new List<GameObject>();
    private float _currentScale;
    private Coroutine _currentCoroutine;
    private bool _isClick;

    private void Start()
    {
        _startScale = _sptireButton.localScale.y;
        _currentScale = _startScale;
        if(_actionsObject != null)
        {
            for (int i = 0; i < _actionsObject.Count; i++)
            {
                _actions.Add(_actionsObject[i].GetComponent<Action>());
            }
        }
    }

    public void SetActionObject(List<GameObject> actions)
    {
        _actionsObject = actions;
    }

    public void SetPosition()
    {
        RaycastHit2D hit = new RaycastHit2D();
        hit = Physics2D.Raycast(transform.position, -transform.up, 100, _layers);
        if (hit.collider != null)
        {
            transform.root.position = hit.point;
        }
    }

    public Transform GetTransformActiveObject()
    {
        if(_actionsObject != null)
        {
            if(_actionsObject.Count > 0)
            {
                if(_actionsObject[0].transform.parent != null)
                {
                    return _actionsObject[0].transform.parent;
                }
                else
                {
                return _actionsObject[0].transform;
                }
            }
            else
            {
                return null;
            }
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
            _isClick = true;
            if(_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
            _currentCoroutine =  StartCoroutine(ColorByClick());
            StartCoroutine(StartLaunch());
        }
        _clickedButton.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _clickedButton.Remove(collision.gameObject);
        if (_clickedButton.Count == 0)
        {
            _isClick = false;
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

           _currentCoroutine =  StartCoroutine(ColorByClickUp());
            StartCoroutine(StartLaunch());
        }
    }
    private IEnumerator StartLaunch()
    {
        for (int i = 0; i < _actions.Count; i++)
        {
            yield return new WaitForSeconds(_delayLaunch);
            _actions[i].Launch(_isClick);
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
    void Launch(bool state);
}

