using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NightSceneManager : MonoBehaviour
{
    public static NightSceneManager Instance { get; private set; }
    string loadedSceneName;

    public List<string> eventScenes = new List<string>() { // �̺�Ʈ �� ���� ����Ʈ
        "EventScene1",
        "EventScene2",
        "EventScene3",
        "EventScene4",
        "EventScene5"

    };

    private void Awake() // ��Ŭ�� ���� , ���� ������Ʈ�� ���� ���� ������ �Ȼ������ ����
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadScene(string sceneName) // ���� �̸��� �Է¹޾Ƽ� ���� �ҷ��´� (�α��� , ����ȭ�鶧 ���)
    {
        StartCoroutine(LoadSceneAsync(sceneName));
    }
    public void GameLoadScene(string sceneName) // ���� �̸��� �Է¹޾Ƽ� ���� �ҷ��´�, (�ʿ��� �ٸ� ���� �ҷ����� ����Ѵ�. �������־���ϴϱ� â���� �߰��ϴ� ������)
    {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive).completed += (AsyncOperation op) =>
        {
            loadedSceneName = sceneName;
            Debug.Log("���� �ε�� �� �̸�: " + loadedSceneName);
        };
    }


    private IEnumerator LoadSceneAsync(string sceneName) // ���� �񵿱��� ó���� ���� ��� (�� ������)
    {
        AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    public void LoadRandomScene() //���� �̺�Ʈ �� �ε�
    {
        int index = Random.Range(0, eventScenes.Count);
        string sceneToLoad = eventScenes[index];
        SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive).completed += (AsyncOperation op) =>
        {
            loadedSceneName = sceneToLoad;
            Debug.Log("���� �ε�� �� �̸�: " + loadedSceneName);
        };
    }

    public void UnloadScene() // �� ��ε� (���� ��� �ҷ����� ������ ��������, �׷��� ������Ѵ�.)
    {
        if (!string.IsNullOrEmpty(loadedSceneName))
        {
            Debug.Log("��ε� �� �̸�: " + loadedSceneName);
            SceneManager.UnloadSceneAsync(loadedSceneName);
        }
    }

    private IEnumerator UnloadSceneAsync(string sceneName)
    {
        AsyncOperation asyncUnload = UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName);

        while (!asyncUnload.isDone)
        {
            yield return null;
        }
    }
}