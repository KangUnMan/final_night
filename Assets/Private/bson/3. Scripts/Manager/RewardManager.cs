using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RewardManager : MonoBehaviour, IRegisterable
{
    [SerializeField]
    private BaseUI inRewardUI;

    [SerializeField]
    private GameObject cardRewardGameObject;
    [SerializeField]
    private Transform cardRewardParent;
    [SerializeField]
    Button moneyRewardButton;
    [SerializeField]
    TMP_Text moneyRewardText;

    [SerializeField]
    Button cardRewardButton;

    [SerializeField]
    private TMP_Text[] shopCardPriceTexts;
  


    [SerializeField]
    private Button passRewardButton;
    [SerializeField]
    private Button moveRewardButton;

    private int[] shopCardPrices = new int[3];
    private BattleCard[] shopCards = new BattleCard[3];

    private BattleCardGenerator cardGenerator => ServiceLocator.Instance.GetService<BattleCardGenerator>();
    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();
    private UIManager UIManager => ServiceLocator.Instance.GetService<UIManager>();




    public void Init()
    {
        moveRewardButton.onClick.AddListener(() => {
            if (battleManager.testMode == true)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                return;
            }
            NightSceneManager.Instance.UnloadScene();
        });
    }

    // ����
    public void ShowReward(BattleData battleData)
    {
        cardRewardParent.DestroyAllChild();
        moneyRewardButton.interactable = true;

        // ����â ���ֱ�
        UIManager.ShowThisUI(inRewardUI);

        // ���� ������ ������ ī��� �ֱ� ������ ī��� �ϴ� ����
        GetCard();

        // ��
        int money = Random.Range(battleData.minMoney, battleData.maxMoney);
        moneyRewardText.text = money + " ��带 ȹ���մϴ�.";
        moneyRewardButton.onClick.AddListener(() => GetMoney(money));
        moneyRewardButton.onClick.AddListener(() => moneyRewardButton.interactable = false);


        // ���� ī�� ����
        cardRewardButton.onClick.AddListener(() => cardRewardGameObject.SetActive(true));
    }

    // ī�� 3�� ����
    public void GetCard()
    {
        GenerateRandomCard randomCardIdGenerator = FindObjectOfType<GenerateRandomCard>();

        BattleCard card1 = cardGenerator.CreateAndSetupCard(randomCardIdGenerator);
        BattleCard card2 = cardGenerator.CreateAndSetupCard(randomCardIdGenerator);
        BattleCard card3 = cardGenerator.CreateAndSetupCard(randomCardIdGenerator);

        card1.onClickAction += () => OnClickGainCard(card1);
        card2.onClickAction += () => OnClickGainCard(card2);
        card3.onClickAction += () => OnClickGainCard(card3);

        SetCardParentAndScale(card1);
        SetCardParentAndScale(card2);
        SetCardParentAndScale(card3);
    }

    private void SetCardParentAndScale(BattleCard card)
    {
        card.transform.SetParent(cardRewardParent);
        card.transform.localScale = Vector3.one;
    }


    public void GetshopCard()
    {
        GenerateRandomCard randomCardIdGenerator = FindObjectOfType<GenerateRandomCard>();

        for (int i = 0; i < 3; i++)
        {
            BattleCard card = cardGenerator.CreateAndSetupCard(randomCardIdGenerator);
            int index = i;  // Ŭ���� ���� �ذ��� ���� ���� ������ ĸó

            shopCardPrices[index] = Random.Range(50, 101);
            shopCards[index] = card;

            card.ChangeState(ECardUsage.Gain);
            card.onClickAction = null;
            card.onClickAction += (() => OnClickBuyCard(card, index));

            card.transform.SetParent(cardRewardParent);
            card.transform.localScale = Vector3.one;

            // ����ǥ ����
            shopCardPriceTexts[index].text = shopCardPrices[index] + " ���";
        }
    }

    private void OnClickBuyCard(BattleCard clickedCard, int index)
    {
        if (clickedCard == null)
        {
            Debug.LogWarning("clickedCard�� null�Դϴ�. �ùٸ��� �ʱ�ȭ�Ǿ����� Ȯ���ϼ���.");
            return;
        }

        if (UserManager.Instance.Gold >= shopCardPrices[index])
        {
            UserManager.Instance.UpdateGold(UserManager.Instance.Gold - shopCardPrices[index]);
            UserManager.Instance.CardDeckIndex.Add(clickedCard.cardID);
            Debug.Log("ī�带 �����߽��ϴ�: " + clickedCard.cardID);

            clickedCard.onClickAction = null; // ī�� Ŭ�� �̺�Ʈ ����

            // ī�� UI�� �帮�� ó��
            Image cardImage = clickedCard.transform.GetComponent<Image>();
            if (cardImage != null)
            {
                cardImage.color = new Color(cardImage.color.r, cardImage.color.g, cardImage.color.b, 0.5f); // �������ϰ� ó��
            }
            else
            {
                Debug.LogWarning("Image ������Ʈ�� �����ϴ�. ī�� UI�� ������Ʈ�� �� �����ϴ�.");
            }

            // ����ǥ�� ���� �Ϸ� ǥ��
            shopCardPriceTexts[index].text = "���� �Ϸ�";
        }
        else
        {
            Debug.LogWarning("��尡 �����մϴ�.");
        }
    }



    // ���� ī�带 ������ �� ����� �Լ�
    private void OnClickGainCard(BattleCard clickedCard)
    {
        int cardID = clickedCard.cardID;
        Debug.Log("Ŭ���� ī�� ID: " + cardID);
        UserManager.Instance.CardDeckIndex.Add(cardID);
        cardRewardGameObject.gameObject.SetActive(false);
    }

    private void GetMoney(int value)
    {
        UserManager.Instance.UpdateGold(UserManager.Instance.Gold+value);
    }

}
