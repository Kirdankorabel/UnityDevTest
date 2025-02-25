using System.Collections;
using UnityEngine;

public class TimeController : MonoBehaviour
{
    [SerializeField] private TimeView _timeView;

    private float _time;
    public event System.Action OnTimeEnded;

    public void StartTimer()
    {
        StopAllCoroutines();
        _time = GameSettings.MaxTime;
        _timeView.SetMaxTime(_time);
        _timeView.UpdateView(_time);
        StartCoroutine(TimerCorutine());
        GameSettings.IsPaused = false;
    }

    private IEnumerator TimerCorutine()
    {
        while(_time > 0)
        {
            if(!GameSettings.IsPaused)
            {
                _time -= Time.deltaTime;
                _timeView.UpdateView(_time);
            }
            yield return null;
        }
        _time = 0;
        GameSettings.IsPaused = true;
        OnTimeEnded?.Invoke();
    }
}
