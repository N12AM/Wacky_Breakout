using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    [SerializeField]private Text text = null;
    
    private StatusUpdate _statusUpdate;



 

    void Start()
    {
        Time.timeScale = 0f;
    _statusUpdate = GameObject.FindWithTag("hud").GetComponent<StatusUpdate>();
        
    }

    private void Update()
    {
        if (text != null)
        {
            text.text = "Score : " + _statusUpdate.CurrentScore.ToString();
        } 
    }

    public void ResumeButtonOnClick()
    {
        AudioManager.Play(AudioClipName.Click);
        Time.timeScale = 1;
        

        
        Destroy(gameObject);
        MenuManager.PauseMenuAlreadyActivated = false;
    }

    public void QuitButtonOnClick()
    {
        Time.timeScale = 1;
      //  MenuManager.QuitButtonAlreadyActivated = false;
        AudioManager.Play(AudioClipName.Click);
        MenuManager.PauseMenuAlreadyActivated = false;
        
        if (MenuManager.EndGameAlreadyActivated)
        {
            MenuManager.EndGameAlreadyActivated = false;
        }
        Destroy(gameObject);
        MenuManager.GoToSelectedMenu(MenuName.GameMenu);
    }
    
}
