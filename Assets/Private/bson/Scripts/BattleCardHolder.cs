using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleCardHolder : MonoBehaviour
{
    public BattleCard selectedCard;  // ���� ���� ī��
    public bool isDrag = false;

    private bool isBattle = false;

    [SerializeField]
    private BattleCard _battleCardPrefab;

    [SerializeField]
    private MyList<BattleCard> testDeck;

    [SerializeField]
    private Transform _cardTransform;   //MyCard

    [SerializeField]
    private Transform _cardDeckTransform; // ī�� �� ��ġ
    [SerializeField]
    private Transform _cardCemetryTransform; // ī�� ���� ��ġ
    [SerializeField]
    private Transform _cardExtinctionTransform; // ī�� �Ҹ� ��ġ

    [SerializeField]
    private Text _cardDeckCountText;
    [SerializeField]
    private Text _cardCemetryCountText;
    [SerializeField]
    private Text _cardExtinctionCountText;

    [SerializeField]
    private MyList<BattleCard> _cardDeck;  // ī�� ��
    [SerializeField]
    private MyList<BattleCard> _cardHands; // ī�� ��
    [SerializeField]
    private MyList<BattleCard> _cardCemetry; // ī�� ����
    [SerializeField]
    private MyList<BattleCard> _cardExtinction; // ī�� �Ҹ�

    [SerializeField]
    private BezierCurve _bezierCurve;

    [SerializeField]
    private float offsetAngle;
    [SerializeField]
    private float angle;
    [SerializeField]
    private float distance;
    [SerializeField]
    private float overSidePosition = 50f;  // ī�� ���콺 ���� �� �翷���� �̵��ϴ� ��
    [SerializeField]
    private float overScale = 1.3f;  // ī�� ���콺 ���� �� �ش� ī�尡 Ŀ���� ��
    [SerializeField]
    private float overUpPosition = 200f; // ī�� ���콺 ���� �� �ش� ī�� Y��ġ ������

    private List<BattleCard> _temporaryList = new List<BattleCard>();

    public BezierCurve BezierCurve => _bezierCurve;

    public void ResumeBattle(List<BattleCard> myCard)
    {
        if (!isBattle)
            return;

        foreach (BattleCard card in myCard)
        {
            card.ChangeState(ECardUsage.Battle);

            card.transform.SetParent(_cardTransform);
            card.onClickAction = null;
        }

        foreach (BattleCard card in _cardDeck)
        {
            card.transform.localPosition = _cardDeckTransform.localPosition;
            card.transform.localEulerAngles = Vector3.zero;
            card.transform.localScale = Vector3.zero;
        }

        foreach (BattleCard card in _cardCemetry)
        {
            card.transform.localPosition = _cardCemetryTransform.localPosition;
            card.transform.localEulerAngles = Vector3.zero;
            card.transform.localScale = Vector3.zero;
        }

        Relocation();
    }

    public void StartBattle(List<BattleCard> myCard)
    {
        isBattle = true;

        // �ʱ�ȭ
        selectedCard = null;
        isDrag = false;

        // �ʿ���� ī��� ����
        while (_temporaryList.Count != 0)
        {
            BattleCard card = _temporaryList[0];
            _temporaryList.Remove(card);
            Destroy(card.gameObject);
        }

        _cardExtinctionTransform.gameObject.SetActive(false);

        _cardDeck = new MyList<BattleCard>();
        _cardHands = new MyList<BattleCard>();
        _cardCemetry = new MyList<BattleCard>();
        _cardExtinction = new MyList<BattleCard>();

        _cardDeck.onChangeList = null;
        _cardHands.onChangeList = null;
        _cardCemetry.onChangeList = null;
        _cardExtinction.onChangeList = null;

        _cardDeck.onChangeList += ShowCardCount;
        _cardHands.onChangeList += ShowCardCount;
        _cardCemetry.onChangeList += ShowCardCount;
        _cardExtinction.onChangeList += ShowCardCount;

        _temporaryList = new List<BattleCard>();

        // �� ī�� �ֱ�
        foreach (BattleCard card in myCard)
        {
            // ��ġ �ʱ�ȭ
            card.transform.SetParent(_cardTransform);

            card.ChangeState(ECardUsage.Battle);

            card.transform.localPosition = _cardDeckTransform.localPosition;
            card.transform.localEulerAngles = Vector3.zero;
            card.transform.localScale = Vector3.zero;

            _cardDeck.Add(card);
        }

        // ����
        Util.ShuffleList(_cardDeck);
    }

    private void ShowCardCount()
    {
        _cardDeckCountText.text = _cardDeck.Count.ToString();
        _cardCemetryCountText.text = _cardCemetry.Count.ToString();
        _cardExtinctionCountText.text = _cardExtinction.Count.ToString();
    }

    // �Ͻ������� ����
    public void AddCardTemporary(BattleCard card)
    {
        card.transform.localPosition = _cardDeckTransform.localPosition;
        card.transform.localEulerAngles = Vector3.zero;
        card.transform.localScale = Vector3.zero;

        _temporaryList.Add(card);
        _cardDeck.Add(card);

        // ����
        Util.ShuffleList(_cardDeck);
    }

    public void EndBattle(List<BattleCard> myCard)
    {
        DiscardAllCard();
        isBattle = false;
        _cardExtinctionTransform.gameObject.SetActive(false);
    }


    // �Ҹ�
    public void Extinction(BattleCard card)
    {
        _cardHands.Remove(card);
        _cardExtinction.Add(card);

        Relocation();
        card.CardController.MoveCard(_cardExtinctionTransform.localPosition, Vector3.zero, Vector3.zero);

        // �Ҹ�� ī�尡 1���̶� ������ �Ҹ� UI ����
        if (_cardExtinction.Count > 0)
        {
            _cardExtinctionTransform.gameObject.SetActive(true);
        }
        else
        {
            _cardExtinctionTransform.gameObject.SetActive(false);
        }
    }


    /// <summary>
    /// ī�� ��ο�
    /// </summary>
    public void DrawCard()
    {
        // �п� ī�� 10���̻��̸� ���̻� ī�带 ��ο����� ����
        if (_cardHands.Count >= 10)
        {
            return;
        }

        // ���� ī�尡 ������ ������ �ִ� ī�带 ���� ���� �ְ� ������ ���� ��ο�
        if (_cardDeck.Count <= 0)
        {
            ReturnToDeck();
            Util.ShuffleList(_cardDeck);
        }

        // ���� �� �� �̾Ƽ� �п� ����
        BattleCard card = _cardDeck[_cardDeck.Count - 1];
        _cardHands.Add(card);
        _cardDeck.Remove(card);

        // ������ ī�� ����ĳ��Ʈ Ȱ��ȭ
        card.CardController.SetActiveRaycast(true);

        Relocation();
    }

    /// <summary>
    /// ī�� ������
    /// </summary>
    /// <param name="card">���� ī��</param>
    public void DiscardCard(BattleCard card)
    {
        _cardCemetry.Add(card);
        _cardHands.Remove(card);

        Relocation();
        card.CardController.MoveCard(_cardCemetryTransform.localPosition, Vector3.zero, Vector3.zero);

        // ������ ī�� ����ĳ��Ʈ ��Ȱ��ȭ
        card.CardController.SetActiveRaycast(false);
    }

    /// <summary>
    /// �п� �ִ� ī�� ���콺 ���� �� ȣ��
    /// </summary>
    /// <param name="card">���콺�� �� ī��</param>
    public void OverCard(BattleCard card)
    {
        float startTheta = GetStartTheta();

        int index = GetCardIndex(card);

        // �ش� ī�带 �� ã���� return;
        if (index == -1)
            return;

        // ī�� ��ġ
        for (int i = 0; i < _cardHands.Count; i++)
        {
            _cardHands[i].transform.SetAsFirstSibling();

            float theta = startTheta + angle * i;
            Vector3 targetPos = transform.position + new Vector3(Mathf.Cos(theta * Mathf.Deg2Rad), Mathf.Sin(theta * Mathf.Deg2Rad), 0) * distance;
            Vector3 targetRot = new Vector3(0f, 0f, theta - 90);
            Vector3 targetScl = Vector3.one;

            if (i < index)
            {
                targetPos += Vector3.right * overSidePosition;
            }
            else if (i > index)
            {
                targetPos -= Vector3.right * overSidePosition;
            }
            else
            {
                targetPos += Vector3.up * overUpPosition;
                targetRot = Vector3.zero;
                targetScl = Vector3.one * overScale;
            }

            _cardHands[i].CardController.MoveCard(targetPos, targetRot, targetScl);
            _cardHands[i].transform.SetAsFirstSibling();
        }

        _cardHands[index].transform.SetAsLastSibling();
    }

    public void MoveCenter(BattleCard card)
    {
        Vector3 targetPos = Vector3.up * (-Screen.height / 2 + overUpPosition / 2);
        Vector3 targetRot = Vector3.zero;
        Vector3 targetScl = Vector3.one * overScale;

        card.CardController.MoveCard(targetPos, targetRot, targetScl);
    }

    // �п� �ִ� ��� ī�� ������
    public void DiscardAllCard()
    {
        while (_cardHands.Count != 0)
        {
            BattleCard card = _cardHands[_cardHands.Count - 1];
            DiscardCard(card);
        }
    }

    /// <summary>
    /// �п� �ִ� ī�� ���ġ
    /// </summary>
    public void Relocation()
    {
        float startTheta = GetStartTheta();

        // ī�� ��ġ
        for (int i = 0; i < _cardHands.Count; i++)
        {
            _cardHands[i].transform.SetAsFirstSibling();

            float theta = startTheta + angle * i;
            Vector3 targetPos = transform.position + new Vector3(Mathf.Cos(theta * Mathf.Deg2Rad), Mathf.Sin(theta * Mathf.Deg2Rad), 0) * distance;
            Vector3 targetRot = new Vector3(0f, 0f, theta - 90);
            Vector3 targetScl = Vector3.one;

            _cardHands[i].CardController.MoveCard(targetPos, targetRot, targetScl);
            _cardHands[i].transform.SetAsFirstSibling();
        }
    }


    // _cardHands�� ������ ���� ���� ���� ������ ��ȯ
    private float GetStartTheta()
    {
        float startTheta = 0;
        if (_cardHands.Count % 2 == 0)
            startTheta -= _cardHands.Count / 2 * angle - (angle / 2) - 90;
        else
            startTheta -= (_cardHands.Count / 2) * angle - 90;

        return startTheta;
    }

    // �ش� ī�尡 �п� ���°�ִ��� ��ȯ
    private int GetCardIndex(BattleCard card)
    {
        for (int i = 0; i < _cardHands.Count; i++)
        {
            if (card == _cardHands[i])
            {
                return i;
            }
        }
        return -1;
    }

    // ���� ī�� ���� ������ ��ȯ
    private void ReturnToDeck()
    {
        while (_cardCemetry.Count != 0)
        {
            BattleCard card = _cardCemetry[_cardCemetry.Count - 1];
            _cardDeck.Add(card);
            _cardCemetry.Remove(card);

            card.transform.localPosition = _cardDeckTransform.localPosition;
        }
    }


    private void Start()
    {
        int generateNumber = 0;

        for (int i = 0; i < 6; i++)
        {
            BattleCard battleCard = Instantiate(_battleCardPrefab, _cardTransform);
            battleCard.Init(this, generateNumber++);
            testDeck.Add(battleCard);
        }

        StartBattle(testDeck);

        for (int i = 0; i < 6; i++)
        {
            DrawCard();
        }
    }
}