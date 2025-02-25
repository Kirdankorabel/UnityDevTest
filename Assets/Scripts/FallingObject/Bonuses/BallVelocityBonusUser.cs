using System.Collections.Generic;

public class BallVelocityBonusUser : BonusUser
{
    private List<BallController> _ballControllers;
    private float _velocityMultipler;

    public override BonusType BonusType => BonusType.sloweDown;

    public BallVelocityBonusUser(List<BallController> balls, float velocityMultipler)
    {
        _ballControllers = balls;
        _velocityMultipler = velocityMultipler;
    }

    public override void DisableBonus()
    {
        _ballControllers.ForEach(ball => ball.VeclocityMultipler = 1f);
        IsActive = false;
    }

    public override void EnableBonus()
    {
        _ballControllers.ForEach(ball => ball.VeclocityMultipler = _velocityMultipler);
        IsActive = true;
    }
}
