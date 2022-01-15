using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandardBlockScript : Block
{
    // Start is called before the first frame update

    protected override void Start()
    {
           //for the standardBlock set the _defaultScore (using configData.csv) => 1
           DefaultScore = ConfigurationUtils.PointsPerStandardBlock;
           
           //gets the reference from the Start method from the base class (Block.cs)
           base.Start();
    }

    //overrides for standardBlock , updates score and destroys it 
    protected override void DestroyBlock(Collision2D other)
    {
           
           if (other.gameObject.CompareTag("Ball"))
           {
                  AudioManager.Play(AudioClipName.StandardBlock);
                  //_statusUpdate.ScoreCount(_defaultScore);
                  
                  
                  //Adding invoker to update score
                  OneArgumentEvent.Invoke(DefaultScore);
                    Destroy(gameObject);
           }
    }
}
