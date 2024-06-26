using UnityEngine;

public class Music : MonoBehaviour
{
    public static Music Instance { private set; get; }

    private void Awake()
    {
        if (Instance != null)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
    }
}
