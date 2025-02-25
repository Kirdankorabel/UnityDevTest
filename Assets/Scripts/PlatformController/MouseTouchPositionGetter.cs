using System;
using Unity.VisualScripting;
using UnityEngine;

public class MouseTouchPositionGetter : IPositionGetter
{
    private float _minDist = 0.01f;
    private Plane _plane = new Plane(Vector3.forward, Vector3.zero);

    public float GetPosition(Vector3 currentPosition)
    {
        var result = 0;
        //if (Input.touchCount > 0)
        //{
        //    Touch touch = Input.GetTouch(0);
        //    Vector3 touchPosition = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, 0, Camera.main.nearClipPlane));
        //    result = touchPosition.x > currentPosition.x ? 1 : -1;
        //}
        //else
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
