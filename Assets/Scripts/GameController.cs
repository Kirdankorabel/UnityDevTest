using System.Collections;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private SettingsPanel _selectionPanel;
    [SerializeField] private TopPanel _topPanel;
    [SerializeField] private ResultPanel _resultPanel;
    [SerializeField] private PlayerController _playerMover;
    [SerializeField] private FallZone _fallZone;
    [SerializeField] private BallSpawner _ballSpawner;
    [SerializeField] private BonusManager _bonusManager;
    [SerializeField] private TimeController _timeController;
    [SerializeField] private int _healthPointCount = 5;

    private GameModel _gameModel;
    private IPositionGetter _platformController;

    private void Awake()
    {
        GameSettings.PositionGetter = new MouseTouchPositionGetter();
        _fallZone.OnBallFalled += OnBallFalledHeandler;
        _timeController.OnTimeEnded += OnTimeEndHeandler;
        _resultPanel.OnNewGameStarted += StartGame;
        GameSettings.ScreenWidth = Camera.main.orthographicSize * Camera.main.aspect;
        GameSettings.BestResult = PlayerPrefs.GetInt(GameSettings.BestResultNamePrefName);
    }

    private void Start()
    {
        _ballSpawner.Construct();
        _topPanel.UpdateBestResult(GameSettings.BestResult);
        StartGame();
    }

    public void StartGame()
    {
        SetGameModel(new GameModel());
        _playerMover.StartMoving();
        _topPanel.UpdaGameResult(0);
        _ballSpawner.StartBallSpawning();
        _timeController.StartTimer();
        _bonusManager.StarBonusSpawning();
    }

    private void OnBallFalledHeandler(int count)
    {
        _gameModel.AddPoitns(count);
    }

    private void SetGameModel(GameModel gameModel)
    {
        if (_gameModel != null)
        {
            _gameModel.OnCountUpdated -= _topPanel.UpdaGameResult;
        }
        _gameModel = new GameModel();
        _gameModel.OnCountUpdated += _topPanel.UpdaGameResult;
    }

    private void OnTimeEndHeandler()
    {
        var points = 0;
        _ballSpawner.Balls.ForEach(ball => points += ball.GetPointCount());
        _gameModel.AddPoitns(points);
        _resultPanel.Open(_gameModel.Count);
        if(_gameModel.Count > GameSettings.BestResult)
        {
            _topPanel.UpdateBestResult(_gameModel.Count);
            GameSettings.BestResult = _gameModel.Count;
            PlayerPrefs.SetInt(GameSettings.BestResultNamePrefName, _gameModel.Count);
        }
    }
}
