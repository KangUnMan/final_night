using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class CardResourceMappedManager : MonoBehaviour
{
    [SerializeField] string CSV_Path;

    public void Start()
    {
        Assert.IsFalse(true); //������� �ʴ� Ŭ����; ���� ����
        GoogleSheetManager m = GetComponent<GoogleSheetManager>();

    }

    public void init(string RowData)
    {
        Assert.IsFalse(true); //������� �ʴ� Ŭ����; ���� ����
        CardResourceLoader loader = new CardResourceLoader();

        //loader.Init("CSV/CardInfo");
        loader.Init(RowData);

        loader.LoadCardDataMap();
        loader.LoadCardSpriteMap();

        //ResourceManager.Instance.Init(loader);
    }

}
