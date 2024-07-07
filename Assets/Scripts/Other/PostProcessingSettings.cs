using UnityEngine;

public class PostProcessingSettings : MonoBehaviour
{
    public static PostProcessingSettings Instance { get; private set; }
    [SerializeField] private GameObject _globalVolume;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        UpdateChanged(SaveGame.Instance.Saves.Settings.PostProcessing);
    }

    public void UpdateChanged(bool state)
    {
        _globalVolume.SetActive(state);
    }
}
