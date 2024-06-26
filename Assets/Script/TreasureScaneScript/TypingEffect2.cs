using System.Collections;
using UnityEngine;
using TMPro;

public class TypingEffect2 : MonoBehaviour
{
    public TextMeshProUGUI Tx;
    private string M_Text = "�տ� ���� �ϳ��� ���δ�.. �����?..";
    private string M2_Text = "��带 ȹ���ߴ�.";
    public GameObject Player;
    public GameObject Open;
    public GameObject Out;
    public AudioClip a;
    private AudioSource audioSource;

    void Start()
    {
        // AudioSource ������Ʈ�� �߰��մϴ�.
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = a;

        Open.SetActive(false);
        Out.SetActive(false);
        Tx.text = "";
        StartCoroutine(_Typing());
    }

    IEnumerator _Typing()
    {
        yield return new WaitForSeconds(1.5f);

        // ����� ����� �����մϴ�.
        audioSource.Play();

        for (int i = 0; i <= M_Text.Length; i++)
        {
            Tx.text = M_Text.Substring(0, i);
            yield return new WaitForSeconds(0.07f);

            // ù ��° �ؽ�Ʈ ��� �Ŀ� ����� ����� �����մϴ�.
            if (i == 0)
            {
                audioSource.Play();
            }
        }

        // ����� ����� �����մϴ�.
        audioSource.Stop();

        Open.SetActive(true);
        Out.SetActive(true);
    }

    // Open ��ư�� Ŭ���� �� ����� �޼���
    public void OnOpenClicked()
    {
        Debug.Log("���� ���� Ŭ����");
        StartCoroutine(OpenChest());
    }

    IEnumerator OpenChest()
    {
        Open.SetActive(false);
        Tx.text = "";
        yield return new WaitForSeconds(3f);

        // ����� ����� �����մϴ�.
        audioSource.Play();

        for (int i = 0; i <= M2_Text.Length; i++)
        {
            Tx.text = M2_Text.Substring(0, i);
            yield return new WaitForSeconds(0.07f);

            // ù ��° �ؽ�Ʈ ��� �Ŀ� ����� ����� �����մϴ�.
            if (i == 0)
            {
                audioSource.Play();
            }
        }

        // ����� ����� �����մϴ�.
        audioSource.Stop();
    }
}
