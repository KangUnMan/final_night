using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start_test : MonoBehaviour
{

    public void OnClickGameStart() //���۹�ư�� ������ ������ �̵�
    {
        Debug.Log("���ӽ���");
        NightSceneManager.Instance.LoadScene("NodeMap Test");
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