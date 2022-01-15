using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenu : MonoBehaviour
{
       public void ReturnToMainMenuOnClick()
       {
              AudioManager.Play(AudioClipName.Click);
              MenuManager.GoToSelectedMenu(MenuName.GameMenu);
              Destroy(gameObject);
       }
}
