using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserManager : MonoBehaviour
{
    // �̱��� �ν��Ͻ�
    public static UserManager Instance { get; private set; }

    // userNum�� ������ ���� (Ŭ���� �ܺο����� ���� ���� �� ������ ������ ���� ������ ����)
    public long UserNum { get; private set; }

    // userNickname�� ������ ����
    public string UserNickname { get; private set; }

    public int Gold { get; private set; }
    // �÷��̾� ��� 
    public int HP { get; private set; }
    // �÷��̾� ü�� 
    public List<CardData> CardDeck { get; private set; }
    // �÷��̾� ��
    public string Map { get; private set; }
    // �÷��̾� �� ����

    private void Awake()
    {
        // �̱��� �ν��Ͻ� �ʱ�ȭ
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // �� ��ü�� ���ο� �� �ε� �ÿ��� �ı����� �ʰ� ��
        }
        else
        {
            Destroy(gameObject); // �ν��Ͻ��� �̹� �����ϸ� ���ο� ��ü�� �ı�
        }
    }

    // userNum ���� �޼���
    public void SetUserNum(long userNum)
    {
        UserNum = userNum;
    }

    // userNickname ���� �޼���
    public void SetUserNickname(string userNickname)
    {
        UserNickname = userNickname;
    }
}