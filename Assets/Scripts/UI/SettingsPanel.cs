using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField] private Button _openSettingsButton;
    [SerializeField] private Button _closeSettingsButton;
    [SerializeField] private ArrowsPanel _arrowsPanel;
    [SerializeField] private Slider _sensivitySlider;
    [SerializeField] private Button _closeButton;
    [Header("Toggles: mouse, arrows, accelerometer")]
    [SerializeField] private List<Toggle> _toggles;

    private List<IPositionGetter> _positionGetters;

    public event System.Action<IPositionGetter> OnIntupSelected;

    public void Constrult()
    {
        _openSettingsButton.onClick.AddListener(Open);
        _closeSettingsButton.onClick.AddListener(Close);
        _positionGetters = new List<IPositionGetter>()
        {
            new MouseTouchPositionGetter(),
            new ArrowsPositionGetter(_arrowsPanel),
            new AccelerometerPositionGetter()
        };

        for (int i = 0; i < _toggles.Count; i++)
        {
            int index = i;
            _toggles[i].onValueChanged.AddListener((isSelected) => OnToggleChanged(index, isSelected));
        }
        _sensivitySlider.onValueChanged.AddListener(UpdateSensivity);
    }

    private void Open()
    {
        GameSettings.IsPaused = true;
        gameObject.SetActive(true);
    }

    private void Close()
    {
        GameSettings.IsPaused = false;
        gameObject.SetActive(false);
    }

    private void OnToggleChanged(int index, bool isSelected)
    {
        if (!isSelected) return;

        GameSettings.PositionGetter = _positionGetters[index];
        _arrowsPanel.Enable(false);
        foreach (Toggle toggle in _toggles)
        {
            if (_toggles[index] != toggle)
            {
                toggle.isOn = false;
            }
        }
    }

    private void UpdateSensivity(float value)
    {
        GameSettings.Sensivity = value;
    }
}
