using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStat : CharacterStat
{
    [SerializeField] private Text hpText;
    [SerializeField] private Text energyText;
    [SerializeField] private Text moneyText;


    private int _money;
    private int _maxOrb;
    private int _currentOrb;
    private int _height;

    public int MaxOrb
    {
        get { return _maxOrb; }
        set
        {
            _maxOrb = value;
            _maxOrb = Mathf.Clamp(_maxOrb, 0, 99);
            energyText.text = _currentOrb + "/" + _maxOrb;
        }
    }

    public int CurrentOrb
    {
        get { return _currentOrb; }
        set
        {
            _currentOrb = value;
            _currentOrb = Mathf.Clamp(_currentOrb, 0, 99);
            energyText.text = _currentOrb + "/" + _maxOrb;
        }
    }

    public int Height
    {
        get { return _height; }
        set
        {
            _height = value;
        }
    }

    public int Money
    {
        get { return _money; }
        set
        {
            _money = value;
            _money = Mathf.Clamp(_money, 0, 9999);
            moneyText.text = _money.ToString();
        }
    }

    public override void Init(Character character)
    {
        base.Init(character);

        setPlayerStatData();
    }

    public void Init(Character character, int maxHp, int currentHp)
    {
        base.Init(character);

        MaxHp = maxHp;
        CurrentHp = currentHp;
        
        setPlayerStatData();
    }

    private void setPlayerStatData()
    {
        Height = 0;
        MaxOrb = 3;
        CurrentOrb = MaxOrb;
        onChangeHp += (() => hpText.text = CurrentHp + "/" + MaxHp);
    }
}
