using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthPointView : MonoBehaviour
{
    [SerializeField] private Image _image;
    [SerializeField] private float _fadeDuration;
    [SerializeField] private Color _defaultColor;

    private Sequence _seq;

    public bool IsActive { get; private set; } = true;

    public void Enabele()
    {
        IsActive = true;
        if (_seq != null)
        {
            _seq.Kill();
        }
        gameObject.SetActive(true);
        _image.color = _defaultColor;
    }

    public void Disable()
    {
        IsActive = false;
        if (_seq != null)
        {
            _seq.Kill();
        }
        _seq = DOTween.Sequence();
        _seq.Append(_image.DOFade(0, _fadeDuration))
           .Append(_image.DOFade(1, _fadeDuration))
           .Append(_image.DOFade(0, _fadeDuration)); 
    }
}
