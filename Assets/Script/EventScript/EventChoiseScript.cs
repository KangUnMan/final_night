using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventChoiseScript : MonoBehaviour
{
    public void Event_One_Scene()
    {
        UserManager.Instance.UpdateMaxHP(UserManager.Instance.MaxHP+20);
        UserManager.Instance.UpdateCurrentHP(UserManager.Instance.CurrentHP + 20);
    }

    public void Event_Two_Scene()
    {
        UserManager.Instance.UpdateMaxHP(UserManager.Instance.MaxHP - 10);
        UserManager.Instance.UpdateCurrentHP(UserManager.Instance.CurrentHP - 10);
    }

    public void Event_Three_One_Scene()
    {
        UserManager.Instance.UpdateMaxHP(UserManager.Instance.MaxHP + 10);
        UserManager.Instance.UpdateCurrentHP(UserManager.Instance.CurrentHP + 10);
    }

    public void Event_Three_Two_Scene()
    {

    }

    public void Event_Three_Three_Scene()
    {
        UserManager.Instance.UpdateMaxHP(UserManager.Instance.MaxHP + 50);
        UserManager.Instance.UpdateCurrentHP(UserManager.Instance.CurrentHP + 50);
    }

    public void Event_Four_Scene()
    {
        UserManager.Instance.UpdateMaxHP(UserManager.Instance.MaxHP - 10);
        UserManager.Instance.UpdateCurrentHP(UserManager.Instance.CurrentHP - 10);
    }

    public void Event_Five_Scene()
    {

    }

    public void Event_Open_BOX()
    {
        UserManager.Instance.UpdateGold(UserManager.Instance.Gold + 50);
    }
}
