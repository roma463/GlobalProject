using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementCollision : MonoBehaviour
{
    [SerializeField] private AnimationAchivement _play;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.parent.TryGetComponent(out PlayerMove pl))
        {
            _play.StartAchivenemt("Красаучик ваще");
        }
    }
}
