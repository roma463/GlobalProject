using UnityEngine;

public class AchievementCollision : MonoBehaviour
{
    [SerializeField] private AnimationAchivement _play;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out PlayerCollision player))
        {
            _play.StartAchivenemt("Красаучик ваще");
        }
    }
}
