using UnityEngine;

public class WallSliper : IWallCollisionHeandler
{
    public void OnWallCollisionHeared(BallController ball)
    {
        ball.SetPosition(new Vector3(-ball.Position.x, ball.Position.y, ball.Position.z));
    }
}
