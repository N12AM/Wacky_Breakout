using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
   //list of all kind of blocks as invokers
   //standard block, bonus, freezer, and speeder block are all inherited the (Block.cs)
   //so, we can call the (oneArgumentEvent.Invoke(point) )  normally
   public static List<Block> Invoker = new List<Block>();
   
   //we don't need a list of listeners as there is only one Listener, (StatusUpdate.cs)
   public static UnityAction<int> Listener;


   public static void AddInvoker(Block invoker)
   {
      Invoker.Add(invoker);
      if (Listener != null)
      {
         invoker.AddPointAndUpdateScoreEvent(Listener); 
      }
      
   }

   public static void AddListener(UnityAction<int> listener)
   {
      Listener = listener;

      foreach (Block invoker in Invoker)
      {
         invoker.AddPointAndUpdateScoreEvent(Listener);
      }
   }
   
   

}
