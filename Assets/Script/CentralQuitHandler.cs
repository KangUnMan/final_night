using UnityEngine;
using System.Collections;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class CentralQuitHandler : MonoBehaviour
{
    private bool isQuitting = false;
    private bool quitConfirmed = false;

    private void OnEnable()
    {
        Application.quitting += OnQuitting;
    }

    private void OnDisable()
    {
        Application.quitting -= OnQuitting;
    }

    private void OnQuitting()
    {
        if (isQuitting) return;

        isQuitting = true;
        StartCoroutine(HandleQuit());
    }

    private IEnumerator HandleQuit()
    {
        // GamePlayDataUpdater�� TimerManager���� �����͸� �����ϴ� �ڷ�ƾ ȣ��
        yield return StartCoroutine(GamePlayDataUpdater.Instance.SaveGameData());
        yield return StartCoroutine(TimerManager.Instance.SavePlayTime());
        Debug.Log("������ ������ �Ϸ�");

        // �ڷ�ƾ �Ϸ� �� ���� �÷��� ����
        quitConfirmed = true;
    }

    private void Update()
    {
        if (isQuitting && quitConfirmed)
        {
            QuitApplication();
        }
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
