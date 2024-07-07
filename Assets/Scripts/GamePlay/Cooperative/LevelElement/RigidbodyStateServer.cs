using Photon.Pun;
using UnityEngine;

public class RigidbodyStateServer : MonoBehaviour
{
    [SerializeField] private PhotonView _phtoton;
    [SerializeField] private Rigidbody2D _rigidBody;

    private void Start()
    {
        if (!_phtoton.IsMine)
        {
            _rigidBody.isKinematic = true;
            _rigidBody.velocity = Vector3.zero;
            _rigidBody.angularVelocity = 0;
        }
    }
}
