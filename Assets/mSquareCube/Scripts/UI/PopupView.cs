using UnityEngine;
using UnityEngine.UI;

public abstract class PopupView<T> : MonoBehaviour where T : PopupParameters
{

    public abstract void Setup(T settings);

    public virtual void Show()
    {
        gameObject.SetActive(true);
    }

    public virtual void Hide() 
    {
        gameObject.SetActive(false);
    }
}
