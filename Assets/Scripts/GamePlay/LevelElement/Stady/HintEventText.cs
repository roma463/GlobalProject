using UnityEngine;
using UnityEngine.Events;

public class HintEventText : Hint
{
    [SerializeField] private UnityEvent _eventShow;
    private InputButton _inputButton;

    private void Start()
    {
        _inputButton = InputButton.Instance;
    }

    public override void AnimationShow()
    {
        base.AnimationShow();
        //_inputButton.KeyE.AddListener(() => _eventShow?.Invoke());
    }

    public override void AnimationHide()
    {
        base.AnimationHide();
    }
}
