using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpeedUpEventManager : MonoBehaviour
{
   private static Block _invoker;
   private static UnityAction _listener;


   public static void AddInvoker(Block invoker)
   {
      _invoker = invoker;
      if (_listener != null)
      {
         _invoker.AddSpeedUpEffectListener(_listener);
      }
   }


   public static void AddListener(UnityAction listener)
   {
      _listener = listener;
      if (_invoker != null)
      {
         _invoker.AddSpeedUpEffectListener(_listener);
      }
   }
}
