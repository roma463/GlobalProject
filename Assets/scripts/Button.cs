using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject _objectAction;
    [SerializeField] private Gradient _chengeColor;
    [SerializeField] private float _speedChenge;
    private Action _action;
    private List<GameObject> _clickedButton = new List<GameObject>();
    private SpriteRenderer _spriteRenderer;
    private float currentColorGradient;
    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        if (_objectAction == null)
            return;
        if (!_objectAction.TryGetComponent(out _action))
        {
            _objectAction = null;
        }
    }
    public Transform GetTransformActiveObject() => _objectAction.transform;
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
        for (; currentColorGradient < 1; currentColorGradient += Time.deltaTime * _speedChenge)
        {
            yield return null;
            _spriteRenderer.color = _chengeColor.Evaluate(currentColorGradient);
        }
    }
    private IEnumerator ColorByClickUp()
    {
        for (; currentColorGradient > 0; currentColorGradient -= Time.deltaTime * _speedChenge)
        {
            yield return null;
            _spriteRenderer.color = _chengeColor.Evaluate(currentColorGradient);
        }
    }
}
public interface Action
{
    void launch();
}

