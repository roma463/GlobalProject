using Photon.Pun;

public class FinishServer : Finish
{
    private int _countFinishPlayer;
    
    public override void WinPlayer()
    {
        _countFinishPlayer++;
        if(_countFinishPlayer == 2)
        {
            if(PhotonNetwork.IsMasterClient)
            {
                base.WinPlayer();
            }
        }
    }
}
