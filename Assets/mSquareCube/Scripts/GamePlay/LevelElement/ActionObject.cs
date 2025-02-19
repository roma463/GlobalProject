using UnityEngine;

public abstract class ActionObject : MonoBehaviour, IAction
{
    public bool IsActive { private set; get; }

    [SerializeField] private bool _startActive;

    private int _countPressedButton = 0;

    public virtual void Start()
    {
        IsActive = _startActive;
    }

    public virtual void Launch(bool state)
    {
        state = state != _startActive;
        if (state)
        {
            PressState();
            IsActive = true;
        }
        else
        {
            Release();
            IsActive = false;
        }
    }

    public abstract void Release();
    public abstract void PressState();

}
