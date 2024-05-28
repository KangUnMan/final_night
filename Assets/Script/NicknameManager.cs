using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using TMPro;
using UnityEngine;
using System;



public class NicknameManager : MonoBehaviour
{
    public TMP_Text nicknameText;
    public long userNum;
    private void Start()
    {
            userNum = UserManager.Instance.UserNum;
        StartCoroutine(GetNicknameCoroutine());
    }

    private IEnumerator GetNicknameCoroutine()
    {
        string url = "http://localhost:8080/api/nickname?userNum=" + userNum; // GET ��û���� ����

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
            NicknameResponse nicknameResponse = JsonUtility.FromJson<NicknameResponse>(response);
            if (nicknameResponse.success)
            {
                UserManager.Instance.SetUserNickname(nicknameResponse.nickname); // UserManager�� ���� userNickname ����
                nicknameText.text = nicknameResponse.nickname;
            }
            else
            {
                Debug.Log("�г����� ���� �������� �ʽ��ϴ�.");
            }
        }
    }

    [System.Serializable]
    private class NicknameResponse
    {
        public bool success;
        public string nickname;
    }
}