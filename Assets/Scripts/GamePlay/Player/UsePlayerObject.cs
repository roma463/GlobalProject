using UnityEngine;

public class UsePlayerObject : MonoBehaviour
{
    public event System.Action e_take;
    public event System.Action e_put;

    public bool UseNow { private set; get; }

    public virtual void Take()
    {
        e_take?.Invoke();
        UseNow = true;
    }
    public virtual void Put()
    {
        e_put?.Invoke();
        UseNow = false;
    }
}
