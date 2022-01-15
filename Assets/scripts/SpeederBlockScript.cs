using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeederBlockScript : Block
{
    // Start is called before the first frame update
    protected override void Start()
    {
        //default point 5
        DefaultScore = ConfigurationUtils.PointsPerSpeederBlock;
        SpeedUpEventManager.AddInvoker(this);
        base.Start();
    }

//overrides for standardBlock , updates score and destroys it 
    protected override void DestroyBlock(Collision2D other)
    {
           
        if (other.gameObject.CompareTag("Ball"))
        {
           AudioManager.Play(AudioClipName.SpeederBlock); 
         //   _statusUpdate.ScoreCount(_defaultScore);
            
         
            //adding invoker to update score
             OneArgumentEvent.Invoke(DefaultScore);
             SpeedUpEvent.Invoke();
             Destroy(gameObject);
        }
    }
}
