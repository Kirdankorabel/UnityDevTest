using UnityEngine;

[System.Serializable]
public class FallingObjectData
{
    [SerializeField] public Color _color = Color.white;
    [SerializeField] public float _size = 1f;
    [SerializeField] public float _speed = 2f;
    [SerializeField] public float _maxSpeed = 2f;
    [SerializeField] public float _gravity = 9.8f;

    public Color Color => _color;
    public float Size => _size;
    public float Speed => _speed;
    public float MaxSpeed => _maxSpeed;
    public float Gravity => _gravity;
}
