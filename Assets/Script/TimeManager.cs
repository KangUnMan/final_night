using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class TimerManager : MonoBehaviour
{
    private int secondsElapsed;
    private long gameDataId;
    private const string getPlayTimeUrl = "http://15.165.102.117:8080/gamesavedata/{id}/playtime";
    private const string updatePlayTimeUrl = "http://15.165.102.117:8080/gamesavedata/{id}/playtime";

    private static TimerManager instance;
    public static TimerManager Instance => instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        secondsElapsed = 0;
        StartCoroutine(StartTimer());
        Debug.Log("Timer started.");
    }

    private IEnumerator StartTimer()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            secondsElapsed++;
            Debug.Log("Seconds elapsed: " + secondsElapsed);
        }
    }

    public void SavePlayTimeSync()
    {
        gameDataId = UserManager.Instance.DataID;
        Debug.Log("Application is quitting. DataID: " + gameDataId + ", Elapsed seconds: " + secondsElapsed);

        string getUrl = getPlayTimeUrl.Replace("{id}", gameDataId.ToString());

        UnityWebRequest getRequest = UnityWebRequest.Get(getUrl);
        var getOperation = getRequest.SendWebRequest();

        while (!getOperation.isDone)
        {
            // ���������� �Ϸ�� ������ ���
        }

        if (getRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error getting current PlayTime: " + getRequest.error);
            return;
        }

        if (!int.TryParse(getRequest.downloadHandler.text, out int currentPlayTime))
        {
            Debug.LogError("Error parsing current PlayTime: " + getRequest.downloadHandler.text);
            return;
        }
        Debug.Log("Current playTime from server: " + currentPlayTime);

        int newPlayTime = currentPlayTime + secondsElapsed;
        Debug.Log("New playTime to be updated: " + newPlayTime);

        string patchUrl = updatePlayTimeUrl.Replace("{id}", gameDataId.ToString());

        string jsonBody = "{\"playtime\":" + newPlayTime + "}";

        UnityWebRequest patchRequest = new UnityWebRequest(patchUrl, "PATCH");
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);
        patchRequest.uploadHandler = new UploadHandlerRaw(bodyRaw);
        patchRequest.downloadHandler = new DownloadHandlerBuffer();
        patchRequest.SetRequestHeader("Content-Type", "application/json");

        var patchOperation = patchRequest.SendWebRequest();

        while (!patchOperation.isDone)
        {
            // ���������� �Ϸ�� ������ ���
        }

        if (patchRequest.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("PlayTime updated successfully to " + newPlayTime);
        }
        else
        {
            Debug.LogError("Error updating PlayTime: " + patchRequest.error);
        }
    }
}