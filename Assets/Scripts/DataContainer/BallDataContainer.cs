using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BallDataContainer", menuName = "ScriptableObjects/DataContainer/BallDataContainer", order = 1)]
public class BallDataContainer : ScriptableObject
{
    [SerializeField] private List<BallData> _ballDatas;

    public BallData GetRandomBall()
    {
        return _ballDatas[Random.Range(0, _ballDatas.Count)];
    }
}
