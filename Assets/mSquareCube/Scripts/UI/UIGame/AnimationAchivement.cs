using UnityEngine;
using UnityEngine.UI;

public class AnimationAchivement : MonoBehaviour
{
    public static AnimationAchivement Instance { private set; get; }
    [SerializeField] private Animator _animator;
    [SerializeField] private Text _achievementText;

    private void Awake()
    {
        Instance = this;
    }

    public void StartAchivenemt(string nameAchivenemt)
    {
        _animator.SetTrigger("Start");
        _achievementText.text = nameAchivenemt;
    }

}
