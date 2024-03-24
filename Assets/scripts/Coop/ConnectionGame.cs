using Photon.Pun;
using TMPro;
using UnityEngine;

public class ConnectionGame : MonoBehaviour
{
    [SerializeField] private GameObject _perfab;
    [SerializeField] private Transform _startPosition;
    [SerializeField] private TextMeshPro _idRoom;

    private void Start()
    {
        _idRoom.text = PhotonNetwork.CurrentRoom.Name;
        PhotonNetwork.Instantiate(_perfab.name, _startPosition.position, Quaternion.identity);
    }

}
