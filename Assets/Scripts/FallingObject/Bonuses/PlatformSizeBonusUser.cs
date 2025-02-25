using DG.Tweening;
using UnityEngine;

public class PlatformSizeBonusUser : BonusUser
{    
    private Transform _platformTransform;
    private Vector3 _defaultScale;
    private Vector3 _bonusScale;
    private float _defaultSize;
    private float _bonusSize;
    private float _animationDuration = 0.5f;

    public override BonusType BonusType => BonusType.size;

    public PlatformSizeBonusUser(Transform platformTransform, float bonusSize)
    {
        _platformTransform = platformTransform;
        _bonusSize = bonusSize;
        _defaultScale = platformTransform.localScale;
        _defaultSize = _defaultScale.x;
        _bonusScale = new Vector3(_bonusSize, _defaultScale.y, _defaultScale.z);
    }

    public override void EnableBonus()
    {
        _platformTransform.DOScaleX(_bonusSize, _animationDuration).OnComplete(() => _platformTransform.localScale = _bonusScale);
    }

    public override void DisableBonus()
    {
        _platformTransform.DOScaleX(_defaultSize, _animationDuration).OnComplete(() => _platformTransform.localScale = _defaultScale);
    }
}
