using UnityEngine;

public class GunState : MonoBehaviour
{
    public static GunState Instance { get; private set; }
    public PositionGun PositionGun {  get; private set; }
    [SerializeField] private bool _startState = true;
    [SerializeField] private GameObject _gunSprite;
    [SerializeField] private Trajectory _trajectory;
    [SerializeField] private TextCountBullet _text;

    private void Awake()
    {
        Instance = this;
        PositionGun = GetComponent<PositionGun>();

        if (!_startState)
            OnDisableGun();
    }

    public void AddBullet(int countAdd)
    {
        PositionGun.AddBullet(countAdd);
        OnEnableGun();
    }

    public void OnDisableGun()
    {
        _trajectory.DisableTrajectoryLine();
        _gunSprite.gameObject.SetActive(false);
        PositionGun.GunDisable();
        _text.DisabledGun();
    }

    public void OnEnableGun()
    {
        _trajectory.EnableTrajectoryLine();
        _gunSprite.gameObject.SetActive(true);
        PositionGun.GunEnable();
        _text.EnabledGun();
    }
}
