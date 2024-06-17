using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverView : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject gameOverPopup;
    public static int clearfloor;
    public TMP_Text clearfloorText;
    public TMP_Text PlaytimeText;
    
    private void Start()
    {
        clearfloor = 0;

        if (UserManager.Instance != null)
        {
            UserManager.Instance.OnDataChanged += SetActiveGamaOver;
        }
        else
        {
            Debug.LogError("UserManager Instance is null. Ensure UserManager is added to the scene.");
        }
    }
    public void OnClickSurrender()
    {
        UserManager.Instance.UpdateCurrentHP(0);
    }

    public void SetActiveGamaOver()
    {
        if (UserManager.Instance.CurrentHP <= 0 && gameOverPopup != null)
        {
            clearfloorText.text = "Ŭ������ ��������: " + clearfloor +"��������";
            gameOverPopup.SetActive(true);
        }
        else
        {
            Debug.LogWarning("gameOverPanel is null or has been destroyed.");
        }

    }

   
}
