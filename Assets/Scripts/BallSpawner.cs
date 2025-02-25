using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [SerializeField] private BallDataContainer _ballDataContainer;
    [SerializeField] private BallFactory _ballFactory;
    [SerializeField] private float _velocityBonusMultipler;
    [SerializeField] private float _spawnHeiht;
    [SerializeField] private float _cooldown;

    private List<BallController> _balls;
    private List<BonusUser> _bonusUsers = new List<BonusUser>();
    private float _spawnTime;
    private float _spawnAreaSize;

    public List<BallController> Balls => _balls;

    private List<ColisionLogicCreator> _colisionLogicCreators = new List<ColisionLogicCreator>()
    {
        new ColisionLogicCreator(WallColisionType.Bounce, new WallBouncer(), new ExcludedPositionChecker()),
        new ColisionLogicCreator(WallColisionType.Slip, new WallSliper(), new IncludedPostionChecker ()),
    };

    public void Construct()
    {
        _balls = new List<BallController>();
        BonusManager.OnBonusAdded += OnBonusAddedHeandeler;
        BonusManager.OnBonusEnded += OnBonusEndedHeandler;
        _bonusUsers.Add(new BallVelocityBonusUser(_balls, _velocityBonusMultipler));
    }

    public void StartBallSpawning()
    {
        StopAllCoroutines();
        _bonusUsers.ForEach(user => user.DisableBonus());
        _spawnAreaSize = GameSettings.ScreenWidth;
        if (_balls.Count > 0)
        {
            _balls.ForEach(b => b.ReleseObject());
        }
        _balls.Clear();
        StartCoroutine(SpawnCorutine());
    }

    private IEnumerator SpawnCorutine()
    {
        while (true)
        {
            if (!GameSettings.IsPaused)
            {
                if (_spawnTime <= 0)
                {
                    _spawnTime = _cooldown;
                    var ballData = _ballDataContainer.GetRandomBall();
                    var ball = _ballFactory.GetItem();

                    ball.SetBallData(ballData)
                        .SetPosition(new Vector3(Random.Range(-_spawnAreaSize, _spawnAreaSize), _spawnHeiht))
                        .SetMinPosition(-_spawnHeiht)
                        .SetWalls(_spawnAreaSize);
                    _colisionLogicCreators.Find(item => item.WallColisionType == ballData.WallColisionType).CreateColisionLogic(ball);
                    if (!_balls.Contains(ball))
                    {
                        _balls.Add(ball);
                    }

                    _bonusUsers.FindAll(user => user.IsActive).ForEach(user => user.EnableBonus());
                    ball.StartMoving();
                }
                else
                {
                    _spawnTime -= Time.deltaTime;
                }
            }
            yield return null;
        }
    }


    private void OnBonusAddedHeandeler(BonusType bonusType)
    {
        _bonusUsers.FindAll(b => b.BonusType == bonusType).ForEach(user => user.EnableBonus());
    }

    private void OnBonusEndedHeandler(BonusType bonusType)
    {
        _bonusUsers.FindAll(b => b.BonusType == bonusType).ForEach(user => user.DisableBonus());
    }
}
