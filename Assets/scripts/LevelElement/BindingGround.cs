using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[ExecuteAlways]
public class BindingGround : MonoBehaviour
{
    [SerializeField] private Button _button;
    [SerializeField] private List<GameObject> _actionsObject = new List<GameObject>();

    [ExecuteAlways]
    private void Start()
    {
        if (_actionsObject.Count > 0)
        {
            _button.SetActionObject(_actionsObject);
        }
        _button.SetPosition();
    }

    public void ButtonClick()
    {
        if(_actionsObject.Count > 0)
        {
            _button.SetActionObject(_actionsObject);
        }
        _button.SetPosition();
    }
}

[CustomEditor(typeof(BindingGround))]
public class ButtonPositionSetDown : Editor
{
    public override void OnInspectorGUI()
    {
        BindingGround button = (BindingGround)target;
        DrawDefaultInspector();
        if (GUILayout.Button("PositionSetDown"))
        {
            button.ButtonClick();
        }
    }
}
