public class ColisionLogicCreator
{
    private IWallCollisionHeandler _wallCollisionHeandler;
    private IHorizontalPositionChecker _positionChecker;

    public WallColisionType WallColisionType { get; private set; }

    public ColisionLogicCreator(WallColisionType type, IWallCollisionHeandler wallCollisionHeandler, IHorizontalPositionChecker positionChecker)
    {
        WallColisionType = type;
        _positionChecker = positionChecker;
        _wallCollisionHeandler = wallCollisionHeandler;
    }

    public void CreateColisionLogic(BallController ball)
    {
        ball.SetWallCollisionLogic(_positionChecker, _wallCollisionHeandler);
    }
}
