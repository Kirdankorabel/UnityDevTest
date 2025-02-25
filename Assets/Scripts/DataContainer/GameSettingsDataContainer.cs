using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/DataContainer/GameSettings", order = 1)]
public class GameSettingsDataContainer : ScriptableObject
{
    [SerializeField] private float _ballSpawnCooldown;
    [SerializeField] private float _bonusSpawnCooldown;
    [SerializeField] private float _playerVelocity;

    public float BallSpawnCooldown => _ballSpawnCooldown;
    public float BonusSpawnCooldown => _bonusSpawnCooldown;
    public float PlayerVelocity => _playerVelocity;
}
