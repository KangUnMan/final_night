using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;

public enum EBattleState
{
    MyTurnStart,
    MyTurn,
    MyTurnEnd,
    EnemyTurnStart,
    EnemyTurn,
    EnemyTurnEnd,
    BattleEnd,
}

public class BattleManager : MonoBehaviour, IRegisterable
{
    private int battleCount = 0;
    public bool testMode = false;

    public System.Action onStartMyTurn;     // 내 턴 시작 시 발생
    public System.Action onEndMyTurn;       // 내 턴 끝 시 발생
    public System.Action onStartEnemyTurn;  // 적 턴 시작 시 발생
    public System.Action onEndEnemyTurn;    // 적 턴 끝 시 발생

    public System.Action onFirstMyTurn;     // 전투 내 첫 턴
    public System.Action onSecondMyTurn;    // 전투 내 두번째 턴
    
    public System.Action onStartBattle;     // 전투가 시작되면 발생
    public System.Action onEndBattle;        // 전투가 끝나면 발생
    
    public InBattleUI inBattleUI;
    public InGameOverUI inGameOverUI;

    public int defeatCommonEnemy;
    public int defeatElite;
    public int defeatBoss;
    
    public int myTurnCount = 1;
    public bool myTurn = false; //상태 전환을 위한 감시값

    public Dictionary<string, System.Action<BattleCard>> CardEffectTable { get; private set; }
    
    [SerializeField]
    private BattlePlayer _player;

    [SerializeField]
    private Transform SpawnEnemiesHierarchy;
    
    private BattleData _currentBattleData;
    
    private List<Enemy> _enemies;
    private BattleManagerStateFactory _stateFactory;
    
    private Coroutine _coBattle = null;

    private UIManager UIManager => ServiceLocator.Instance.GetService<UIManager>();
    private RewardManager rewardManager => ServiceLocator.Instance.GetService<RewardManager>();

    public BattlePlayer Player => _player;
    public List<Enemy> Enemies => _enemies;

    private Enemy targetEnemy = null;
    
    public Enemy TargetEnemy
    {
        get { return targetEnemy; }
        set
        {
            if (Player.CardHolder.selectedCard == null)
                return;

            targetEnemy?.LockOff();
            targetEnemy = value;
            targetEnemy?.LockOn();

            // 타겟이 널이 아니면 베지어 곡선 하이라이트
            Player.CardHolder.BezierCurve.Highlight(targetEnemy != null);
        }
    }
    
    public void Init()
    {
        battleCount = 0;
        _stateFactory = new BattleManagerStateFactory(this);

        makeEffectTable();
    }
    
    public void EndMyTurn()
    {
        myTurn = false;
    }
    
    public void StartBattle(BattleData battleData)
    {
        // 배틀데이터 저장
        _currentBattleData = battleData;

        // 배틀 UI 활성화
        UIManager.ShowThisUI(inBattleUI);

        // 적 생성
        _enemies = new List<Enemy>();
        for (int i = 0; i < battleData.Enemies.Count; i++)
        {
            Enemy enemy = Instantiate(battleData.Enemies[i], battleData.SpawnPos[i], Quaternion.identity, SpawnEnemiesHierarchy);
            _enemies.Add(enemy);
        }

        myTurnCount = 1;
        myTurn = true;

        onStartBattle?.Invoke();

        _stateFactory.ChangeState(EBattleState.MyTurnStart);

        if (_coBattle != null)
        {
            StopCoroutine(_coBattle);
        }
        _coBattle = StartCoroutine(CoBattle());
    }

    IEnumerator CoBattle()
    {
        while(true)
        {
            _stateFactory.CurrentState.Update();

            // 플레이어 죽음 확인
            if (_player.PlayerStat.IsDead)
                break;

            // 적 죽음 확인
            if(_enemies.Count == 0)
                break;

            yield return null;
        }

        if (_player.PlayerStat.IsDead)  // 플레이어가 죽었다.
        {
            UIManager.ShowThisUI(inGameOverUI);
        }
        else if(Player.PlayerStat.Height >= 16)  // 보스를 깼다...
        {
        }
        else
        {
            onEndBattle?.Invoke();

            Debug.Log("보상을 줍니다.");
            rewardManager.ShowReward(_currentBattleData);
        }
    }

    private void makeEffectTable()
    {
        CardEffectTable = new Dictionary<string, System.Action<BattleCard>>();
        BattleCardEffect battleCardEffect = new BattleCardEffect();

        // 현재 객체의 모든 public 메소드를 가져옵니다
        MethodInfo[] methods = battleCardEffect.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

        foreach (MethodInfo method in methods)
        {
            // 매개변수가 없는 메소드만 처리합니다
            if (method.GetParameters().Length == 1)
            {
                // 메소드를 Action으로 변환하여 Dictionary에 추가합니다
                Action<BattleCard> action = (Action<BattleCard>)Delegate.CreateDelegate(typeof(Action<BattleCard>), battleCardEffect, method);
                CardEffectTable.Add(method.Name, action);
            }
        }

        if (CardEffectTable.Count != methods.Length)
        {
            Assert.IsTrue(false, "Effect함수 로드 실패");
        }
    }
}
