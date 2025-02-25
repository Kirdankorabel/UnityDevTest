using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TopPanel : MonoBehaviour
{
    [SerializeField] private Transform _rootTransform;
    [SerializeField] private TMP_Text _gameResult;
    [SerializeField] private TMP_Text _bestResult;

    public void UpdateBestResult(int count)
    {
        _bestResult.text = count.ToString();
    }

    public void UpdaGameResult(int count)
    {
        _gameResult.text = count.ToString();
    }
}
