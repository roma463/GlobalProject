using Photon.Pun;
using UnityEngine;

public class UsePlayerObjectServer : UsePlayerObject
{
    [SerializeField] private PhotonView _photonView;
    [SerializeField] private PhotonTransformView _photonTransformView;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private ViewSynchronization _viewSynchronization;

    private void OnValidate()
    {
        if(_photonView != null)
            _photonView.Synchronization = _viewSynchronization;
    }

    public PhotonView GetPhotonView() => _photonView;

    public override void Take()
    {
        base.Take();
        if(_photonView.IsMine)
        {
            _photonView.Synchronization = ViewSynchronization.Off;
        }
        else
        {
            ChangeIsKinematic(false);

        }
    }

    public override void Put()
    {
        base.Put();
        _photonTransformView.EnablePosition();
        if (_photonView.IsMine)
        {
            _photonView.Synchronization = _viewSynchronization;
            print(_rigidbody.velocity);
        }
        else
        {
            ChangeIsKinematic(true);
        }
    }

    private void ChangeIsKinematic(bool state)
    {
        _rigidbody.isKinematic = state;
        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = 0;
    }
}
