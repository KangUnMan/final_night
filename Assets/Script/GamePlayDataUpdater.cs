using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Collections.Generic;

public class GamePlayDataUpdater : MonoBehaviour
{
    private const string BaseUrl = "http://localhost:8080/gamesavedata/";

    private bool isQuitRequested;

    private void OnEnable()
    {
        Application.wantsToQuit += WantsToQuit;
    }

    private void OnDisable()
    {
        Application.wantsToQuit -= WantsToQuit;
    }

    private bool WantsToQuit()
    {
        // ���� ���� ��û�� ó���ϱ� ���� false�� ��ȯ
        isQuitRequested = true;

        // ���� �����͸� ������ ����
        GameData gameData = new GameData
        {
            HP = UserManager.Instance.HP,
            Gold = UserManager.Instance.Gold,
            CardDeckIndex = UserManager.Instance.CardDeckIndex,
            HeroCardDeckIndex = UserManager.Instance.HeroCardDeckIndex,
            Map = UserManager.Instance.Map,
        };

        // ���� ������ ������ ����
        long gameId = UserManager.Instance.DataID; // ������Ʈ�� ���� �������� ID
        StartCoroutine(UpdateGameDataAndQuit(gameId, gameData));

        return false; // ���Ḧ �Ͻ� �ߴ�
    }

    private IEnumerator UpdateGameDataAndQuit(long id, GameData gameData)
    {
        yield return PatchGameData(id, gameData);

        // ���� ���� �۾� ����
        isQuitRequested = false;
        Application.Quit();
    }

    public IEnumerator PatchGameData(long id, GameData gameData)
    {
        string url = BaseUrl + id;
        string jsonData = JsonConvert.SerializeObject(gameData); // ����ȭ ����

        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

        UnityWebRequest request = new UnityWebRequest(url, "PATCH");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("GameData update complete! Status Code: " + request.responseCode);
            Debug.Log("Sent data: " + jsonData); // ���۵� ������ �α�
        }
    }
}
