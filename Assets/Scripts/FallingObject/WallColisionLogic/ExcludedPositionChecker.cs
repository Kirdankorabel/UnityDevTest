using UnityEngine;

public class ExcludedPositionChecker : IHorizontalPositionChecker
{
    public bool CheckPosition(Vector3 position, float areaSize, float ballSsize)
    {
        return position.x < -(areaSize) && position.x > (areaSize);
    }
}