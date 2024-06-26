using UnityEngine;

[ExecuteAlways]
public class NoiseScreenMenu : MonoBehaviour
{
    [SerializeField] private Cyan.Blit _blit;
    [SerializeField] private bool _isActiveStart;

    [ExecuteAlways]
    private void Awake()
    {
        _blit.SetActive(_isActiveStart);
    }
}
