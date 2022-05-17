using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private GameObject _objectAction;
    private Action _action;
    private List<GameObject> _clickedButton = new List<GameObject>();
    private void Start()
    {
        if (_objectAction == null)
            return;
        if (!_objectAction.TryGetComponent(out _action))
        {
            _objectAction = null;
        }
    }
    public Transform GetTransformActiveObject()
    {
        return _objectAction.transform;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_clickedButton.Count == 0)
            _action.launch();
        _clickedButton.Add(collision.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _clickedButton.Remove(collision.gameObject);
        if (_clickedButton.Count == 0)
            _action.launch();
    }
}
public interface Action
{
    void launch();
}

