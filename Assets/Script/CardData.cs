using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New GenericCard", menuName = "Card Game/GenericCard")]
public class CardData : ScriptableObject
{
    public string cardName;
    public int cardValue;
    public Sprite cardImage;

    // �ٸ� ī�� �Ӽ��� �߰� ����
}
