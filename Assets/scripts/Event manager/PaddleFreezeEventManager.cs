using UnityEngine;
using UnityEngine.Events;


public class PaddleFreezeEventManager : MonoBehaviour
{
      private static Block Invoker;
      private static UnityAction Listener;


      public static void AddInvoker(Block invoker)
      {
            Invoker = invoker;
            if (Listener != null)
            {
                  Invoker.AddPaddleEventListener(Listener);
            }
      }

      public static void AddListener(UnityAction listener)
      {
            Listener = listener;
            if (Invoker != null)
            {
                  Invoker.AddPaddleEventListener(Listener); 
            }
      }
}
