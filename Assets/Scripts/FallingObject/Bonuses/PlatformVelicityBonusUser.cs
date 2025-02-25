public class PlatformVelicityBonusUser : BonusUser
{
    private float _velocityMultipler;
    private PlayerController _playerController; 

    public override BonusType BonusType => BonusType.slowUp;

    public PlatformVelicityBonusUser(PlayerController playerController, float velocityMultipler)
    {
        _playerController = playerController;
        _velocityMultipler = velocityMultipler;
    }

    public override void DisableBonus()
    {
        _playerController.VeclocityMultipler = 1f;
    }

    public override void EnableBonus()
    {
        _playerController.VeclocityMultipler = _velocityMultipler;
    }
}
