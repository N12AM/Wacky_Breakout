using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Events;

public abstract class Block : MonoBehaviour
{
       protected StatusUpdate StatusUpdate;
//       private int _defaultScore = 10;
       protected int DefaultScore;

       protected OneArgumentEvent OneArgumentEvent;
       protected SpeedUpEvent SpeedUpEvent;
       protected PaddleFreezeEvent PaddleFreezeEvent;
       
       void Awake()
       {
              OneArgumentEvent = new OneArgumentEvent();
              SpeedUpEvent = new SpeedUpEvent();
              PaddleFreezeEvent = new PaddleFreezeEvent();
       }

       protected virtual void Start()
       {
              StatusUpdate = GameObject.FindGameObjectWithTag("hud").GetComponent<StatusUpdate>();
              EventManager.AddInvoker(this);
              
       }

       private void OnCollisionEnter2D(Collision2D other)
     {
            DestroyBlock(other);
     }

       //this method updates score and destroys the appropriate gameObject  
       protected abstract void DestroyBlock(Collision2D other);

       public void AddPointAndUpdateScoreEvent(UnityAction<int> listener)
       {
              OneArgumentEvent.AddListener(listener);
       }

       public void AddSpeedUpEffectListener(UnityAction listener)
       {
              SpeedUpEvent.AddListener(listener);
       }

       public void AddPaddleEventListener(UnityAction listener)
       {
              PaddleFreezeEvent.AddListener(listener);
       }
}
