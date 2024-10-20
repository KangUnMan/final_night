using System.Collections.Generic;
using UnityEngine;

public class GenerateRandomCard : MonoBehaviour
{
    private List<int> generatedCardIds = new List<int>();

    public int GetUniqueRandomCardId() // �޼��带 public���� ����
    {
        List<int> availableCardIds = new List<int>();
        for (int i = 1; i <= 16; i++) // ī�� ID ���� (1~16)
        {
            if (!generatedCardIds.Contains(i)) // �̹� ������ ī�� ID�� ����
            {
                availableCardIds.Add(i);
            }
        }

        if (availableCardIds.Count == 0)
        {
            Debug.LogError("No more unique cards available to generate.");
            return -1; // �� �̻� ������ �� �ִ� ī�尡 ���� ��
        }

        int randomIndex = Random.Range(0, availableCardIds.Count);
        int randomCardId = availableCardIds[randomIndex];
        generatedCardIds.Add(randomCardId); // ������ ī�� ID�� ����Ʈ�� �߰�
        return randomCardId;
    }
}
