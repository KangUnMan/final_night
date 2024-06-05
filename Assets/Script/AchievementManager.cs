using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using System;

public class AchievementManager : MonoBehaviour
{
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            string newJson = "{ \"Items\": " + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.Items;
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }

    public TMP_Text clearCountText;  // UI TextMeshPro element to display clearCount
    public TMP_Text playtimeText;    // UI TextMeshPro element to display playtime
    public long userNum;

    private void Start()
    {
        userNum = UserManager.Instance.UserNum;
        StartCoroutine(GetGameDataCoroutine());
    }

    private IEnumerator GetGameDataCoroutine()
    {
        string url = "http://localhost:8080/gamesavedata/user/" + userNum; // GET ��û URL

        UnityWebRequest www = UnityWebRequest.Get(url);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            string response = www.downloadHandler.text;
            Debug.Log("Server response: " + response); // ���� ���� �α� ���
            GameData[] gameDataArray = JsonHelper.FromJson<GameData>(response); // JSON ������ �迭�� ���
            if (gameDataArray != null && gameDataArray.Length > 0)
            {
                GameData gameData = gameDataArray[0]; // Assuming you want to get the first item
                UserManager.Instance.SetData_ID(gameData.id); // ������ �����Ҷ� ����� ���̵� ���� �Ŵ����� ����
                Debug.Log(gameData.id);
                clearCountText.text = "���� Ŭ���� Ƚ�� : "+gameData.clearCount.ToString();
                playtimeText.text = "�÷���Ÿ�� : "+ FormatPlaytime(gameData.playtime);
            }
            else
            {
                Debug.Log("�����Ͱ� �������� �ʽ��ϴ�.");
            }
        }
    }

    private string FormatPlaytime(int playtimeInSeconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(playtimeInSeconds);
        return $"{time.Hours}�ð� {time.Minutes}�� {time.Seconds}��";
    }

    [System.Serializable]
    private class GameData
    {
        public bool success;
        public long id;
        public int clearCount;
        public int playtime;
    }
}
