using UnityEngine;
using UnityEngine.UI;

public class rewardPopup : MonoBehaviour
{
    public GameObject popup; // �˾� â
    public Button button1;   // ù ��° ��ư
    public Button button2;   // �� ��° ��ư

    private void Start()
    {
            popup.SetActive(false);
    }
    void Update()
    {
        CheckButtons();
    }

    void CheckButtons()
    {
        // �� ��ư�� ��� ��Ȱ��ȭ�Ǿ����� Ȯ��
        if (!button1.interactable && !button2.interactable)
        {
            ClosePopup();
        }
    }

    void ClosePopup()
    {
        // �˾� â�� ��Ȱ��ȭ�ϰų� �ݴ� ����
        popup.SetActive(false);
    }
}