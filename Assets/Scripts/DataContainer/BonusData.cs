using UnityEngine;

[System.Serializable]
public class BonusData : FallingObjectData
{
    [SerializeField] private BonusType _bonusType;
    [SerializeField] private float _duration;
    [SerializeField] private Sprite _sprite;

    public BonusType BonusType => _bonusType;
    public float Duration => _duration;
    public Sprite Sprite => _sprite;
}
