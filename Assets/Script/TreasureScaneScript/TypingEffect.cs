using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TypingEffect : MonoBehaviour
{
    public TextMeshProUGUI Tx;
    private string m_Text = "������ ���� �Ǵ�... ���� ���� �Ϸ��� �ɱ�...";
    private string s_Text = "�޽��� ���ϸ� ü���� ȸ����������... �ʿ���ٸ� �׳� ���� ����...";
    public GameObject Statue;
    public GameObject Player;
    public GameObject NextBtn;
    public GameObject Rest;
    public GameObject Out;

    void Start()
    {
        NextBtn.SetActive(false);
        Rest.SetActive(false);
        Out.SetActive(false);
        Statue.SetActive(false);
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
        for (int i = 0; i <= m_Text.Length; i++) // <= �� �����Ͽ� ��ü �ؽ�Ʈ�� �������� ����
        {
            Tx.text = m_Text.Substring(0, i);
            yield return new WaitForSeconds(0.07f);
        }
        NextBtn.SetActive(true);
    }

    IEnumerator s_typing()
    {
        Tx.text = "";
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i <= s_Text.Length; i++) // <= �� �����Ͽ� ��ü �ؽ�Ʈ�� �������� ����
        {
            Tx.text = s_Text.Substring(0, i);
            yield return new WaitForSeconds(0.07f);
        }
        Rest.SetActive(true);
        Out.SetActive(true);
    }

    // NextBtn�� Ŭ���� �� ����� �޼���
    public void OnNextBtnClicked()
    {
        Debug.Log("NextBtn Ŭ��");
        NextBtn.SetActive(false); // ���� ��ư ��Ȱ��ȭ
        Player.SetActive(false);
        Statue.SetActive(true);
        StartCoroutine(s_typing()); // s_typing �ڷ�ƾ ����
    }
}
