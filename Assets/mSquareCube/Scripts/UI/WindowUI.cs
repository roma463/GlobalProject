using UnityEngine;

public class WindowUI : MonoBehaviour
{
    [SerializeField] private GameObject _elements;

    public virtual void Activate()
    {
        _elements.SetActive(true);
    }

    public virtual void Deactivate() 
    {
        _elements.SetActive(false);
    }
}
