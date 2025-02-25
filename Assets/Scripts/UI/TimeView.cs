using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimeView : MonoBehaviour
{
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private Image _timeImage;

    private float _maxTime;
    private float _lastTime;

    public void SetMaxTime(float maxTime)
    {
        _maxTime = maxTime;
        _lastTime = maxTime;
        _timeText.text = ((int)_maxTime).ToString();
    }

    public void UpdateView(float time)
    {
        if(_lastTime - time > 1f)
        {
            _lastTime = time;
            _timeText.text = ((int)time).ToString();
        }
        _timeImage.fillAmount = (time /  _maxTime);
    }
}
