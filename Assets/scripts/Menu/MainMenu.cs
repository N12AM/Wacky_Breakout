using System.Collections;
using System.Collections.Generic;
using UnityEngine;




/// <summary>
/// attach this to the main camera for the main menu buttons to work
/// </summary>
public class MainMenu : MonoBehaviour
{

       public void PlayButtonOnclick()
       {
              AudioManager.Play(AudioClipName.Click);
              MenuManager.GoToSelectedMenu(MenuName.PlayGame);
       } 
       public void HelpButtonOnClick()
       {
              AudioManager.Play(AudioClipName.Click);
              MenuManager.GoToSelectedMenu(MenuName.GameHelp);
       }

       public void QuitButtonOnClick()
       {
              Application.Quit();
       }
}
