using UnityEngine;

public class InvisibleBlock : MonoBehaviour, Action
{
    [SerializeField] private bool _startActive;
    private void Start()
    {
        ActiveThisObject(_startActive);
    }
    public void launch()
    {
        ActiveThisObject(!gameObject.activeInHierarchy);
    }
    private void ActiveThisObject(bool stateActive)
    {
        gameObject.SetActive(stateActive);
    }
}
