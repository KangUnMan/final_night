using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroDeckManager : MonoBehaviour
{
    public List<HeroCardData> herodeck = new List<HeroCardData>();

    // �������� ����ī�带 �߰��ϴ� �Լ�
    public void AddCardToDeck(HeroCardData herocard)
    {
        herodeck.Add(herocard);
    }

    // �������� ����ī�带 �����ϴ� �Լ�
    public void RemoveCardFromDeck(HeroCardData herocard)
    {
        herodeck.Remove(herocard);
    }
}
