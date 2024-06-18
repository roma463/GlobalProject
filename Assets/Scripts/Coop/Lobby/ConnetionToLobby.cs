using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class ConnetionToLobby : MonoBehaviourPunCallbacks
{
    [SerializeField] private UnityEngine.UI.Button _startGameButton;
    [SerializeField] private TextMeshProUGUI _playerIsConnetion;
    [SerializeField] private TextMeshProUGUI _waitingConnection;
    [SerializeField] private TextMeshProUGUI _idServerLevel;
    [SerializeField] private LevelList _levelList;
    [SerializeField] private PhotonView _photonView;

    private void Start()
    {
        _idServerLevel.text = PhotonNetwork.CurrentRoom.Name;
        if (PhotonNetwork.IsMasterClient)
        {
            _startGameButton.gameObject.SetActive(true);
            IsConnectedPlayer(false);
        }
    }

    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            _startGameButton.gameObject.SetActive(true);
            IsConnectedPlayer(false);
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            IsConnectedPlayer(true);
        }
    }
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        if(PhotonNetwork.IsMasterClient)
        {
            IsConnectedPlayer(false);
        }
    }

    private void IsConnectedPlayer(bool isConnected)
    {
        _startGameButton.interactable = isConnected;
        _playerIsConnetion.gameObject.SetActive(isConnected);
        _waitingConnection.gameObject.SetActive(!isConnected);
    }
    
    public void StartLoadLevel()
    {
        var joinLevelIndex = SaveGame.Instance.Saves.LevelJointsIndex;
        _photonView.RPC(nameof(LoadLevel), RpcTarget.All, joinLevelIndex);
    }

    [PunRPC]
    public void LoadLevel(int indexLevel)
    {
        PhotonNetwork.LoadLevel(_levelList.GetBuildIndexBySaveForJoinMode(indexLevel));
    }
}
