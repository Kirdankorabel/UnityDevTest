using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BallController : FallingObject
{
    [SerializeField] private List<TMP_Text> _scoreTexts;

    private float _bounceMultipler;
    private BallModel _ballModel;
    private IHorizontalPositionChecker _positionChecker;
    private IWallCollisionHeandler _wallCollisionHeandler;

    public Vector3 Position => transform.position;

    public BallController SetWallCollisionLogic(IHorizontalPositionChecker positionChecker, IWallCollisionHeandler wallCollisionHeandler)
    {
        if(_ballModel != null)
        {
            _ballModel.OnUpdate -= UpdateCouter;
        }
        _ballModel = new BallModel();
        _ballModel.OnUpdate += UpdateCouter;
        _positionChecker = positionChecker;
        _wallCollisionHeandler = wallCollisionHeandler;
        UpdateCouter(0);
        return this;
    }

    public BallController SetBallData(BallData ballData)
    {
        _bounceMultipler = ballData.BounceMultipler;
        base.SetFallingObjectData(ballData);
        return this;
    }

    public override void PlayerColisionAction(float velocityX)
    {
        if(_velocityY > 0)
        {
            _velocityX = velocityX;
            _velocityY = -_velocityY * _bounceMultipler;
            _ballModel.AddPoint();
        }
    }

    public void Inverse()
    {
        _velocityX = -_velocityX;
    }

    public int GetPointCount()
    {
        if(_ballModel.IsCounted)
        {
            return 0;
        }
        else
        {
            _ballModel.Count();
            return _ballModel.Counter;
        }
    }

    protected override void CheckPosiiton()
    {
        base.CheckPosiiton();
        if (!_positionChecker.CheckPosition(Position, _minX, _ballData.Size))
        {
            _wallCollisionHeandler.OnWallCollisionHeared(this);
        }
    }

    private void UpdateCouter(int counter)
    {
        string str = counter.ToString();
        _scoreTexts.ForEach(text => text.text = str);
    }
}
