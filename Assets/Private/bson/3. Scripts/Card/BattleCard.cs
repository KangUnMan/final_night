using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public enum ECardUsage
{
    Battle,   // ��Ʋ
    DisCard,  // ����
    Gain,     // ���
    Check,    // Ȯ��
    Size,
}
public class BattleCard : MonoBehaviour
{
    //Deprecated start
    public int generateNumber;
    public ECardType cardType;
    public int cost;
    public int cardID;
    public string cardName;
    //Deprecated end

    //public int CardId;

    public ObservableArray<int> EffectValues;

    // ��Ŭ�� �Լ�
    public Action onClickAction;

    private static int s_generationNumber = 0;

    [SerializeField]
    private BattleCardController _cardController;

    private BattleCardStateFactory _CardStateFactory;
    private BattleCardHolder _cardHolder;
    private BattleCardData _currentCardData;

    //public BattleCardData CardData => _currentCardData;
    public BattleCardState CurrentState => _CardStateFactory.CurrentState;
    public BattleCardHolder CardHolder => _cardHolder;
    public BattleCardController CardController => _cardController;
    
    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();
    Dictionary<decimal, BattleCardData> CardDataMap => ResourceManager.Instance.CardDataMap;
    Dictionary<string, Sprite> CardSpriteMap => ResourceManager.Instance.CardSpriteMap;

    //Deprecated
    /*public void Init(BattleCardHolder cardHolder, BattleCardData cardData, int generateNumber)
    {
        Assert.IsTrue(false, "Used Deprecated Func, Use \'public void Init(BattleCardHolder cardHolder, int cardId)\'");
        _CardStateFactory = new BattleCardStateFactory(this);

        _cardHolder = cardHolder;
        _currentCardData = cardData;

        _cardController.Init(_currentCardData.isBezierCurve, this);

        // ���� ������
        this.generateNumber = s_generationNumber++;
        cardType = _currentCardData.cardType;
        cost = _currentCardData.cost;
        cardName = _currentCardData.cardName;
        cardID = _currentCardData.id;

        //Image cardImage = GetComponent<Image>();
        //cardImage.sprite = _currentCardData.cardImage;
    }*/

    public void Init(BattleCardHolder cardHolder, int cardId)
    {
        updateCardResource(cardId);

        _CardStateFactory = new BattleCardStateFactory(this);
        _cardHolder = cardHolder;

        _cardController.Init(_currentCardData.isBezierCurve, this);

        // ���� ������
        this.generateNumber = s_generationNumber++;
        cardType = _currentCardData.cardType;
        cost = _currentCardData.cost;
        cardName = _currentCardData.cardName;

    }

    public void ChangeState(ECardUsage cardUsage)
    {
        _CardStateFactory.ChangeState(cardUsage);
    }
    
    public void UseCard()
    {
        if (TryUseCard())
        {
            //_cardData.useEffect.ForEach(useEffect => useEffect?.Invoke());
            foreach (var useCard in _currentCardData.effects)
            {
                battleManager.CardEffectTable[useCard]?.Invoke(this);
            }

            if(_currentCardData.isExtinction)
            {
                // �Ҹ� ī��� �Ҹ�
                _cardHolder.Extinction(this);
            }
            else
            {
                // ī�� ����
                _cardHolder.DiscardCard(this);
            }
        }
    }

    public void Discard()
    {
        // �� ī�忡�� ������
        battleManager.Player.CardList.Remove(this);
    }

    private bool TryUseCard()
    {
        if (battleManager.Player.PlayerStat.CurrentOrb >= _currentCardData.cost)
        {
            battleManager.Player.PlayerStat.CurrentOrb -= _currentCardData.cost;
            return true;
        }
        else
        {
            _cardController.SetActiveRaycast(true);
            return false;
        }
    }
    
    public void UpdateTextCardInfo()
    {
        SetTextCardInfo(_currentCardData.cardExplanation, EffectValues.ToArrayString());
    }

    private void SetTextCardInfo(string format, params object[] args)
    {
        Transform goInfor = transform.Find("infor");
        TMP_Text tmp = goInfor.GetChild(1).GetComponent<TMP_Text>();
        
        if (Regex.IsMatch(format, @"\{[0-9]+\}"))
        {
            tmp.text = string.Format(format, args); // string.Format() ���
        }
        else
        {
            tmp.text = format; // ���� �����ڰ� ������ �״�� ��ȯ (������ ���ڿ�)
        }
    }

    private void updateCardResource(int cardId)
    {
        this._currentCardData = CardDataMap[cardId];

        transform.Find("background").GetComponent<Image>().sprite = CardSpriteMap[_currentCardData.getSpritePath("background")];

        transform.Find("icon").GetComponent<Image>().sprite = CardSpriteMap[_currentCardData.getSpritePath("icon")];

        Transform goName = transform.Find("name");
        goName.GetComponent<Image>().sprite = CardSpriteMap[_currentCardData.getSpritePath("name")];
        goName.GetChild(0).GetComponent<TMP_Text>().text = _currentCardData.cardName;

        Transform goCost = transform.Find("cost");
        goCost.GetComponent<Image>().sprite = CardSpriteMap[_currentCardData.getSpritePath("cost")];
        goCost.GetChild(0).GetComponent<TMP_Text>().text = _currentCardData.cost.ToString();

        Transform goInfor = transform.Find("infor");
        goInfor.GetComponent<Image>().sprite = CardSpriteMap[_currentCardData.getSpritePath("infor")];
        goInfor.GetChild(0).GetComponent<TMP_Text>().text = _currentCardData.cardTypeString;

        EffectValues = ConvertToEffectValues(_currentCardData.constants);
        EffectValues.OnValueChanged -= UpdateTextCardInfo;
        EffectValues.OnValueChanged += UpdateTextCardInfo;

        UpdateTextCardInfo();
    }

    private ObservableArray<int> ConvertToEffectValues(string[] constants)
    {
        int[] effectValues = new int[constants.Length];

        for (int i = 0; i < constants.Length; i++)
        {
            // TryParse�� ����Ͽ� ��ȯ ���и� ����
            if (int.TryParse(constants[i], out int result))
            {
                effectValues[i] = result;
            }
            else
            {
                Assert.IsTrue(false, "Constants�� int���¸� ����");
            }
        }

        return new ObservableArray<int>(effectValues);
    }
}
