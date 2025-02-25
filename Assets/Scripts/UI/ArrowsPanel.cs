using UnityEngine;

public class ArrowsPanel : MonoBehaviour
{
    [SerializeField] private Arrow _leftArrow;
    [SerializeField] private Arrow _rightArrow;

    public void Enable(bool value)
    {
        gameObject.SetActive(value);
    }

    public float GetPosition()
    {
        if (_leftArrow.IsPressed && !_rightArrow.IsPressed)
        {
            return -1;
        }
        else if (!_leftArrow.IsPressed && _rightArrow.IsPressed)
        {
            return 1;
        }
        return 0;
    }
}
