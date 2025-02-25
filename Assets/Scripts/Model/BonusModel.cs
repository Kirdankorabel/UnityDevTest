public class BonusModel
{
    private float _duration;
    private BonusType _bonusType;

    public event System.Action<BonusModel> OnBonusEnded;

    public BonusType BonusType => _bonusType;
    public float Duration => _duration;

    public BonusModel(float duration, BonusType bonusType)
    {
        _duration = duration;
        _bonusType = bonusType;
    }

    public void AddDuration(float time)
    {
        _duration += time;
        if (_duration < 0)
        {
            OnBonusEnded?.Invoke(this);
        }
    }
}
