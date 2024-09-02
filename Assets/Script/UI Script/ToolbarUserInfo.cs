using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolbarUserInfo : MonoBehaviour
{
    public TMP_Text nicknameText;
    public TMP_Text GoldText;
    public TMP_Text HPText;
    public TMP_Text SPText;

    public int MaxSp = 100;
    
    private void Start()
    {
        if (UserManager.Instance != null)
        {
            UserManager.Instance.OnDataChanged += UpdateStausText;
            UpdateNickname();
            UpdateStausText();
        }
        else
        {
            Debug.LogError("UserManager Instance is null. Ensure UserManager is added to the scene.");
        }
    }

    private void UpdateNickname()
    {
        string nickname = UserManager.Instance.UserNickname;
        if (!string.IsNullOrEmpty(nickname))
        {
            nicknameText.text = nickname;
        }
        else
        {
            Debug.LogWarning("UserNickname is not set.");
        }
    }

    public void UpdateStausText()
    {
        GoldText.text = UserManager.Instance.Gold.ToString();
        HPText.text = UserManager.Instance.CurrentHP.ToString()+"/" + UserManager.Instance.MaxHP.ToString();
        SPText.text = UserManager.Instance.CurrentSP.ToString() + "/" + MaxSp;
    }
   
    public void GameExit()
    {
        Application.Quit();
#if UNITY_EDITOR
        // ����Ƽ �����Ϳ��� ���� ���� ��� �����͸� �����մϴ�.
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}