using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuDetection : MonoBehaviour
{

    private int _total = 0;
    void Start()
    {
        BallCountEventManager.AddListener(BallCount);
    }
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (!MenuManager.PauseMenuAlreadyActivated)
            {
                print("respond");
                MenuManager.GoToSelectedMenu(MenuName.PauseGame);
            }
        }
    }


    private void BallCount(int count)
    {
        _total += count;
      //  print(_total);
        if (_total == 5)
        {
            //    print("End");
            MenuManager.GoToSelectedMenu(MenuName.EndGame);
        }

        if (GameObject.FindGameObjectsWithTag("block").Length == 0)
        {
            MenuManager.GoToSelectedMenu(MenuName.EndGame);
        }
        
    } 
}
