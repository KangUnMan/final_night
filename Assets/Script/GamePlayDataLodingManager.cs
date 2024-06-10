using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Newtonsoft.Json;

public class GamePlayDataLodingManager : MonoBehaviour
{
    private int BasicHP = 80;
    private int BasicGold = 99;
    private List<int> BasicCardDeck = new List<int> { 1, 1, 1, 1, 1, 2, 2, 2, 2, 3 };
    private const string BaseUrl = "http://localhost:8080/gamesavedata/user/";

    // userNumber�� ������ ����
    public long userNumber;

    // Start is called before the first frame update
    void Start()
    {
        // ���⼭ userNumber�� ������ �� �ֽ��ϴ�.
        userNumber = UserManager.Instance.UserNum; // UserManager���� userNumber ������

        StartCoroutine(GetGameData(userNumber));
    }

    private IEnumerator GetGameData(long userNumber)
    {
        string url = BaseUrl + userNumber;
        UnityWebRequest request = UnityWebRequest.Get(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            string jsonData = request.downloadHandler.text;
            List<GameData> gameDataList = JsonConvert.DeserializeObject<List<GameData>>(jsonData);

            if (gameDataList == null || gameDataList.Count == 0)
            {
                // gameData�� null�̰ų� ����Ʈ�� ��������� �⺻ �� ����
                SetDefaultGameData();
            }
            else
            {
                // gameData�� ������ UserManager�� �� ����
                foreach (GameData gameData in gameDataList)
                {
                    SetUserManagerData(gameData);
                }
            }
        }
    }

    private void SetDefaultGameData()
    {
        UserManager.Instance.SetHP(BasicHP);
        UserManager.Instance.SetGold(BasicGold);
        UserManager.Instance.SetCardDeckindex(BasicCardDeck);


        // ������ ���
        Debug.Log("Default HP: " + BasicHP);
        Debug.Log("Default Gold: " + BasicGold);
        Debug.Log("Default CardDeckIndex: " + string.Join(", ", BasicCardDeck));
        Debug.Log("�� ������ ����");
    }

    private void SetUserManagerData(GameData gameData)
    {
        UserManager.Instance.SetHP(gameData.HP);
        UserManager.Instance.SetGold(gameData.Gold);
        UserManager.Instance.SetCardDeckindex(gameData.CardDeckIndex);
        UserManager.Instance.SetHeroCardDeckindex(gameData.HeroCardDeckIndex);
        UserManager.Instance.SetMap(gameData.Map);

        // ���� ���� ��� (������)
        Debug.Log("HP: " + gameData.HP);
        Debug.Log("Gold: " + gameData.Gold);
        Debug.Log("CardDeckIndex: " + string.Join(", ", gameData.CardDeckIndex));
        Debug.Log("HeroCardDeckIndex: " + string.Join(", ", gameData.HeroCardDeckIndex));
        Debug.Log("Map: " + JsonConvert.SerializeObject(gameData.Map, Formatting.Indented));
    }
}
