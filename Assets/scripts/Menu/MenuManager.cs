using System;
using UnityEngine;
using Object = UnityEngine.Object;
public static class MenuManager
{
       public static bool PauseMenuAlreadyActivated = false;
       public static bool QuitButtonAlreadyActivated = false;
       public static bool HelpButtonAlreadyActivated = false;
       public static bool EndGameAlreadyActivated = false;
      public static void GoToSelectedMenu(MenuName name)
      {
            if (name == MenuName.GameMenu)
            {
                   UnityEngine.SceneManagement.SceneManager.LoadScene("menu");
            }
            else if (name == MenuName.PauseGame)
            {
                   try
                   {
                          PauseMenuAlreadyActivated = true;
                          Object.Instantiate(Resources.Load("PauseMenu"));
                   }
                   catch (Exception )
                   {
                         //ignored 
                   }
            }
            else if (name == MenuName.PlayGame)
            {
                   UnityEngine.SceneManagement.SceneManager.LoadScene("Scenes/playGame");
            }
            else if (name == MenuName.GameHelp)
            {
                   try
                   {
                          HelpButtonAlreadyActivated = true;
                          Object.Instantiate(Resources.Load("helpMenu"));
                   }
                   catch (Exception )
                   {
                         //ignored 
                   }
            }
            else if (name == MenuName.EndGame)
            {
                   try
                   {
                          EndGameAlreadyActivated = true;
                          Object.Instantiate(Resources.Load("GameEndMessage"));
                   }
                   catch (Exception)
                   {
                          // ignored
                   }
            }
      }
}

