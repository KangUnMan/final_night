using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Card", menuName = "Card Game/Card")]
public class CardData : ScriptableObject
{
    public string cardName;
    public int cardValue;
    public Sprite cardImage;

    // �ٸ� ī�� �Ӽ��� �߰� ����
}
