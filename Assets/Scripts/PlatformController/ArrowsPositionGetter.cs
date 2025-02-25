using UnityEngine;

public class ArrowsPositionGetter : IPositionGetter
{
    private ArrowsPanel _panel;

    public ArrowsPositionGetter(ArrowsPanel panel)
    {
        _panel = panel;
    }

    public float GetPosition(Vector3 currentPosition)
    {
        if(!_panel.isActiveAndEnabled)
        {
            _panel.Enable(true);
        }
        return _panel.GetPosition();
    }
}