using UnityEngine;

public class WindowUI : MonoBehaviour
{
    [SerializeField] private bool _viewIsStart;
    [SerializeField] private GameObject _elements;

    public virtual void Start()
    {
        if (_viewIsStart)
            Show();
        else
            Hide();
    }

    public virtual void Show()
    {
        _elements.SetActive(true);
    }

    public virtual void Hide() 
    {
        _elements.SetActive(false);
    }
}
