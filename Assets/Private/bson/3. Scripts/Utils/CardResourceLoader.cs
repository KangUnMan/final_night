using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UIElements;

public class CardResourceLoader
{
    private List<Dictionary<string, object>> CSV_Data;

    public Dictionary<decimal, BattleCardData> CardDataMap;
    public Dictionary<string, Sprite> CardSpriteMap;

    public List<int> AttackCardIdList = new List<int>();
    public List<int> SkillCardIdList = new List<int>();
    public List<int> HeroCardIdList = new List<int>();

    public void Init(string csvPathOrRowData)
    {
        CSV_Data = isFilePath(csvPathOrRowData) 
            ?  CSVReader.Read(csvPathOrRowData)
            : CSVReader.Parse(csvPathOrRowData);
    }

    public bool LoadCardSpriteMap()
    {
        if (CSV_Data == null)
        {
            CardSpriteMap = null;
            Debug.LogError("usage Init() first.");
            return false;
        }

        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprite/card_resource");

        CardSpriteMap = new Dictionary<string, Sprite>();

        foreach (var sprite in sprites)
        {
            CardSpriteMap[sprite.name] = sprite;
        }

        return true;
    }

    public bool LoadCardDataMap()
    {
        if (CSV_Data == null)
        {
            CardDataMap = null;
            Debug.LogError("usage Init() first.");
            return false;
        }

        CardDataMap = new Dictionary<decimal, BattleCardData>();

        foreach (Dictionary<string, object> element in CSV_Data)
        {
            BattleCardData temp = new BattleCardData();

            string txtInfo = parseInfoText(element["Constants"].ToString(), element["Description"] as string);

            if (txtInfo == string.Empty)
            {
                CardDataMap = null;
                return false;
            }

            Dictionary<string, string> spritePaths = new Dictionary<string, string>();

            spritePaths["background"] = element["resource_back"] as string;
            spritePaths["icon"] = element["resource_icon"] as string;
            spritePaths["name"] = element["resource_name"] as string;
            spritePaths["cost"] = element["resource_cost"] as string;
            spritePaths["infor"] = element["resource_infor"] as string;

            temp.setSpritePaths(spritePaths);
            
            int.TryParse(element["Id"].ToString(), out temp.id);

            temp.cardName = element["CardName"].ToString();

            int.TryParse(element["Cost"].ToString(), out temp.cost);

            temp.constants = (element["Constants"].ToString()).Split(';');

            int eType;
            int.TryParse(element["eType"].ToString(), out eType);
            temp.cardType = ECardType.Attack + eType;
            switch(temp.cardType)
            {
            case ECardType.Attack:
                AttackCardIdList.Add(temp.id);
                break;
            case ECardType.Skill:
                SkillCardIdList.Add(temp.id);
                break;
            case ECardType.Hero:
                HeroCardIdList.Add(temp.id);
                break;
            default:
                    Debug.LogError("invaild CardType; LoadCardDataMap(out Dictionary<decimal, BattleCardData> CardDataMap)");
                break; 
            }

            temp.cardTypeString = element["TypeString"].ToString();

            temp.cardExplanation = txtInfo;

            bool.TryParse(element["bBezier"].ToString(), out temp.isBezierCurve);

            bool.TryParse(element["bExtinction"].ToString(), out temp.isExtinction);

            temp.effects = (element["Effects"].ToString()).Split(';');

            CardDataMap[temp.id] = temp;
        }

        return true;
    }

    private string parseInfoText(string constants, string description)
    {
        const string CURLY_BRACES_RE = @"\{[^}]*\}";
        string ret;

        int braceCount = Regex.Matches(description, CURLY_BRACES_RE).Count;
        int splitCount = constants.Count(c => c == ';');
        if (splitCount + 1 != braceCount)
        {
            Debug.LogError("syntax error; parseInfoText(string constants, string infor)");
            return string.Empty;
        }

        string[] contantArray = constants.Split(';');
        string[] inforArray = Regex.Split(description, CURLY_BRACES_RE);

        ret = inforArray[0];

        for (int i = 0; i < contantArray.Length; i++)
        {
            ret += contantArray[i];
            ret += inforArray[i + 1];
        }
        return ret;
    }

    bool isFilePath(string input)
    {
        return input.Contains(@"\") || input.Contains(@"/");
    }
}
