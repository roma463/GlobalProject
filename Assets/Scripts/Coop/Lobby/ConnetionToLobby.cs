using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;

public class ConnetionToLobby : MonoBehaviourPunCallbacks
{
    [SerializeField] private TextMeshProUGUI _massage;
    [SerializeField] private TextMeshProUGUI _idServerLevel;
    [SerializeField] private LevelList _levelList;
    [SerializeField] private PhotonView _photonView;

    private void Start()
    {
        _idServerLevel.text = PhotonNetwork.CurrentRoom.Name;
#if UNITY_EDITOR
        var joinLevelIndex = SaveGame.Instance.Saves.LevelJointsIndex;
        _photonView.RPC(nameof(LoadLevel), RpcTarget.All, joinLevelIndex);
#endif
    }


    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            var joinLevelIndex = SaveGame.Instance.Saves.LevelJointsIndex;
            _photonView.RPC(nameof(LoadLevel), RpcTarget.All, joinLevelIndex);
        }
    }

    [PunRPC]
    public void LoadLevel(int indexLevel)
    {
        PhotonNetwork.LoadLevel(_levelList.GetBuildIndexBySaveForJoinMode(indexLevel));
    }
}
