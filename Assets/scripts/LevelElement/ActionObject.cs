using UnityEngine;

public abstract class ActionObject : MonoBehaviour, Action
{
    public bool IsActive { private set; get; }
    [SerializeField] private bool _startActive;
    private AnimationActivityObject _animationStateObject;
    private int _countPressedButton = 0;

    private void OnValidate()
    {
        if(gameObject.TryGetComponent(out AnimationActivityObject animation))
            _animationStateObject = animation;
        else if(gameObject.GetComponentInChildren<AnimationActivityObject>() != null)
        {
            _animationStateObject = gameObject.GetComponentInChildren<AnimationActivityObject>();
        }
    }

    public virtual void Start()
    {
        IsActive = _startActive;
    }

    public virtual void Launch(bool state)
    {
        if (state)
            _countPressedButton++;
        else
            _countPressedButton--;

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
