using Photon.Pun;

public class GameUISeriver : GameUi
{
    private PhotonView _photonView;
    public void InitPhoton(PhotonView view)
    {
        _photonView = view;
    }
    public override void Restart()
    {
        RestartPhoton();
    }

    public void RestartPhoton()
    {
        PhotonNetwork.LoadLevel(GetIndexCurrentScene());
        _photonView.RPC(nameof(LoadSceneRPC), RpcTarget.Others, GetIndexCurrentScene());
        print("Server restart");

    }

    public override void NextLevel()
    {
        PhotonNetwork.LoadLevel(GetIndexCurrentScene()+1);
    }

    [PunRPC]
    public void LoadSceneRPC(int idScene)
    {
        // Загрузка новой сцены на всех клиентах
        PhotonNetwork.LoadLevel(idScene);
    }
}
