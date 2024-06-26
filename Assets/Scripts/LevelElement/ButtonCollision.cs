using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonCollision : MonoBehaviour
{
    [SerializeField] private float _delayLaunch;
    [SerializeField] private ActionObject[] _actionsObject;
    [Range(0,1)]
    [SerializeField] private float _scaleByClick;
    [SerializeField] private Transform _spriteButton;
    [SerializeField] private float _speedChenge;
    [SerializeField] private LayerMask _layers;
    [SerializeField] private GameObject _sprite;
    private List<IAction> _actionInterface = new List<IAction>();
    private float _openScale;
    private Coroutine _currentCoroutine;
    private bool _isClick;
    private int _countClickedObject = 0;

    private void Start()
    {
        _openScale = _spriteButton.localScale.y;
        if(_actionsObject != null)
        {
            for (int i = 0; i < _actionsObject.Length; i++)
            {
                _actionInterface.Add(_actionsObject[i].gameObject.GetComponent<IAction>());
            }
        }
    }

    public Transform GetTransformActiveObject()
    {
        if(_actionsObject.Length != 0)
        {
            if (_actionsObject[0].transform.parent != null)
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
            throw new System.Exception(" нопка ничего не активирует");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_countClickedObject == 0)
        {
            _isClick = true;
            if(_currentCoroutine != null)
            {
                StopCoroutine(_currentCoroutine);
            }
            _currentCoroutine =  StartCoroutine(AnimationClick(_scaleByClick));
            StartCoroutine(StartLaunch());
        }
        _countClickedObject++;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (!gameObject.activeInHierarchy)
            return;

        _countClickedObject--;
        if (_countClickedObject == 0)
        {
            _isClick = false;
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            _currentCoroutine = StartCoroutine(AnimationClick(_openScale));
            StartCoroutine(StartLaunch());
        }
    }

    private IEnumerator StartLaunch()
    {
        for (int i = 0; i < _actionInterface.Count; i++)
        {
            yield return new WaitForSeconds(_delayLaunch);
            _actionInterface[i].Launch(_isClick);
        }
    }

    private IEnumerator AnimationClick(float targetScale)
    {
        var targetVector = new Vector2(_spriteButton.transform.localScale.x, targetScale);
        while((Vector2)_spriteButton.localScale != targetVector)
        {
            _spriteButton.localScale = Vector2.MoveTowards(_spriteButton.localScale, targetVector, Time.deltaTime * _speedChenge);
            yield return null;
        }
    }
}

public interface IAction
{
    void Launch(bool state);
}

