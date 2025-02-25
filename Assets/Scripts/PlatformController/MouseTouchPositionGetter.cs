using System;
using UnityEngine;

public class MouseTouchPositionGetter : IPositionGetter
{
    private float _minDist = 0.1f;
    private Plane _plane = new Plane(Vector3.forward, Vector3.zero);

    public float GetPosition(Vector3 currentPosition)
    {
        var result = 0;
        if (Input.GetMouseButton(0)) 
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (_plane.Raycast(ray, out float distance) && Math.Abs(ray.GetPoint(distance).x - currentPosition.x) > _minDist)
            {
                result = ray.GetPoint(distance).x < currentPosition.x ? -1 : 1;
            }
        }
        return result;
    }
}
