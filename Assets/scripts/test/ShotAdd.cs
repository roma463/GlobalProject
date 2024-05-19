using UnityEngine;

public class ShotAdd : MonoBehaviour
{
    [SerializeField] private int _countBullet;
    [SerializeField] private bool _readFerstCollision = true;
    private bool _isFerstCollisionEnter = true;
    private bool _isFerstCollisonExit = true;
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_readFerstCollision)
        {
            if (_isFerstCollisionEnter)
            {
                GunState.Instance.AddBullet(_countBullet);
                //_positionGunPlayer.OnEnableGun();
                _isFerstCollisionEnter = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (_isFerstCollisonExit)
        {
            //_positionGunPlayer.OnDisableGun();
            _isFerstCollisonExit = false;
        }
    }
}
