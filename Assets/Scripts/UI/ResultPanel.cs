using DG.Tweening;
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
    [SerializeField] private float _duration = 0.5f;

    public event System.Action OnNewGameStarted;

    private void Awake()
    {
        _newGameButton.onClick.AddListener(StartNewGame);
    }

    public void Open(int gameResult)
    {
        transform.localScale = Vector3.zero;
        gameObject.SetActive(true);
        _resultText.text = gameResult.ToString();
        _resultMassageText.text = gameResult > GameSettings.BestResult ? _recordMassage : _resultMassage;
        transform.DOScale(1f, _duration);
    }

    private void StartNewGame()
    {
        transform.localScale = Vector3.one;
        transform.DOScale(0f, _duration).OnComplete(() =>
        {
            OnNewGameStarted?.Invoke();
            gameObject.SetActive(false);
        });
    }
}
