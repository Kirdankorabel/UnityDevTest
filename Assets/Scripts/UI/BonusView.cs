using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BonusView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private BonusType _bonusType;
    [SerializeField] private TMP_Text _timeText;

    private float _lastTime;

    public BonusType BonusType => _bonusType;

    public BonusView SetData(BonusData data)
    {
        _image.sprite = data.Sprite;
        _bonusType = data.BonusType;
        return this;
    }

    public void Enable(bool value)
    {
        gameObject.SetActive(value);
        if (value )
        {
            _lastTime = float.MaxValue;
        }
    }

    public void UpdateView(float time)
    {
        if (Mathf.Abs(_lastTime - time) > 0.5f)
        {
            _lastTime = time;
            _timeText.text = ((int)time).ToString();
        }
    }
}
