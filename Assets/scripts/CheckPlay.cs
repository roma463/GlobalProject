using UnityEngine;
using UnityEngine.UI;

public class CheckPlay : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Text _achievementText;

    public void StartAchivenemt(string nameAchivenemt)
    {
        _animator.SetTrigger("Start");
        _achievementText.text = nameAchivenemt;
    }

}
