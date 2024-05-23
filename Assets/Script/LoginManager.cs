using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;



public class LoginManager : MonoBehaviour
{
    public TMP_InputField userIdInput;
    public TMP_InputField passwordInput;
    public TMP_Text resultText;
    public GameObject loginpopup;

    public void OnLoginButtonClicked()
    {
        StartCoroutine(LoginCoroutine());
    }

    private IEnumerator LoginCoroutine()
    {
        string url = "http://localhost:8080/api/login"; // ������ db�� �α��� ��û
        WWWForm form = new WWWForm();
        form.AddField("userId", userIdInput.text);
        form.AddField("userPassword", passwordInput.text);

        UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            loginpopup.SetActive(true);
            resultText.text = "Error: ������ ����Ǿ������ʽ��ϴ�." + www.error;
        }
        else
        {
            string response = www.downloadHandler.text;
            LoginResponse loginResponse = JsonUtility.FromJson<LoginResponse>(response);
            if (loginResponse.success)
            {
                NightSceneManager.Instance.LoadScene("Main");
            }
            else
            {
                loginpopup.SetActive(true);
                resultText.text = "���̵� �Ǵ� ��й�ȣ�� ��ġ�����ʽ��ϴ�.";
            }
        }
    }

    [System.Serializable]
    private class LoginResponse
    {
        public bool success;
        public string message;
    }
}
