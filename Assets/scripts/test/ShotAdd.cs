using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotAdd : MonoBehaviour
{
    [SerializeField] private int _countBullet;
    [SerializeField] private PositionGun _positionGunPlayer;
    [SerializeField] private bool _readFerstCollision = true;
    private bool _isFerstCollisionEnter = true;
    private bool _isFerstCollisonExit = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_readFerstCollision)
        {
            if (_isFerstCollisionEnter)
            {
                _positionGunPlayer.AddBullet(_countBullet);
                _positionGunPlayer.OnEnableGun();
                _isFerstCollisionEnter = false;
            }
        }
        _readFerstCollision = true;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isFerstCollisonExit)
        {
            _positionGunPlayer.OnDisableGun();
            _isFerstCollisonExit = false;
        }
    }
}
