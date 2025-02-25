using System.Collections.Generic;
using UnityEngine;

public class BonusPanel : MonoBehaviour
{
    [SerializeField] private BonusView _viewPrefab;
    [SerializeField] private Transform _rootTransform;
    [SerializeField] private BonusDataContainer _bonusDataContainer;

    private Dictionary<BonusType, BonusView> _views;

    private void Awake()
    {
        _views = new Dictionary<BonusType, BonusView>();
        BonusData data;
        BonusView bonusView;
        for (var i = 0; i < _bonusDataContainer.Datas.Count; i++)
        {
            data = _bonusDataContainer.Datas[i];
            bonusView = Instantiate(_viewPrefab, _rootTransform).SetData(data);
            _views.Add(data.BonusType, bonusView);
            bonusView.Enable(false);
        }
    }

    public void UpdateView(BonusType type, float duration)
    {
        _views[type].UpdateView(duration);
    }

    public void EnableBonus(BonusType type, bool enable)
    {
        _views[type].Enable(enable);
    }
}
