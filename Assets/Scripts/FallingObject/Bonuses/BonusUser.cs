public abstract class BonusUser
{
    public bool IsActive { get;protected set; }
    public abstract BonusType BonusType { get; }
    public abstract void EnableBonus();
    public abstract void DisableBonus();
}
