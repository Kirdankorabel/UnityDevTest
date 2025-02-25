using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultPanel : MonoBehaviour
{
    [SerializeField] private TMP_Text _resultMassageText;
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private Button _newGameButton;
    [SerializeField] private string _recordMassage;
    [SerializeField] private string _resultMassage;

    public event System.Action OnNewGameStarted;

    private void Awake()
    {
        _newGameButton.onClick.AddListener(StartNewGame);
    }

    public void Open(int gameResult)
    {
        gameObject.SetActive(true);
        _resultText.text = gameResult.ToString();
        _resultMassageText.text = gameResult > GameSettings.BestResult ? _recordMassage : _resultMassage;
    }

    private void StartNewGame()
    {
        OnNewGameStarted?.Invoke();
        gameObject.SetActive(false);
    }
}
