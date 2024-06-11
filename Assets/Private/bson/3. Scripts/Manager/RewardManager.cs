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
    private Button passRewardButton;
    [SerializeField]
    private Button moveRewardButton;
    

    private BattleCardGenerator cardGenerator => ServiceLocator.Instance.GetService<BattleCardGenerator>();
    private BattleManager battleManager => ServiceLocator.Instance.GetService<BattleManager>();
    private UIManager UIManager => ServiceLocator.Instance.GetService<UIManager>();


    public void Init()
    {
        moveRewardButton.onClick.AddListener(() => {
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
        moneyRewardText.text = money + "��带 ȹ���մϴ�.";
        moneyRewardButton.onClick.AddListener(() => GetMoney(money));
        moneyRewardButton.onClick.AddListener(() => moneyRewardButton.interactable = false);



        // ���� ī�� ����
        cardRewardButton.onClick.AddListener(() => cardRewardGameObject.SetActive(true));
    }

    // ī�� 3�� ����
    private void GetCard()
    {
        BattleCard card1 = cardGenerator.GeneratorRandomCard();
        BattleCard card2 = cardGenerator.GeneratorRandomCard();
        BattleCard card3 = cardGenerator.GeneratorRandomCard();

        card1.ChangeState(ECardUsage.Gain);
        card2.ChangeState(ECardUsage.Gain);
        card3.ChangeState(ECardUsage.Gain);

        card1.onClickAction = null;
        card2.onClickAction = null;
        card3.onClickAction = null;

        card1.onClickAction += (() => OnClickGainCard());
        card2.onClickAction += (() => OnClickGainCard());
        card3.onClickAction += (() => OnClickGainCard());

        card1.transform.SetParent(cardRewardParent);
        card2.transform.SetParent(cardRewardParent);
        card3.transform.SetParent(cardRewardParent);

        card1.transform.localScale = Vector3.one;
        card2.transform.localScale = Vector3.one;
        card3.transform.localScale = Vector3.one;
    }

    // ���� ī�带 ������ �� ����� �Լ�
    private void OnClickGainCard()
    {
        // ī�带 ���
        // ī�� ���� â�� �ݰ�
        // ī�� ������ ���ְ�

        cardRewardGameObject.gameObject.SetActive(false);
    }

    private void GetMoney(int value)
    {
        battleManager.Player.PlayerStat.Money += value;
    }

}
