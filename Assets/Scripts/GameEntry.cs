using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GameEntry : MonoBehaviour
{
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private string _gameSceneName;
    [SerializeField] private string _responseCodeMassage = "ResponseCode: ";
    [SerializeField] private bool _alwaysOpen;

    private string _url = "https://ybotm4mn5d.execute-api.eu-central-1.amazonaws.com/testing";

    private void Start()
    {
        LoadGame();
    }

    private void LoadGame()
    {
        var isFrist = PlayerPrefs.GetInt(GameSettings.IsNewPlayerPrefName);
        if (_alwaysOpen || isFrist == 0)
        {
            PlayerPrefs.SetInt(GameSettings.IsNewPlayerPrefName, 1);
            StartCoroutine(FetchData());
        }
        else
        {
            Invoke("StartGame", 2f);
        }
    }

    private IEnumerator FetchData()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(_url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError ||
                request.result == UnityWebRequest.Result.ProtocolError)
            {
                _resultText.text = _responseCodeMassage + request.responseCode;
                Invoke("StartGame", 2f);
            }
            else
            {
                ProcessResponse(request.downloadHandler.text);
                Invoke("StartGame", 5f);
            }
        }
    }

    private void ProcessResponse(string jsonResponse)
    {
        try
        {
            ResponseData responseData = JsonUtility.FromJson<ResponseData>(jsonResponse);
            _resultText.text = responseData.result;
        }
        catch
        {
            Invoke("StartGame", 2f);
        }
    }

    private void StartGame()
    {
        SceneManager.LoadScene(_gameSceneName);
    }

    [System.Serializable]
    private class ResponseData
    {
        public string result;
    }
}
