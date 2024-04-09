using UnityEngine;

//[RequireComponent (typeof(AnimationActivityObject))]
public abstract class ActionObject : MonoBehaviour, Action
{
    public bool IsActive { private set; get; }
    [SerializeField] private bool _startActive;
    private AnimationActivityObject _animationStateObject;

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
        //_animationStateObject.Change(IsActive);
    }

    public virtual void RemoveFromListButton()
    {

    }

    public virtual void launch()
    {
        IsActive = !IsActive;
        //_animationStateObject.Change(IsActive);
    }
}
