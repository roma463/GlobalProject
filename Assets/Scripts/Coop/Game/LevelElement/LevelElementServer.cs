using Photon.Pun;
using UnityEngine;

public class LevelElementServer : MonoBehaviour
{
    [SerializeField] private PhotonView _photonView;

    private void Awake()
    {
        if (!PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.Destroy(gameObject);
        }
    }
}
