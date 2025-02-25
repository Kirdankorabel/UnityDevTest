using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private SettingsPanel _settingsPanel;

    private void Awake()
    {
        _settingsPanel.Constrult();
    }
}
