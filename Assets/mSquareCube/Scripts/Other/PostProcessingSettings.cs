using UnityEngine;
using Zenject;

public class PostProcessingSettings : MonoBehaviour
{
    public static PostProcessingSettings Instance { get; private set; }
    [SerializeField] private GameObject _globalVolume;
    private SaveGame _save;

    [Inject]
    public void Construct(SaveGame save)
    {
        _save = save;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateChanged(_save.Data.Settings.PostProcessing);
    }

    public void UpdateChanged(bool state)
    {
        _globalVolume.SetActive(state);
    }
}
