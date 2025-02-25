using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BonusDataContainer", menuName = "ScriptableObjects/DataContainer/BonusDataContainer", order = 1)]
public class BonusDataContainer : ScriptableObject
{
    [SerializeField] private List<BonusData> _datas;

    public List<BonusData> Datas => _datas;

    public BonusData GetRandomBonus()
    {
        return _datas[Random.Range(0, _datas.Count)];
    }
}
