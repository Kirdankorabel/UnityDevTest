using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using System.Collections;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _bonusVelocityMultipler = 2f;
    [SerializeField] private float _bounceMultiplier = 2f;
    [SerializeField] private float _bonusSize = 1.5f;

    private Queue<float> _speedHistory = new Queue<float>();
    private float _recordTime = 0.5f;
    private List<BonusUser> _bonusUsers = new List<BonusUser>();

    public Vector3 Position => transform.position;
    public float VeclocityMultipler { get; set; } = 1f;

    private void Start()
    {
        BonusManager.OnBonusAdded += OnBonusAddedHeandeler;
        BonusManager.OnBonusEnded += OnBonusEndedHeandler;
        _bonusUsers.Add(new PlatformSizeBonusUser(transform, _bonusSize));
        _bonusUsers.Add(new PlatformVelicityBonusUser(this, _bonusVelocityMultipler));
        _sizeX = GameSettings.ScreenWidth;
    }

    public void StartMoving()
    {
        StopAllCoroutines();
        _bonusUsers.ForEach(user => user.DisableBonus());
        StartCoroutine(MovePlatformCprutine());
    }
    private float _sizeX;

    private void UpdatePosition(float direction)
    {
        if (direction == 0) return;

        Vector3 targetPosition = transform.position + Vector3.right * direction;
        targetPosition.x = Mathf.Clamp(targetPosition.x, -_sizeX, _sizeX);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, _speed * VeclocityMultipler * Time.deltaTime);
    }

    private void RecordSpeed(float currentSpeed)
    {
        _speedHistory.Enqueue(currentSpeed);
        if (_speedHistory.Count > Mathf.RoundToInt(_recordTime / Time.deltaTime))
        {
            _speedHistory.Dequeue();
        }
    }

    public float GetAverageSpeed()
    {
        return _speedHistory.Count > 0 ? _speedHistory.Average() : 0f;
    }

    private void OnBonusAddedHeandeler(BonusType bonusType)
    {
        _bonusUsers.FindAll(b => b.BonusType == bonusType).ForEach(user => user.EnableBonus());
    }

    private void OnBonusEndedHeandler(BonusType bonusType)
    {
        _bonusUsers.FindAll(b => b.BonusType == bonusType).ForEach(user => user.DisableBonus());
    }

    private void OnTriggerEnter(Collider other)
    {
        var ball = other.gameObject.GetComponent<FallingObject>();
        if (ball != null)
        {
            float relativePosition = (ball.transform.position.x - transform.position.x) / (transform.localScale.x / 2);
            float bounceForce = GetAverageSpeed() + (relativePosition * _bounceMultiplier);
            ball.PlayerColisionAction(bounceForce);
        }
    }

    private IEnumerator MovePlatformCprutine()
    {
        while (true)
        {
            if (!GameSettings.IsPaused)
            {
                UpdatePosition(GameSettings.PositionGetter.GetPosition(Position));
            }
            yield return null;
        }
    }
}
