using UnityEngine;

[System.Serializable]
public class BallData : FallingObjectData
{
    [SerializeField] public WallColisionType _wallColisionType;
    [SerializeField] public float _bounceMultipler = 1f;

    public WallColisionType WallColisionType => _wallColisionType;
    public float BounceMultipler => _bounceMultipler;
}
