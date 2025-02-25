using UnityEngine;
using UnityEngine.UI;

public class BonusController : FallingObject
{
    [SerializeField] private Image _image;

    private BonusModel _bonusModel;

    public event System.Action<BonusModel> OnPickUp;

    public BonusController SetBonus(BonusModel bonusModel)
    {
        _bonusModel = bonusModel;
        return this;
    }

    public BonusController SetImage(Sprite sprite)
    {
        _image.sprite = sprite;
        return this;
    }

    public override void PlayerColisionAction(float velocityX)
    {
        OnPickUp?.Invoke(_bonusModel);
        ReleseObject();
    }
}
