using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingEffect2 : MonoBehaviour
{
    public TextMeshProUGUI Tx;
    private string[] texts = {
        "......",
        "������� ��� ó��.",
        "�����ؾ�."
    };
    public GameObject Player;
    public GameObject Paladin;
    public GameObject NextBtn;
    private int currentTextIndex = 0;

    void Start()
    {
        NextBtn.SetActive(false);
        
        Tx.text = "";

        // NextBtn�� Button ������Ʈ�� OnClick �̺�Ʈ�� �����մϴ�.
        Button nextButtonComponent = NextBtn.GetComponent<Button>();
        if (nextButtonComponent != null)
        {
            nextButtonComponent.onClick.AddListener(OnNextBtnClicked);
        }
        else
        {
            Debug.LogError("NextBtn�� Button ������Ʈ�� �����ϴ�!");
        }

        StartCoroutine(_Typing());
    }

    IEnumerator _Typing()
    {
        yield return new WaitForSeconds(1.5f);
        if (Player.activeSelf)
        {
            Player.SetActive(false);
            Paladin.SetActive(true);
        }
        else
        {
            Player.SetActive(true);
            Paladin.SetActive(false);
        }

        for (int i = 0; i <= texts[currentTextIndex].Length; i++)
        {
            Tx.text = texts[currentTextIndex].Substring(0, i);
            yield return new WaitForSeconds(0.07f);
        }
        NextBtn.SetActive(true);
    }

    // NextBtn�� Ŭ���� �� ����� �޼���
    public void OnNextBtnClicked()
    {
        Debug.Log("NextBtn Ŭ��");
        NextBtn.SetActive(false); // ���� ��ư ��Ȱ��ȭ

        // �÷��̾�� �ȶ���� Ȱ��ȭ ���¸� ��ȯ
      

        if (currentTextIndex < texts.Length - 1)
        {
            currentTextIndex++;
            StartCoroutine(_Typing()); // ���� �ؽ�Ʈ Ÿ���� ����
        }
        else
        {
            UserManager.Instance.SetNewGamePlay(1);
            Debug.Log(UserManager.Instance.NewGamePlay);
            NightSceneManager.Instance.LoadScene("NodeMap Test");
        }
    }
}
