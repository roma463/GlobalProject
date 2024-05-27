using UnityEngine;

public abstract class ActionObject : MonoBehaviour, Action
{
    public bool IsActive { private set; get; }
    [SerializeField] private bool _startActive;
    private AnimationActivityObject _animationStateObject;
    private int _countPressedButton = 0;

    public virtual void Start()
    {
        IsActive = _startActive;
    }

    public virtual void Launch(bool state)
    {
        if (_startActive)
            state = !state;

        if (state)
            _countPressedButton++;
        else
            _countPressedButton--;

        _countPressedButton = Mathf.Clamp(_countPressedButton, 0, _countPressedButton);

        if (_countPressedButton == 0)
        {
            Release();
            IsActive = false;
        }
        else if (_countPressedButton == 1 && !IsActive)
        {
            PressState();
            IsActive = true;
        }
    }

    public abstract void Release();
    public abstract void PressState();

}
