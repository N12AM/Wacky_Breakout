using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BallCountEventManager : MonoBehaviour
{
   private static BallController _invoker;
   private static UnityAction<int> _listener;

   public static void AddInvoker(BallController invoker)
   {
      _invoker = invoker;
      if (_listener != null)
      {
         _invoker.BallCountEventListener(_listener);
      }
   }

   public static void AddListener(UnityAction<int> listener)
   {
      _listener = listener;
      if (_invoker != null)
      {
         _invoker.BallCountEventListener(_listener);
      }
   }
   
   
   
}
