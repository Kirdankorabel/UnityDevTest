using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.XR;

public class BonusManager : MonoBehaviour
{
    [SerializeField] private BonusDataContainer _BonusDataContainer;
    [SerializeField] private BonusFactory _bonusFactory;
    [SerializeField] private BonusPanel _panel;
    [SerializeField] private float _spawnHeiht;

    private List<BonusModel> _activeBonuses = new List<BonusModel>();
    private List<BonusController> _bonusControllers = new List<BonusController>();
    private float _spawnTime;
    private float _spawnAreaSize;
    private float _gameAreaSize;
    private float _cooldown;

    public static event System.Action<BonusType> OnBonusAdded;
    public static event System.Action<BonusType> OnBonusEnded;

    public void Construct()
    {
        _cooldown = GameSettings.BonusSpawnCooldown;
        _gameAreaSize = GameSettings.ScreenWidth;
    }

    public void StarBonusSpawning()
    {
        StopAllCoroutines();
        _spawnAreaSize = GameSettings.ScreenWidth;
        if (_bonusControllers != null)
        {
            _bonusControllers.ForEach(b => b.ReleseObject());
        }
        _activeBonuses.ForEach(b => _panel.EnableBonus(b.BonusType, false));
        _activeBonuses.Clear();
        StartCoroutine(SpawnBonusesCorutine());
        StartCoroutine(TimeCorutine());
    }

    public void AddBonus(BonusModel model)
    {
        var bonus = _activeBonuses.Find(b => b.BonusType == model.BonusType);
        if (bonus != null)
        {
            bonus.AddDuration(model.Duration);
            _panel.UpdateView(bonus.BonusType, bonus.Duration);
        }
        else
        {
            _activeBonuses.Add(model);
            OnBonusAdded?.Invoke(model.BonusType);
            model.OnBonusEnded += OnBonusEndedHeandler;
            _panel.EnableBonus(model.BonusType, true);
        }
    }

    private void OnBonusEndedHeandler(BonusModel bonus)
    {
        bonus.OnBonusEnded -= OnBonusEndedHeandler;
        OnBonusEnded?.Invoke(bonus.BonusType);
        _panel.EnableBonus(bonus.BonusType, false);
    }

    private void UpdateBonusDuraction(BonusModel bonusModel, float deltaTime)
    {
        bonusModel.AddDuration(-deltaTime);
        _panel.UpdateView(bonusModel.BonusType, bonusModel.Duration);
    }

    private IEnumerator TimeCorutine()
    {
        var deltaTime = 0f;
        while(true)
        {
            if(!GameSettings.IsPaused)
            {
                deltaTime = Time.deltaTime;
                _activeBonuses.ForEach(b => UpdateBonusDuraction(b, deltaTime));
                _activeBonuses.RemoveAll(b => b.Duration < 0);
            }
            yield return null;
        }
    }

    private IEnumerator SpawnBonusesCorutine()
    {
        while (true)
        {
            if (!GameSettings.IsPaused)
            {
                if(_spawnTime <= 0)
                {
                    _spawnTime = _cooldown;
                    var bonusData = _BonusDataContainer.GetRandomBonus();
                    var bonusView = _bonusFactory.GetItem();

                    bonusView.SetBonus(new BonusModel(bonusData.Duration, bonusData.BonusType))
                         .SetImage(bonusData.Sprite)
                         .SetPosition(new Vector3(Random.Range(-_spawnAreaSize, _spawnAreaSize), _spawnHeiht))
                         .SetMinPosition(-_spawnHeiht)
                         .SetWalls(_gameAreaSize)
                         .SetFallingObjectData(bonusData);
                    bonusView.StartMoving();

                    if (!_bonusControllers.Contains(bonusView))
                    {
                        _bonusControllers.Add(bonusView);
                        bonusView.OnPickUp += AddBonus;
                    }
                }
                else
                {
                    _spawnTime -= Time.deltaTime;
                }
            }
            yield return null;
        }
    }
}
