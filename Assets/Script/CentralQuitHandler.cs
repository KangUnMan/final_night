using UnityEngine;
using System.Collections;
using UnityEditor;

public class CentralQuitHandler : MonoBehaviour
{
    public GamePlayDataUpdater gamePlayDataUpdater;  // Inspector���� �Ҵ�
    public TimerManager timerManager;                // Inspector���� �Ҵ�
    private bool isQuitting = false;
    private bool quitConfirmed = false;

    private void OnApplicationQuit()
    {
        if (isQuitting) return;

        isQuitting = true;
        StartCoroutine(HandleQuit());
        Application.CancelQuit(); // ���ø����̼� ���Ḧ ����ϰ� �ڷ�ƾ �Ϸ� �� ����
    }

    private IEnumerator HandleQuit()
    {
        if (gamePlayDataUpdater != null)
        {
            yield return StartCoroutine(gamePlayDataUpdater.SaveGameData());
        }
        else
        {
            Debug.LogError("GamePlayDataUpdater is not assigned in the Inspector");
        }

        if (timerManager != null)
        {
            yield return StartCoroutine(timerManager.SavePlayTime());
        }
        else
        {
            Debug.LogError("TimerManager is not assigned in the Inspector");
        }

        Debug.Log("������ ������ �Ϸ�");

        // �ڷ�ƾ �Ϸ� �� ���� �÷��� ����
        quitConfirmed = true;

        QuitApplication();
    }

    private void QuitApplication()
    {
#if UNITY_EDITOR
        // �����Ϳ��� ���� ���̸� �÷��� ��带 ����
        EditorApplication.isPlaying = false;
#else
        // ����� ���ø����̼ǿ����� ���ø����̼� ����
        Application.Quit();
#endif
    }
}
