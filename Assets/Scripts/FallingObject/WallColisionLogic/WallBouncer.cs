public class WallBouncer : IWallCollisionHeandler
{
    public void OnWallCollisionHeared(BallController ball)
    {
        ball.Inverse();
    }
}
