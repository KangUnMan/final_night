using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class card : MonoBehaviour
{
    
    // Start is called before the first frame update
    

    // Update is called once per frame
    public void SceneChange()
    {
        NightSceneManager.Instance.LoadScene("deck_check");
    }
}
