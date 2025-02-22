using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeAnimationController : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private UsePlayerObject _usePlayerObject;

    private void Awake()
    {
        _usePlayerObject.e_put += PutAnimation;
        _usePlayerObject.e_take += IdleAnimation;
    }

    private void OnDisable()
    {
        _usePlayerObject.e_put -= PutAnimation;
        _usePlayerObject.e_take -= IdleAnimation;
    }

    private void PutAnimation()
    {
        _animator.SetTrigger("Idle");
    }

    private void IdleAnimation()
    {
        _animator.SetTrigger("Think");
    }
}
