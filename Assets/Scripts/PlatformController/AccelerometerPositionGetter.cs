using UnityEngine;

public class AccelerometerPositionGetter : IPositionGetter
{
    public float GetPosition(Vector3 currentPosition)
    {
        float accelerationX = Mathf.Clamp(Input.acceleration.x, -1f, 1f);
        return accelerationX;
    }
}