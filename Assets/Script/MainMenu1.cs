using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu1 : MonoBehaviour
{

    public void OnClickGameStart()
    {
        Debug.Log("���ӽ���");
        SceneManager.LoadScene("deck_check");
    }
    public void OnClickOption()
    {
        Debug.Log("ȯ�漳��");
    }
    public void OnClickGameQuit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}