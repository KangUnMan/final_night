using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class start_test : MonoBehaviour
{

    public void OnClickGameStart() //���۹�ư�� ������ ������ �̵�
    {
        Debug.Log("���ӽ���");
        GlobalSoundManager.Instance.PlaySE(ESE.ShowMap);
        if (UserManager.Instance.NewGamePlay == 0)
        {
           // NightSceneManager.Instance.LoadScene("chapter0");
            NightSceneManager.Instance.LoadScene("NodeMap Test");
        }
        else
        {
            NightSceneManager.Instance.LoadScene("NodeMap Test");
        }
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